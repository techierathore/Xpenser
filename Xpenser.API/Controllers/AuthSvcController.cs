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
    public class AuthSvcController : ControllerBase
    {
        private readonly IAppUserRepository UserRepo;
        private readonly ILoggerManager AppLogger;

        public AuthSvcController(IAppUserRepository aUserRepo,
             ILoggerManager aLogger)
        {
            UserRepo = aUserRepo;
            AppLogger = aLogger;
        }

        [HttpPost("AppLogin")]
        public IActionResult AppLogin([FromBody] SvcData aLoginData)
        {
            if (null == aLoginData) return BadRequest("InValid or Blank User Details");
            try
            {
                AppLogger.LogCritical("=== Mobile Login Data Starts ===");
                var vOrgCode = TeleMetriEncrypt.TeleDecrypt(aLoginData.OrgCode);
                var vEmail = TeleMetriEncrypt.TeleDecrypt(aLoginData.LoginEmail);
                var vPass = TeleMetriEncrypt.TeleDecrypt(aLoginData.LoginPass);

                AppLogger.LogCritical("=== Org Code : " + vOrgCode + " ===");
                AppLogger.LogCritical("=== Email / User ID : " + vEmail + " ===");
                AppLogger.LogCritical("=== Password : " + vPass + " ===");
                vPass = TeleMetriEncrypt.CreateHash(vPass);
                AppLogger.LogCritical("=== PasswordHash : " + vPass + " ===");
                AppLogger.LogCritical("=== Mobile Login Data Ends ===");
                var vOrgByCode = OrgRepo.GetOrganizationByCode(vOrgCode);
                if (vOrgByCode == null)
                { return BadRequest("User Not Found"); }
                var vConString = vOrgByCode.ConString;
                //var vConString = "host=47.206.83.31;port=3306;user id=root;Password=Tipsey123.;database=megascapesdb;";

                AppLogger.LogCritical("=== Connection String : " + vConString + " ===");
                AppLogger.LogCritical("=== Mobile Login Data Ends ===");
                EmpRepo = new EmployeeRepo(vConString);
                AppUser vReturnObj = null; string sJwToken = string.Empty;
                var vValidatedByEmail = EmpRepo.GetMobileEmployee(vEmail, vPass);
                if (vValidatedByEmail != null)
                {
                    vReturnObj = GetAppUser(vValidatedByEmail, EmpRepo, vConString);
                    sJwToken = GenerateEmployeeJWToken(vValidatedByEmail, vConString);
                }
                else
                {
                    var vValidatedByMob = EmpRepo.GetMobEmployee(vEmail, vPass);
                    if (vValidatedByMob == null)
                    { return BadRequest("User Not Found"); }
                    vReturnObj = GetAppUser(vValidatedByMob, EmpRepo, vConString);
                    sJwToken = GenerateEmployeeJWToken(vValidatedByMob, vConString);
                }

                string vRetData = JsonSerializer.Serialize(vReturnObj);
                string sEncryptedData = TeleMetriEncrypt.TeleEncrypt(vRetData);
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
            AppUser vReturnObj;
            var vUserID = SvcUtils.GetUserIDFromToken(aTokenData.JwToken);
            var vValidatedToken = LoginRepo.GetUserByToken(vUserID, aTokenData.JwToken);
            if (vValidatedToken != null)
            {
                var vValidatedUser = UserRepo.GetSingle(vUserID);
                if (vValidatedUser == null)
                {
                    return BadRequest("User Not Found");
                }
                vReturnObj = new AppUser
                {
                    UserId = vValidatedUser.TeleUserId,
                    ManagerId = 0,
                    FirstName = vValidatedUser.FirstName,
                    LastName = vValidatedUser.LastName,
                    Email = vValidatedUser.Email,
                    UserRole = vValidatedUser.UserRole,
                    ConString = vValidatedUser.ConString,
                    AccessToken = aTokenData.JwToken,
                    RefreshToken = aTokenData.JwToken
                };
            }
            else
            {
                var vConString = SvcUtils.GetConnectionFromToken(aTokenData.JwToken);
                TenantLoginRepo = new UserLoginRepo(vConString);
                var vValidatedTenantToken = TenantLoginRepo.GetUserByToken(vUserID, aTokenData.JwToken);
                if (vValidatedTenantToken == null)
                { return BadRequest("User Not Found"); }
                EmpRepo = new EmployeeRepo(vConString);
                var vValidatedTenantUser = EmpRepo.GetSingle(vUserID);
                if (vValidatedTenantUser == null)
                {
                    return BadRequest("User Not Found");
                }
                vReturnObj = new AppUser
                {
                    UserId = vValidatedTenantUser.EmployeeId,
                    ManagerId = vValidatedTenantUser.ManagerId,
                    FirstName = vValidatedTenantUser.FirstName,
                    LastName = vValidatedTenantUser.LastName,
                    Email = vValidatedTenantUser.Email,
                    UserRole = vValidatedTenantUser.EmployeeTier,
                    ConString = vConString,
                    AccessToken = aTokenData.JwToken,
                    RefreshToken = aTokenData.JwToken,
                };

            }
            string vRetData = JsonSerializer.Serialize(vReturnObj);
            string sEncryptedData = AppEncrypt.EncryptText(vRetData);
            SvcData vReturnData = new SvcData()
            {
                ComplexData = sEncryptedData,
                JwToken = aTokenData.JwToken
            };
            return Ok(vReturnData);
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
