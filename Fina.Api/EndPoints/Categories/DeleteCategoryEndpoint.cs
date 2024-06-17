using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Fina.Api.Common.Api;
using Fina.Common.Models;
using Fina.Common.Requests.Categories;
using Fina.Common.Responses;
using Fina.Common.Services;

namespace Fina.Api.EndPoints.Categories
{
    public class DeleteCategoryEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
           .WithName("Categories: Delete")
           .WithSummary("Exclui uma nova categoria")
           .WithDescription("Exclui uma nova categoria")
           .WithOrder(1)
           .Produces<Response<Category?>>();


        private static async Task<IResult> HandleAsync(
            ICategoryService service, long id)
        {
            var request = new DeleteCategoryRequest 
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var response = await service.DeleteAsync(request);
            return response.IsSuccess ? TypedResults.Ok(response) 
            : TypedResults.BadRequest(response);
        }
    }
}