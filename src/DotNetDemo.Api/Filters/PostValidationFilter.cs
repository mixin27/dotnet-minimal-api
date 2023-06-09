using Domain.Models;

namespace DotNetDemo.Api.Filters
{
    public class PostValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var post = context.GetArgument<Post>(1);

            if (string.IsNullOrEmpty(post.Content))
                return await Task.FromResult(Results.BadRequest("Please provide a valid post."));

            return await next(context);
        }
    }
}
