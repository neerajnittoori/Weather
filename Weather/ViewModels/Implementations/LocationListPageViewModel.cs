using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Core;
using Core.Models;
using Weather.Pages;
using Weather.ViewModels.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Core;

namespace Weather.ViewModels.Implementations
{
    public class LocationListPageViewModel : INotifyPropertyChanged, ILocationListPageViewModel
    {
        private ObservableCollection<Location> _locationList;

        public LocationListPageViewModel()
        {
            LocationList = GetLocationsListToSearch();
    }

        #region Properties

        public ObservableCollection<Location> LocationList
        {
            get => _locationList;
            set
            {
                _locationList = value;
                OnPropertyChanged(nameof(LocationList));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NavigateToWeatherDetailsPage
        {

          get
          {
            return new Command<Location>(async (u) => await NavigateToWeatherDetailsPageAsync(u));
          }
        }

        public INavigation Navigation { get; set; }

    #endregion

    #region Methods

    protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<Location> GetLocationsListToSearch()
        {
            return new ObservableCollection<Location>
            {
                new Location
                {
                    Name = "New York City",
                    Id = 5128581
                },
                new Location
                {
                    Name = "San Francisco",
                    Id = 5391959
                },
                new Location
                {
                    Name = "Chicago",
                    Id = 4887398
                },
                new Location
                {
                    Name = "Boston",
                    Id = 4930956
                }
            };
        }

        private async Task NavigateToWeatherDetailsPageAsync(Location selectedLocation)
        {
          if (Navigation != null)
          {
            var weatherDetailsPageViewModel = CoreInitializer.Container.Resolve<IWeatherDetailsPageViewModel>();
            weatherDetailsPageViewModel.DataTransfer = selectedLocation;
            var weatherDetailsPage = new WeatherDetailsPage
                                     {
                                       BindingContext = weatherDetailsPageViewModel
                                     };
            await Navigation.PushAsync(weatherDetailsPage);
          }
          else
          {
            throw new Exception( " ViewModel Requires a Valid Navigation");
          }
        }


    #endregion
  }
}
