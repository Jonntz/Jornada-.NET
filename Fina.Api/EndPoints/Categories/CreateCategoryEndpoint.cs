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
    public class CreateCategoryEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
           .WithName("Categories: Create")
           .WithSummary("Cria uma nova categoria")
           .WithDescription("Cria uma nova categoria")
           .WithOrder(1)
           .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(
            ICategoryService service,
            CreateCategoryRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await service.CreateAsync(request);
            return response.IsSuccess ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response) 
            : TypedResults.BadRequest(response);
        }
    }
}