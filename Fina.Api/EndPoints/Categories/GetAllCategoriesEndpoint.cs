using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fina.Api.Common.Api;

namespace Fina.Api.EndPoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndPoint
    {
        public static void Map(IEndPointRouterBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithName("Categories: Get All")
                .WithSummary("Recupera todas as categorias")
                .WithDescription("Recupera todas as categorias")
                .WithOrder(5)
                .Produces<PagedResponse<List<Category>?>>();

        
        private static async Task<IResult> HandleAsync(
        ICategoryService service,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = ApiConfiguration.UserId,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await service.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    }
}