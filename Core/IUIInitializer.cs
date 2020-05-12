using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Core
{
  public interface IUIInitializer
  {
    void RegisterViewModelImplementations(ContainerBuilder builder);
  }
}
