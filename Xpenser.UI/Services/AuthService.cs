using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Xpenser.Models;

namespace Xpenser.UI.Services
{
    public class AuthService : IAuthService
    {
        public HttpClient SvcClient { get; }
        public AppSettings SvcBaseUrl { get; }
        readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        protected const string LoginSvcUrl = "AuthSvc/AppLogin";
        protected const string RegSvcUrl = "AuthSvc/AppSignUp";
        protected const string RefreshTokenSvcUrl = "AuthSvc/RefreshToken";
        protected const string UserByTokenSvcUrl = "AuthSvc/GetUserByToken";
        public AuthService(HttpClient aSvcClient, IOptions<AppSettings> aSvcUrlSetting)
        {
            SvcBaseUrl = aSvcUrlSetting.Value;

            aSvcClient.BaseAddress = new Uri(SvcBaseUrl.ServiceBaseAddress);
            aSvcClient.DefaultRequestHeaders.Add(AppConstants.UserAgent, AppConstants.AppTypeBlazor);

            SvcClient = aSvcClient;
        }

        public async Task<AppUser> LoginAsync(SvcData aLoginUser)
        {
            try
            {
                aLoginUser.LoginEmail = AppEncrypt.EncryptText(aLoginUser.LoginEmail);
                aLoginUser.LoginPass = AppEncrypt.EncryptText(aLoginUser.LoginPass);
                string serializedUser = JsonSerializer.Serialize(aLoginUser);

                var vRequestMessage = new HttpRequestMessage(HttpMethod.Post, LoginSvcUrl)
                {
                    Content = new StringContent(serializedUser)
                };
                vRequestMessage.Content.Headers.ContentType
                    = new System.Net.Http.Headers.MediaTypeHeaderValue(AppConstants.JsonMediaTypeHeader);

                var vSvcResponse = await SvcClient.SendAsync(vRequestMessage);
                if (vSvcResponse.IsSuccessStatusCode)
                {
                    var vResponseBody = await vSvcResponse.Content.ReadAsStreamAsync();
                    SvcData vSvcRetObj = await JsonSerializer.DeserializeAsync<SvcData>(vResponseBody, JsonOptions);
                    string sDeCryptedUser = AppEncrypt.DecryptText(vSvcRetObj.ComplexData);
                    return JsonSerializer.Deserialize<AppUser>(sDeCryptedUser, JsonOptions);
                } return null;
            }
            catch (Exception)
            { throw; }
         }

        public async Task<AppUser> RegisterUserAsync(SvcData aLoginUser)
        {
            aLoginUser.ComplexData = AppEncrypt.EncryptText(aLoginUser.ComplexData);
            string serializedUser = JsonSerializer.Serialize(aLoginUser);

            var vRequestMessage = new HttpRequestMessage(HttpMethod.Post, RegSvcUrl)
            {
                Content = new StringContent(serializedUser)
            };
            vRequestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue(AppConstants.JsonMediaTypeHeader);
            try
            { 
            var vSvcResponse = await SvcClient.SendAsync(vRequestMessage);
                if (vSvcResponse.IsSuccessStatusCode)
                {
                    var vResponseBody = await vSvcResponse.Content.ReadAsStreamAsync();
                    SvcData vSvcRetObj = await JsonSerializer.DeserializeAsync<SvcData>(vResponseBody, JsonOptions);
                    string sDeCryptedUser = AppEncrypt.DecryptText(vSvcRetObj.ComplexData);
                    return JsonSerializer.Deserialize<AppUser>(sDeCryptedUser, JsonOptions);
                }
                else throw new Exception(vSvcResponse.ReasonPhrase);
            }
            catch (Exception)
            { throw; }
        }
        public async Task<AppUser> RefreshTokenAsync(RefreshRequest aRefreshRequest)
        {
            string vSerializedUser = JsonSerializer.Serialize(aRefreshRequest);

            var vRequestMessage = new HttpRequestMessage(HttpMethod.Post, RefreshTokenSvcUrl)
            {
                Content = new StringContent(vSerializedUser)
            };
            vRequestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue(AppConstants.JsonMediaTypeHeader);

            var vSvcResponse = await SvcClient.SendAsync(vRequestMessage);
            var vResponseBody = await vSvcResponse.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<AppUser>(vResponseBody, JsonOptions);
        }

        public async Task<AppUser> GetUserByAccessTokenAsync(string aSvcAccessToken)
        {
            SvcData vVerifyToken = new SvcData() { JwToken = aSvcAccessToken };
            string vRefreshRequest = JsonSerializer.Serialize(vVerifyToken);

            var vRequestMessage = new HttpRequestMessage(HttpMethod.Post, UserByTokenSvcUrl)
            {
                Content = new StringContent(vRefreshRequest)
            };
            vRequestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue(AppConstants.JsonMediaTypeHeader);
            var vSvcResponse = await SvcClient.SendAsync(vRequestMessage);
            var vResponseBody = await vSvcResponse.Content.ReadAsStreamAsync();
            SvcData vSvcRetObj = await JsonSerializer.DeserializeAsync<SvcData>(vResponseBody, JsonOptions);
            string sDeCryptedUser = AppEncrypt.DecryptText(vSvcRetObj.ComplexData);
            return JsonSerializer.Deserialize<AppUser>(sDeCryptedUser, JsonOptions);
        }
    }
}
