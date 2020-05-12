using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Core.Services.Implementations;
using Core.Services.Interfaces;

namespace Core
{
  public class CoreInitializer
  {
    public static IContainer Container { get; private set; }
    public static void RegisterImplementations(IUIInitializer uiInitializer)
    {
      var builder = new ContainerBuilder();
      
      builder.RegisterType<HttpHelper>().As<IHttpHelper>().SingleInstance();
      builder.RegisterType<WeatherForecastService>().As<IWeatherForecastService>().SingleInstance();
      uiInitializer.RegisterViewModelImplementations(builder);
      Container = builder.Build();
    }
  }
}
