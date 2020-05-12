using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Models;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using Xamarin.Forms;
using Weather.DisplayModels;
using Weather.ViewModels.Interfaces;

namespace Weather.ViewModels.Implementations
{
  public class WeatherDetailsPageViewModel : INotifyPropertyChanged, IWeatherDetailsPageViewModel
  {
    private Location _location;
    private ObservableCollection<WeatherDetailsDisplayModel> _weatherDetailsList;
    private string _title;
    private bool _isRefreshing;
    private bool _showErrorMessage;
    private IWeatherForecastService _weatherForecastService;
    #region Properties

    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand LoadData => new Command(async () => { await LoadDataAsync();});

    public ObservableCollection<WeatherDetailsDisplayModel> WeatherDetailsList
    {
      get => _weatherDetailsList;
      set
      {
        _weatherDetailsList = value;
        OnPropertyChanged(nameof(WeatherDetailsList));
      }
    }

    public string Title
    {
      get => _title;
      set
      {
        _title = value;
        OnPropertyChanged(nameof(Title));
      }
    }

    public bool IsRefreshing
    {
      get => _isRefreshing;
      set
      {
        _isRefreshing = value;
        OnPropertyChanged(nameof(IsRefreshing));
      }
    }

    public bool ShowErrorMessage
    {
      get => _showErrorMessage;
      set
      {
        _showErrorMessage = value;
        OnPropertyChanged(nameof(ShowErrorMessage));
      }
    }

    public object DataTransfer
    {
      set
      {
        if (value is Location locationToFetchForecast)
        {
          _location = locationToFetchForecast;
          Title = $"{locationToFetchForecast.Name} Forecast";
        }
      }
    }

    #endregion

    #region Constructor

    public WeatherDetailsPageViewModel(IWeatherForecastService weatherForecastService)
    {
      _weatherForecastService = weatherForecastService;
    }

    //public WeatherDetailsPageViewModel(Location location)
    //{
    //  _location = location;
    //  WeatherDetailsList = new ObservableCollection<WeatherDetailsDisplayModel>();
    //  Title = $"{location.Name} Forecast";
    //}

    #endregion

    #region Methods

    private async Task LoadDataAsync()
    {
      try
      {
        IsRefreshing = true;
        ShowErrorMessage = false;
        var forecastResponse = await _weatherForecastService.GetFiveDayWeatherForecast(_location.Id);
        BuildWeatherDetailsDisplayModelList(forecastResponse);
      }
      catch (Exception e)
      { 
        Console.WriteLine(e);
        ShowErrorMessage = true;
      }
      finally
      {
        IsRefreshing = false;
      }
    }

    private void BuildWeatherDetailsDisplayModelList(ForecastResponse response)
    {
      //Just some of the forecast details
      var weatherDetailsDisplayList = response.ForecastList.Select(x => new WeatherDetailsDisplayModel
                                                             {
                                                               ForecastDateTimeString = x.ForecastDateTime.ToString("D"),
                                                               Temperature = $"Temperature - {x.ForecastVariable.Temp:F} Kelvin",
                                                               WindSpeed = $"Wind Speed - {x.Wind.Speed} Mps"
                                                             }).ToList();

      WeatherDetailsList = new ObservableCollection<WeatherDetailsDisplayModel>(weatherDetailsDisplayList);
    }


    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

  }
}
