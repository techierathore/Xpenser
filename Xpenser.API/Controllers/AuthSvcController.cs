using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Xpenser.API.ErrorLogging;
using Xpenser.API.DaCore;
using Xpenser.Models;
using Xpenser.API.Common;

namespace Xpenser.API.Controllers
{
    [Route("[controller]")]
    public class AuthSvcController : Controller
    {
        private readonly IAppUserRepository UserRepo;
        private readonly ILoggerManager AppLogger;
        private readonly IUserLoginRepository LoginRepo;
        private readonly EmailService EmailService;
        private readonly AppSettings AppSettings;
        public AuthSvcController(IAppUserRepository aUserRepo,
            IUserLoginRepository aUserLogins,
            EmailService aEmailService,
            AppSettings aAppSettings,
             ILoggerManager aLogger)
        {
            UserRepo = aUserRepo;
            LoginRepo = aUserLogins;
            AppLogger = aLogger;
            EmailService = aEmailService;
            AppSettings = aAppSettings;
        }

        /// <summary>
        /// This Method Will be used by the UI for Logging into 
        /// the Application.
        /// </summary>
        /// <param name="aSignUpData"></param>
        /// <returns>Object Containing Current Logged In User</returns>
        [HttpPost("AppSignUp")]
        public IActionResult AppSignUp([FromBody] SvcData aSignUpData)
        {
            if (aSignUpData == null)
            { return BadRequest(); }
            try
            {
                var vUserDataJson = AppEncrypt.DecryptText(aSignUpData.ComplexData);
                AppUser vNewUser = JsonSerializer.Deserialize<AppUser>(vUserDataJson);

                var vCheckUserByEmail = UserRepo.GetUserByEmail(vNewUser.UserEmail);
                if (vCheckUserByEmail != null) return BadRequest("User with this Email already present use login or Forgot Password (if you had forgotten the password) ");
                var vCheckUserByMobile = UserRepo.GetUserByMobile(vNewUser.MobileNo);
                if (vCheckUserByMobile != null) return BadRequest("User with this Phone No already present use login or Forgot Password (if you had forgotten the password) ");
                vNewUser.PasswordHash = AppEncrypt.CreateHash(vNewUser.PasswordHash);
                vNewUser.UserRole = AppConstants.AppUseRole;
                var vVerificationCode = $"{vNewUser.UserEmail}#{vNewUser.LastName}#{DateTime.UtcNow}";
                vNewUser.VerificationCode = AppEncrypt.CreateHash(vVerificationCode);
                var vNewUserId = UserRepo.InsertToGetId(vNewUser);
                if (vNewUserId > 0)
                {
                    vNewUser.AppUserId = vNewUserId;
                    SendVerificationEmail(vNewUser);
                }
                else
                {
                    return BadRequest("Unable to Save New User");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                AppLogger.LogCritical(ex.Message);
                return BadRequest(ex);
            }
        }
        [Route("[action]/{VerificationCode}")]
        [HttpPatch]
        public IActionResult UpdateVerificationCode(string VerificationCode)
        {
            try
            {
                UserRepo.UpdateVerificationCode(VerificationCode);
                return Ok();
            }
            catch (Exception ex)
            {
                AppLogger.LogCritical(ex.Message);
                return BadRequest(ex);
            }
        }
        [HttpPost("VerifyEmail")]
        public IActionResult VerifyEmail([FromBody] SvcData aVerifyEmailData)
        {
            if (aVerifyEmailData == null)
            {
                return BadRequest();
            }

            try
            {
                var vVerificationCode = AppEncrypt.DecryptText(aVerifyEmailData.VerificationCode);
                var vAppUser = UserRepo.GetUserByVerificationCode(vVerificationCode);

                if (vAppUser == null)
                {
                    return BadRequest("Invalid verification code.");
                }

                if (!vAppUser.IsVerified)
                {
                    vAppUser.IsVerified = true;
                    vAppUser.VerificationCode = null;
                    UserRepo.Update(vAppUser);
                }

                var vRetData = JsonSerializer.Serialize(vAppUser);
                var vEncryptedData = AppEncrypt.EncryptText(vRetData);
                var vReturnData = new SvcData { ComplexData = vEncryptedData };

                return Ok(vReturnData);
            }
            catch (Exception ex)
            {
                AppLogger.LogCritical(ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost("ResendVerifiEmail")]
        public IActionResult ResendVerifiEmail([FromBody] SvcData aVerifiEmailData)
        {
            if (aVerifiEmailData == null)
            {
                return BadRequest();
            }

            try
            {
                var vLoginEmail = AppEncrypt.DecryptText(aVerifiEmailData.LoginEmail);
                var vUser = UserRepo.GetUserByEmail(vLoginEmail);

                if (vUser != null)
                {
                    SendVerificationEmail(vUser);
                    return Ok();
                }
                else
                {
                    return BadRequest("User Not Found");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogCritical(ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdateNSendVerifiEmail")]
        public IActionResult UpdateNSendVerifiEmail([FromBody] SvcData aVerifiEmailData)
        {
            if (aVerifiEmailData == null)
            {
                return BadRequest();
            }

            try
            {
                var vLoginEmail = AppEncrypt.DecryptText(aVerifiEmailData.LoginEmail);
                var vUser = UserRepo.GetUserByEmail(vLoginEmail);

                if (vUser != null)
                {
                    var vNewUserJson = AppEncrypt.DecryptText(aVerifiEmailData.ComplexData);
                    var vNewUser = JsonSerializer.Deserialize<AppUser>(vNewUserJson);

                    var vCheckUserByEmail = UserRepo.GetUserByEmail(vNewUser.UserEmail);

                    if (vCheckUserByEmail != null)
                    {
                        return BadRequest("Verification email has been already sent.");
                    }

                    vUser.UserEmail = vNewUser.UserEmail;

                    // UserEmail is changing so we need to create a new VerificationCode
                    var vVerificationCode = $"{vUser.UserEmail}#{vUser.LastName}#{DateTime.UtcNow}";
                    vUser.VerificationCode = AppEncrypt.CreateHash(vVerificationCode);

                    UserRepo.UpdateUserEmail(vUser);

                    SendVerificationEmail(vUser);

                    return Ok();
                }
                else
                {
                    return BadRequest("User Not Found");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogCritical(ex.Message);
                return BadRequest(ex);
            }
        }
        [HttpPost("AppLogin")]
        public IActionResult AppLogin([FromBody] SvcData aLoginData)
        {
            if (aLoginData == null)
            { return BadRequest(); }
            try
            {
                string sJwToken;
                var vEmail = AppEncrypt.DecryptText(aLoginData.LoginEmail);
                var vPass = AppEncrypt.DecryptText(aLoginData.LoginPass);
                vPass = AppEncrypt.CreateHash(vPass);
                var vValidatedUser = UserRepo.GetLoginUser(vEmail, vPass);
                if (vValidatedUser != null)
                {
                    sJwToken = GenerateJWToken(vValidatedUser);
                    var vUserLogins = new UserLogin()
                    {
                        LoginToken = sJwToken,
                        IssueDate = DateTime.Today,
                        LoginDate = DateTime.Today,
                        ExipryDate = DateTime.Today.AddDays(2),
                        TokenStatus = TokenStatus.ValidToken.ToString(),
                        UserId = vValidatedUser.AppUserId
                    };
                    LoginRepo.Insert(vUserLogins);
                    vValidatedUser.AccessToken = sJwToken;
                    vValidatedUser.RefreshToken = sJwToken;
                }
                else
                { return BadRequest("User Not Found"); }
                string vRetData = JsonSerializer.Serialize(vValidatedUser);
                string sEncryptedData = AppEncrypt.EncryptText(vRetData);
                SvcData vReturnData = new SvcData()
                {
                    ComplexData = sEncryptedData,
                    JwToken = sJwToken
                };
                return Ok(vReturnData);
            }
            catch (Exception ex)
            {
                AppLogger.LogCritical(ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost("GetUserByToken")]
        public IActionResult GetUserByToken([FromBody] SvcData aTokenData)
        {
            if (aTokenData == null)
            { return BadRequest(); }
            var vUserID = SvcUtils.GetUserIDFromToken(aTokenData.JwToken);
            var vValidatedToken = LoginRepo.GetUserByToken(vUserID, aTokenData.JwToken);
            if (vValidatedToken != null)
            {
                var vValidatedUser = UserRepo.GetSingle(vUserID);
                if (vValidatedUser == null)
                {
                    return BadRequest("User Not Found");
                }
                vValidatedUser.AccessToken = aTokenData.JwToken;
                vValidatedUser.RefreshToken = aTokenData.JwToken;
                string vRetData = JsonSerializer.Serialize(vValidatedUser);
                string sEncryptedData = AppEncrypt.EncryptText(vRetData);
                SvcData vReturnData = new SvcData()
                {
                    ComplexData = sEncryptedData,
                    JwToken = aTokenData.JwToken
                };
                return Ok(vReturnData);
            }
            else { return BadRequest("User Not Found"); }

        }
        [HttpPost("SendPasswordResetEmail")]
        public IActionResult SendPasswordResetEmail([FromBody] SvcData aPasswordResetData)
        {
            if (aPasswordResetData == null)
            {
                return BadRequest();
            }

            try
            {
                var vLoginEmail = AppEncrypt.DecryptText(aPasswordResetData.LoginEmail);
                //var vLoginEmail = aPasswordResetData.LoginEmail;
                var vAppUser = UserRepo.GetUserByEmail(vLoginEmail);

                if (vAppUser != null)
                {
                    var vVerificationCode = $"{vAppUser.UserEmail}#{vAppUser.LastName}#{DateTime.UtcNow}";
                    vAppUser.VerificationCode = AppEncrypt.CreateHash(vVerificationCode);

                    UserRepo.Update(vAppUser);

                    var vPasswordResetUrl = $"{AppSettings.WebAppBaseUrl}/ReSetPass/{vAppUser.VerificationCode}";

                    var vSubject = "Password reset link";

                    using var vStreamReader = System.IO.File.OpenText("EmailTemplates/ResetPassword.html");
                    var vTemplate = vStreamReader.ReadToEnd();
                    vTemplate = vTemplate.Replace("{PageTitle}", vSubject);
                    vTemplate = vTemplate.Replace("{FirstName}", vAppUser.FirstName);
                    vTemplate = vTemplate.Replace("{VerifyUrl}", vPasswordResetUrl);
                    EmailService.SendEmail(vAppUser.FullName, vAppUser.UserEmail, vSubject, vTemplate);
                    return Ok();
                }
                else
                {
                    return BadRequest("User Not Found");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogCritical(ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] SvcData aPasswordResetData)
        {
            if (aPasswordResetData == null)
            {
                return BadRequest();
            }

            try
            {
                var vVerificationCode = AppEncrypt.DecryptText(aPasswordResetData.VerificationCode);
                //var vVerificationCode = aPasswordResetData.VerificationCode;

                var vAppUser = UserRepo.GetUserByVerificationCode(vVerificationCode);

                if (vAppUser != null)
                {
                    var vUserDataJson = AppEncrypt.DecryptText(aPasswordResetData.ComplexData);
                    var vNewUser = JsonSerializer.Deserialize<AppUser>(vUserDataJson);
                    //var vNewUser = JsonSerializer.Deserialize<AppUser>(aPasswordResetData.ComplexData);

                    vAppUser.PasswordHash = AppEncrypt.CreateHash(vNewUser.PasswordHash);
                    vAppUser.IsVerified = true;
                    vAppUser.VerificationCode = null;
                    UserRepo.Update(vAppUser);

                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid verification code (Link Expired)");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogCritical(ex.Message);
                return BadRequest(ex);
            }
        }

        private string GenerateJWToken(AppUser aLoggedInUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppConstants.JWTTokenGenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.PrimarySid,Convert.ToString(aLoggedInUser.AppUserId)),
                    new Claim(ClaimTypes.Name,aLoggedInUser.FullName),
                    new Claim(ClaimTypes.Email, aLoggedInUser.UserEmail),
                    new Claim(ClaimTypes.Role, aLoggedInUser.UserRole)
                }),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private void SendVerificationEmail(AppUser aUser)
        {
            var vVerificationUrl = $"{AppSettings.WebAppBaseUrl}/LoginPage/{aUser.VerificationCode}";

            var vSubject = "Email Verification Link";

            using var vStreamReader = System.IO.File.OpenText("EmailTemplates/VerifyEmail.html");
            string vTemplate = vStreamReader.ReadToEnd();
            vTemplate = vTemplate.Replace("{PageTitle}", vSubject);
            vTemplate = vTemplate.Replace("{FirstName}", aUser.FirstName);
            vTemplate = vTemplate.Replace("{VerifyUrl}", vVerificationUrl);
            vTemplate = vTemplate.Replace("{SiteUrl}", AppSettings.SiteUrl);
            vTemplate = vTemplate.Replace("{LogoUrl}", AppSettings.SiteLogoUrl);
            vTemplate = vTemplate.Replace("{HelpUrl}", AppSettings.HelpUrl);
            vTemplate = vTemplate.Replace("{AppName}", AppSettings.AppName);
            vTemplate = vTemplate.Replace("{CopyRightMessage}", AppSettings.CopyRightMessage);
            EmailService.SendEmail(aUser.FullName, aUser.UserEmail, vSubject, vTemplate);
        }

    }
}
