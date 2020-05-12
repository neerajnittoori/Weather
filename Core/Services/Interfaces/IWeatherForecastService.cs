using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services.Interfaces
{
  public interface IWeatherForecastService
  {
    Task<ForecastResponse> GetFiveDayWeatherForecast(int locationId);
  }
}
