using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Xpenser.Models;

namespace Xpenser.API.Controllers
{
    public static class SvcUtils
    {
        public static long GetUserIDFromToken(string aJwToken)
        {
            var vTokenHandler = new JwtSecurityTokenHandler();
            var vTokenObject = vTokenHandler.ReadJwtToken(aJwToken);
            var vIdValue = vTokenObject.Claims.First(c => c.Type == "primarysid").Value;
            var vResult = Convert.ToInt64(vIdValue);
            return vResult;
        }
        public static string GetConnectionFromToken(string aJwToken)
        {
            var vTokenHandler = new JwtSecurityTokenHandler();
            var vTokenObject = vTokenHandler.ReadToken(aJwToken) as JwtSecurityToken;
            var vResult = vTokenObject.Claims.First(claim => claim.Type == ClaimTypes.Hash).Value;
            string sConstring = AppEncrypt.DecryptText(vResult);
            return sConstring;
        }

        public static ImagesNDoc SaveUploadedFile(IFormFile aFile)
        {
            // Saving Image on Server
            if (aFile.Length <= 0)
            {
                return null;
            }
            var vFileName = Path.GetFileNameWithoutExtension(aFile.FileName);
            var vExtension = Path.GetExtension(aFile.FileName);
            Random vRnd = new Random();
            int iRndCount = vRnd.Next(1000, 9999);
            string sImageName = vFileName + iRndCount.ToString();
            var vImageSavePath = sImageName + vExtension;
            var vFileSavePath = Path.Combine("ReceiptImages/", vImageSavePath);
            using var fileStream = new FileStream(vFileSavePath, FileMode.Create);
            aFile.CopyTo(fileStream);

            var vReceiptImage = new ImagesNDoc()
            {
                ImgDocName = aFile.FileName,
                ImgDocPath = vFileSavePath
            };
            return vReceiptImage;
        }

        public static Stream OpenFile(string aFilePath)
        {
            return File.Open(aFilePath, FileMode.Open, FileAccess.Read);
        }
    }
}
