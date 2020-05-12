using System;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using Newtonsoft.Json;

namespace Core.Services.Implementations
{
  public class HttpHelper : IHttpHelper
  {
    private static readonly HttpClient Client = new HttpClient
                                                {
                                                  Timeout = TimeSpan.FromSeconds(30)
                                                };
    public async Task<T> GetAsync<T>(string urlToGet)
    {
      var response = await Client.GetAsync(urlToGet);
      if (!response.IsSuccessStatusCode)
      {
        throw new Exception($"Failure in Service Call {urlToGet}");
      }
      var result = await response.Content.ReadAsStringAsync();
      if (!string.IsNullOrEmpty(result))
      {
        return JsonConvert.DeserializeObject<T>(result);
      }
      else
      {
        throw new Exception("Invalid Response Received");
      }
    }
  }
}
