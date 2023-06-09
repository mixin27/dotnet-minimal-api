using Application.Posts.Commands;
using Application.Posts.Queries;
using Domain.Models;
using DotNetDemo.Api.Abstractions;
using DotNetDemo.Api.Filters;
using MediatR;

namespace DotNetDemo.Api.EndpointDefinitions
{
    public class PostEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var posts = app.MapGroup("/api/posts");

            posts.MapGet("/", GetAllPosts);
            posts.MapPost("/", CreatePost)
                .AddEndpointFilter<PostValidationFilter>();
            posts.MapGet("/{id}", GetPostById)
                .WithName("GetPostById");
            posts.MapPut("/{id}", UpdatePost)
                .AddEndpointFilter<PostValidationFilter>();
            posts.MapDelete("/{id}", DeletePost);
        }

        private async Task<IResult> GetPostById(IMediator mediator, int id)
        {
            var command = new GetPostById { PostId = id };
            var post = await mediator.Send(command);
            return TypedResults.Ok(post);
        }

        // TODO: create CreatePostDTO model instead of Post
        private async Task<IResult> CreatePost(IMediator mediator, Post post)
        {
            var command = new CreatePost { PostContent = post.Content };
            var createdPost = await mediator.Send(command);
            return Results.CreatedAtRoute("GetPostById", new { createdPost.Id }, createdPost);
        }

        private async Task<IResult> GetAllPosts(IMediator mediator)
        {
            var command = new GetAllPosts();
            var posts = await mediator.Send(command);
            return TypedResults.Ok(posts);
        }

        // TODO: create UpdatePostDTO model instead of Post
        private async Task<IResult> UpdatePost(IMediator mediator, Post post, int id)
        {
            var command = new UpdatePost { PostId = id, PostContent = post.Content };
            var updatedPost = await mediator.Send(command);
            return Results.Ok(updatedPost);
        }

        private async Task<IResult> DeletePost(IMediator mediator, int id)
        {
            var command = new DeletePost { PostId = id };
            await mediator.Send(command);
            return TypedResults.NoContent();
        }
    }
}
