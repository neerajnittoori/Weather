using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Core;
using Weather.ViewModels.Implementations;
using Weather.ViewModels.Interfaces;

namespace Weather
{
  public class UIInitializer : IUIInitializer
  {
    public void RegisterViewModelImplementations(ContainerBuilder builder)
    {
      builder.RegisterType<LocationListPageViewModel>().As<ILocationListPageViewModel>().SingleInstance();
      builder.RegisterType<WeatherDetailsPageViewModel>().As<IWeatherDetailsPageViewModel>().SingleInstance();
    }
  }
}
