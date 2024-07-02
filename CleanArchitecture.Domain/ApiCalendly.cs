using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CleanArchitecture.Domain;

public sealed class ApiCalendly(IHttpClientFactory httpClientFactory) : ICalendly
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public HttpClient GetHttpClient(string accessToken)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("CalendlyClient");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        return httpClient;
    }

    public async Task<T> GetDataAsync<T>(string apiUri, HttpClient httpClient)
    {
        var response = await httpClient.GetAsync(apiUri);
        response.EnsureSuccessStatusCode(); // Check for errors

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content) ?? throw new InvalidOperationException("Failed to deserialize response.");
    }

    public async Task<T> GetDataAsync<T>(string apiUri, string accessToken)
    {
        T? dataAsync;
        using(HttpClient httpClient = GetHttpClient(accessToken))
        {
            dataAsync = await GetDataAsync<T>(apiUri, httpClient);
        }
        return dataAsync;
    }

    public async Task<T> GetDataAsync<T>(string apiUri, HttpClient httpClient, FormUrlEncodedContent encodedContent)
    {
        var response = await httpClient.PostAsync(apiUri, encodedContent);
        response.EnsureSuccessStatusCode(); // Check for errors

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content) ?? throw new InvalidOperationException("Failed to deserialize response.");
    }

    public async Task<T> GetDataAsync<T>(string apiUri, string accessToken, FormUrlEncodedContent encodedContent)
    {
        T? dataAsync;
        using (HttpClient httpClient = GetHttpClient(accessToken))
        {
            dataAsync = await GetDataAsync<T>(apiUri, httpClient, encodedContent);
        }
        return dataAsync;
    }
}