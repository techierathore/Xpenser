using Blazored.LocalStorage;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Xpenser.Models;

namespace Xpenser.UI.Services;

public class ManageService<TEntity> : IManageService<TEntity>
{

    public HttpClient ServiceClient { get; }
    public AppSettings SvcBaseUrl { get; }
    public ILocalStorageService LocalStorageSvc { get; }
    readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    public ManageService(HttpClient aSvcClient,
        IOptions<AppSettings> aSvcUrlSetting
        , ILocalStorageService aLocalStorageSvc)
    {
        SvcBaseUrl = aSvcUrlSetting.Value;
        LocalStorageSvc = aLocalStorageSvc;
        aSvcClient.BaseAddress = new Uri(SvcBaseUrl.ServiceBaseAddress);
        aSvcClient.DefaultRequestHeaders.Add(AppConstants.UserAgent, AppConstants.AppTypeBlazor);
        ServiceClient = aSvcClient;
    }
    public async Task<List<TEntity>> GetAllAsync(string aRequestUri)
    {
        try
        {
            var vRequestMessage = new HttpRequestMessage(HttpMethod.Get, aRequestUri);

            var vAccessToken = await LocalStorageSvc.GetItemAsync<string>(AppConstants.AccessKey);
            vRequestMessage.Headers.Authorization
                = new AuthenticationHeaderValue(AppConstants.BearerKey, vAccessToken);

            var vServiceResponse = await ServiceClient.SendAsync(vRequestMessage);
            if (vServiceResponse.IsSuccessStatusCode)
            {
                var vResponseBody = await vServiceResponse.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<TEntity>>(vResponseBody, JsonOptions);
            }
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public async Task<List<TEntity>> GetReportAsync(string aRequestUri, ReportInput aObj)
    {
        try
        {
            string sSerialisedObj = JsonSerializer.Serialize(aObj);
            var vRequestMessage = new HttpRequestMessage(HttpMethod.Post, aRequestUri);
            var vAccessToken = await LocalStorageSvc.GetItemAsync<string>(AppConstants.AccessKey);
            vRequestMessage.Headers.Authorization
                = new AuthenticationHeaderValue(AppConstants.BearerKey, vAccessToken);
            vRequestMessage.Content = new StringContent(sSerialisedObj);
            vRequestMessage.Content.Headers.ContentType
                = new MediaTypeHeaderValue(AppConstants.JsonMediaTypeHeader);

            var vServiceResponse = await ServiceClient.SendAsync(vRequestMessage);
            if (vServiceResponse.IsSuccessStatusCode)
            {
                var vResponseBody = await vServiceResponse.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<TEntity>>(vResponseBody, JsonOptions);
            }
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public async Task<List<TEntity>> GetAllSubsAsync(string aRequestUri, long aId)
    {
        try
        {
            var vRequestMessage = new HttpRequestMessage(HttpMethod.Get, aRequestUri + aId);
            var vAccessToken = await LocalStorageSvc.GetItemAsync<string>(AppConstants.AccessKey);
            vRequestMessage.Headers.Authorization
                = new AuthenticationHeaderValue(AppConstants.BearerKey, vAccessToken);

            var vServiceResponse = await ServiceClient.SendAsync(vRequestMessage);
            if (vServiceResponse.IsSuccessStatusCode)
            {
                var vResponseBody = await vServiceResponse.Content.ReadAsStreamAsync();
                if (vResponseBody.Length > 0)
                {
                    return await JsonSerializer.DeserializeAsync<List<TEntity>>(vResponseBody, JsonOptions);
                }
                else return null;
            }
            else return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public async Task<List<TEntity>> GetAllByStringAsync(string aRequestUri, string aValue)
    {
        try
        {
            var vRequestMessage = new HttpRequestMessage(HttpMethod.Get, aRequestUri + aValue);
            var vAccessToken = await LocalStorageSvc.GetItemAsync<string>(AppConstants.AccessKey);
            vRequestMessage.Headers.Authorization
                = new AuthenticationHeaderValue(AppConstants.BearerKey, vAccessToken);

            var vServiceResponse = await ServiceClient.SendAsync(vRequestMessage);
            if (vServiceResponse.IsSuccessStatusCode)
            {
                var vResponseBody = await vServiceResponse.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<TEntity>>(vResponseBody, JsonOptions);
            }
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public async Task<TEntity> GetSingleAsync(string aRequestUri, long aId)
    {
        try
        {
            var vRequestMessage = new HttpRequestMessage(HttpMethod.Get, aRequestUri + aId);
            var vAccessToken = await LocalStorageSvc.GetItemAsync<string>(AppConstants.AccessKey);
            vRequestMessage.Headers.Authorization
                = new AuthenticationHeaderValue(AppConstants.BearerKey, vAccessToken);

            var vServiceResponse = await ServiceClient.SendAsync(vRequestMessage);
            var vResponseBody = await vServiceResponse.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TEntity>(vResponseBody, JsonOptions);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public async Task<TEntity> GetIntSingleAsync(string aRequestUri, int aId)
    {
        try
        {
            var vRequestMessage = new HttpRequestMessage(HttpMethod.Get, aRequestUri + aId);
            var vAccessToken = await LocalStorageSvc.GetItemAsync<string>(AppConstants.AccessKey);
            vRequestMessage.Headers.Authorization
                = new AuthenticationHeaderValue(AppConstants.BearerKey, vAccessToken);

            var vServiceResponse = await ServiceClient.SendAsync(vRequestMessage);
            var vResponseBody = await vServiceResponse.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TEntity>(vResponseBody, JsonOptions);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public async Task<TEntity> SaveAsync(string aRequestUri, TEntity aObj)
    {
        try
        {
            string sSerialisedObj = JsonSerializer.Serialize(aObj);
            var vRequestMessage = new HttpRequestMessage(HttpMethod.Post, aRequestUri);
            var vAccessToken = await LocalStorageSvc.GetItemAsync<string>(AppConstants.AccessKey);
            vRequestMessage.Headers.Authorization
                = new AuthenticationHeaderValue(AppConstants.BearerKey, vAccessToken);
            vRequestMessage.Content = new StringContent(sSerialisedObj);
            vRequestMessage.Content.Headers.ContentType
                = new MediaTypeHeaderValue(AppConstants.JsonMediaTypeHeader);

            var vServiceResponse = await ServiceClient.SendAsync(vRequestMessage);
            var vResponseBody = await vServiceResponse.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TEntity>(vResponseBody, JsonOptions);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public async Task<TEntity> UpdateAsync(string aRequestUri, TEntity aObj)
    {
        try
        {
            string sSerialisedObj = JsonSerializer.Serialize(aObj);
            var vRequestMessage = new HttpRequestMessage(HttpMethod.Put, aRequestUri);
            var vAccessToken = await LocalStorageSvc.GetItemAsync<string>(AppConstants.AccessKey);
            vRequestMessage.Headers.Authorization
                = new AuthenticationHeaderValue(AppConstants.BearerKey, vAccessToken);
            vRequestMessage.Content = new StringContent(sSerialisedObj);
            vRequestMessage.Content.Headers.ContentType
                = new MediaTypeHeaderValue(AppConstants.JsonMediaTypeHeader);

            var vServiceResponse = await ServiceClient.SendAsync(vRequestMessage);
            var vResponseBody = await vServiceResponse.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TEntity>(vResponseBody, JsonOptions);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<bool> UploadFile(string aRequestUri, TEntity aObj,
                                                Stream aFiles, string aFileName)
    {
        try
        {
            string sSerialisedObj = JsonSerializer.Serialize(aObj);
            var vAccessToken = await LocalStorageSvc.GetItemAsync<string>(AppConstants.AccessKey);
            var vAuthHeader = new AuthenticationHeaderValue(AppConstants.BearerKey, vAccessToken);
            ServiceClient.DefaultRequestHeaders.Authorization = vAuthHeader;
            sSerialisedObj = AppEncrypt.EncryptText(sSerialisedObj);
            var aInputData = new DocsWithFiles()
            {
                ComplexData = sSerialisedObj,
                DocFile = aFiles
            };
            MultipartFormDataContent vMultiPartData = new MultipartFormDataContent();
            HttpContent vImageContent = new StreamContent(aInputData.DocFile);
            vImageContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "DocFile",
                FileName = aFileName
            };
            vImageContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            vMultiPartData.Add(vImageContent);
            vMultiPartData.Add(new StringContent(aInputData.ComplexData), "ComplexData");
            var vSvcResponse = await ServiceClient.PostAsync(aRequestUri, vMultiPartData);
            return vSvcResponse.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public async Task<byte[]> DownloadFile(string aRequestUri, long aId)
    {
        try
        {
            var vAccessToken = await LocalStorageSvc.GetItemAsync<string>(AppConstants.AccessKey);
            ServiceClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue(AppConstants.BearerKey, vAccessToken);

            var vServiceResponse = await ServiceClient.GetByteArrayAsync(aRequestUri + aId);
            return vServiceResponse;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
