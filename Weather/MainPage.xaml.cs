using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Weather.Controls;
using Weather.ViewModels.Implementations;

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
            this.BindingContext = new LocationListPageViewModel
                                  {
                                    Navigation = Navigation
                                  };

        }
    }
}
