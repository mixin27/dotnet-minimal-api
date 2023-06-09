using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _ctx;

        public PostRepository(SocialDbContext context)
        {
            _ctx = context;
        }

        public async Task<Post> CreatePost(Post post)
        {
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;
            _ctx.Posts.Add(post);

            // Store to database.
            await _ctx.SaveChangesAsync();

            return post;
        }

        public async Task DeletePost(int postId)
        {
            var post = await _ctx.Posts.FirstOrDefaultAsync(post => post.Id == postId);

            if (post == null) return;

            _ctx.Posts.Remove(post);

            await _ctx.SaveChangesAsync();
        }

        public async Task<ICollection<Post>> GetAllPosts()
        {
            return await _ctx.Posts.ToListAsync();
        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null) throw new Exception("Not founde");

            return post;
        }

        public async Task<Post> UpdatePost(string? content, int postId)
        {
            var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null) throw new Exception("Not founde");

            post.UpdatedAt = DateTime.Now;
            post.Content = content;

            await _ctx.SaveChangesAsync();

            return post;
        }
    }
}