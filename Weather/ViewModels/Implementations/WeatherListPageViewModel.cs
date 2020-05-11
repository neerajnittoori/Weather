using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Models;
using Xamarin.Forms;

namespace Core.ViewModels.Implementations
{
    public class WeatherListPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Location> _locationList;

        public WeatherListPageViewModel()
        {
            LocationList = GetLocationsListToSearch();
            NavigateToWeatherDetailsPage = new Command<Location>(execute: async (u) =>
            {
                await NavigateToWeatherDetailsPageAsync(u);
            });
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

        public ICommand NavigateToWeatherDetailsPage;

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
        }



        #endregion
    }
}
