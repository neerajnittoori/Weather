using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
  public interface IHttpHelper
  {
    Task<T> GetAsync<T>(string urlToGet);
  }
}
