using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Api.Common.Api
{
    public interface IEndPoint
    {
        static abstract void Map(IEndpointRouteBuilder app);
    }
}