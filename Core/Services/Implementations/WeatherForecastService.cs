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
      //Your WeatherAPI Token goes here...
      var weatherApiToken "XYZ";
      string forecastAddress = "http://api.openweathermap.org/data/2.5/forecast?appid=" + weatherApiToken + "&id=" + locationId;
      
      return await _httpHelper.GetAsync<ForecastResponse>(forecastAddress);
    }

  }
}
