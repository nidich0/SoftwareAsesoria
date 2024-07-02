using System;
using System.Net.Http;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Interfaces;

public interface ICalendly
{
    HttpClient GetHttpClient(string accessToken);
    Task<T> GetDataAsync<T>(string apiUri, HttpClient httpClient);
    Task<T> GetDataAsync<T>(string apiUri, string accessToken);
    Task<T> GetDataAsync<T>(string apiUri, HttpClient httpClient, FormUrlEncodedContent encodedContent);
    Task<T> GetDataAsync<T>(string apiUri, string accessToken, FormUrlEncodedContent encodedContent);
}