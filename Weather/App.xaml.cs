using System;
using Autofac;
using Core;
using Weather.ViewModels.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Weather
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var locationListPageViewModel = CoreInitializer.Container.Resolve<ILocationListPageViewModel>();
            
            //MainPage is the location list page
            var mainPage = new MainPage();
            mainPage.BindingContext = locationListPageViewModel;
            var navPage = new NavigationPage(mainPage);
            locationListPageViewModel.Navigation = navPage.Navigation;
            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
