using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fina.Api.Common.Api;
using Fina.Common.Models;
using Fina.Common.Requests.Categories;
using Fina.Common.Responses;
using Fina.Common.Services;

namespace Fina.Api.EndPoints.Categories
{
    public class GetByIdCategoryEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                .WithName("Categories: Get Byt ID")
                .WithSummary("Recupera uma categoria")
                .WithDescription("Recupera uma categoria")
                .WithOrder(4)
                .Produces<Response<Category>?>();
            
        private static async Task<IResult> HandleAsync(
            ICategoryService service, long id)
        {
            var request = new GetCategoryByIdRequest() 
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var response = await service.GetByIdAsync(request);
            return response.IsSuccess ? TypedResults.Ok(response) 
                : TypedResults.BadRequest(response);
        }
    }
}