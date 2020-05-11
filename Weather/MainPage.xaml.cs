using System.Collections.Generic;
using System.ComponentModel;
using Core.ViewModels.Implementations;
using Xamarin.Forms;
using Weather.Controls;
namespace Weather
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
          
            InitializeComponent();
            this.BindingContext = new WeatherListPageViewModel();

        }

        void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {

        }
    }
}
