using System;
using System.Threading.Tasks;
using Core.Models;
using Core.Services.Interfaces;

namespace Core.Services.Implementations
{
  public class WeatherForecastService : IWeatherForecastService
  {

    private IHttpHelper _httpHelper;
    public WeatherForecastService(IHttpHelper httpHelper)
    {
      _httpHelper = httpHelper;
    }

    public async  Task<ForecastResponse> GetFiveDayWeatherForecast(int locationId)
    {
      string forecastAddress = "http://api.openweathermap.org/data/2.5/forecast?appid=3c4bbae75023b7aefbb2246bc777e336&id=" + locationId;
      //var response = await HttpHelper.Client.GetAsync(forecastAddress);
      //var result = await response.Content.ReadAsStringAsync();
      //if (!string.IsNullOrEmpty(result))
      //{
      //  return JsonConvert.DeserializeObject<ForecastResponse>(result);
      //}
      //else
      //{
      //  throw new Exception("Invalid Response Received");
      //}
      return await _httpHelper.GetAsync<ForecastResponse>(forecastAddress);
    }

  }
}