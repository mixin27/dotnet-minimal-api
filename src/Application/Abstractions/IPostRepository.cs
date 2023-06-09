using Domain.Models;

namespace Application.Abstractions
{
    public interface IPostRepository
    {
        Task<ICollection<Post>> GetAllPosts();

        Task<Post> GetPostById(int postId);

        Task<Post> CreatePost(Post post);

        Task<Post> UpdatePost(String? content, int postId);

        Task DeletePost(int postId);
    }
}