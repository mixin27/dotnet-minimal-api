using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Posts.Commands;
using Domain.Models;
using MediatR;

namespace Application.Posts.CommandHandlers
{
    public class UpdatePostHandler : IRequestHandler<UpdatePost, Post>
    {

        private readonly IPostRepository _postRepository;

        public UpdatePostHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Post> Handle(UpdatePost request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.UpdatePost(request.PostContent, request.PostId);

            return post;
        }
    }
}