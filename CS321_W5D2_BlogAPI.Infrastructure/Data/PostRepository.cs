using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _dbContext;

        public PostRepository(AppDbContext dbContext) 
        {
            dbContext = _dbContext;
        }

        public Post Get(int id)
        {
            // : Implement Get(id). Include related Blog and Blog.User
            return _dbContext.Posts.Include(p => p.Blog)
                .ThenInclude(y => y.User)
                .FirstOrDefault(p => p.Id == id);

        }

        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            // : Implement GetBlogPosts, return all posts for given blog id

            // : Include related Blog and AppUser
            return _dbContext.Posts.Include(p => p.Blog)
                    .ThenInclude(b => b.User)
                    .Where(p => p.BlogId == blogId);

        }

        public Post Add(Post Post)
        {
            // : add Post
            _dbContext.Posts.Add(Post);
            _dbContext.SaveChanges();
            return Post;
        }

        public Post Update(Post updatedPost)
        {
            // : update Post
            var currentPost = _dbContext.Posts.Find(updatedPost.Id);
            if (currentPost == null) return null;
            _dbContext.Entry(currentPost)
               .CurrentValues
               .SetValues(updatedPost);
            _dbContext.Posts.Update(updatedPost);
            _dbContext.SaveChanges();
            return currentPost;
        }

        public IEnumerable<Post> GetAll()
        {
            // : get all posts
            return _dbContext.Posts.Include(b => b.Blog)
                .ThenInclude(b => b.User);
        }

        public void Remove(int id)
        {
            // : remove Post
            var currentPost = this.Get(id);
            if (currentPost != null)
            {
                _dbContext.Posts.Remove(currentPost);
                _dbContext.SaveChanges();
            }
        }

    }
}
