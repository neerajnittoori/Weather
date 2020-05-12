using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Weather.DisplayModels
{
  public class WeatherDetailsDisplayModel : INotifyPropertyChanged
  {

    #region Fields

    private string _forecastDateTimeString;
    private string _temperature;
    private string _windspeed;

    #endregion

    #region Properties

    public event PropertyChangedEventHandler PropertyChanged;

    public string ForecastDateTimeString
    {
      get => _forecastDateTimeString;
      set
      {
        _forecastDateTimeString = value;
        OnPropertyChanged(nameof(ForecastDateTimeString));
      }
    }

    public string Temperature
    {
      get => _temperature;
      set
      {
        _temperature = value;
        OnPropertyChanged(nameof(Temperature));
      }
    }

    public string WindSpeed
    {
      get => _windspeed;
      set
      {
        _windspeed = value;
        OnPropertyChanged(WindSpeed);
      }
    }

    #endregion

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
