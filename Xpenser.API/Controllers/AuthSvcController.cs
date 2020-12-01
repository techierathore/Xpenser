using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Xpenser.API.ErrorLogging;
using Xpenser.API.Repositories;
using Xpenser.Models;

namespace Xpenser.API.Controllers
{
    [Route("[controller]")]
    public class AuthSvcController : Controller
    {
        private readonly IAppUserRepository UserRepo;
        private readonly ILoggerManager AppLogger;
        private readonly IUserLoginRepository LoginRepo;

        public AuthSvcController(IAppUserRepository aUserRepo,
            IUserLoginRepository aUserLogins,
             ILoggerManager aLogger)
        {
            UserRepo = aUserRepo;
            LoginRepo = aUserLogins;
            AppLogger = aLogger;
        }

        [HttpPost("AppSignUp")]
        public IActionResult AppSignUp([FromBody] SvcData aSignUpData)
        {
            if (aSignUpData == null)
            { return BadRequest(); }
            try
            {
                string sJwToken;
                var vUserDataJson = AppEncrypt.DecryptText(aSignUpData.ComplexData);
                AppUser vNewUser = JsonSerializer.Deserialize<AppUser>(vUserDataJson);

                var vCheckUserByEmail = UserRepo.GetUserByEmail(vNewUser.EmailID);
                if (vCheckUserByEmail != null) return BadRequest("User with this Email already present use login or Forgot Password (if you had forgotten the password) ");
                var vCheckUserByMobile = UserRepo.GetUserByMobile(vNewUser.MobileNo);
                if (vCheckUserByMobile != null) return BadRequest("User with this Phone No already present use login or Forgot Password (if you had forgotten the password) ");
                vNewUser.PasswordHash = AppEncrypt.CreateHash(vNewUser.PasswordHash);
                var iNewUserId = UserRepo.InsertToGetId(vNewUser);
                if (iNewUserId >0)
                {
                    vNewUser.AppUserId = iNewUserId;
                    sJwToken = GenerateJWToken(vNewUser);
                    var vUserLogins = new UserLogin()
                    {
                        LoginToken = sJwToken,
                        IssueDate = DateTime.Today,
                        LoginDate = DateTime.Today,
                        ExipryDate = DateTime.Today.AddDays(2),
                        TokenStatus = TokenStatus.ValidToken.ToString(),
                        UserId = vNewUser.AppUserId
                    };
                    LoginRepo.Insert(vUserLogins);
                    vNewUser.AccessToken = sJwToken;
                    vNewUser.RefreshToken = sJwToken;
                }
                else
                { return BadRequest("Unable to Save New User"); }
                string vRetData = JsonSerializer.Serialize(vNewUser);
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
                    new Claim(ClaimTypes.Email, aLoggedInUser.EmailID),
                    new Claim(ClaimTypes.Role, aLoggedInUser.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
