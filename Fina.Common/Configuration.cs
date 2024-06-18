using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Common
{
    public static class Configuration
    {
      public const int DefaultPageNumber = 1;
      public const int DefaultPageSize = 10;  
      public const int DefaultPageStatusCode = 200;

      
      public static string BackendUrl { get; set; } = string.Empty;
      public static string FrontendUrl { get; set; } = string.Empty;
    }
}