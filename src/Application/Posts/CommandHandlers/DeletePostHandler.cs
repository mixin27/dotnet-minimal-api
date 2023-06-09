using Application.Abstractions;
using Application.Posts.Commands;
using MediatR;

namespace Application.Posts.CommandHandlers
{
    public class DeletePostHandler : IRequestHandler<DeletePost>
    {

        private readonly IPostRepository _postRepository;

        public DeletePostHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Handle(DeletePost request, CancellationToken cancellationToken)
        {
            await _postRepository.DeletePost(request.PostId);

            // No Unit return after v12
            // for non-async, return Task.CompletedTask;
            // for async, return;
            return;
        }
    }
}
