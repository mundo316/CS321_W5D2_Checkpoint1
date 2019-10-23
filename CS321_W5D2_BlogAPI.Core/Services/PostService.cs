using System;
using System.Collections.Generic;
using CS321_W5D2_BlogAPI.Core.Models;

namespace CS321_W5D2_BlogAPI.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUserService _userService;

        public PostService(IPostRepository postRepository, IBlogRepository blogRepository, IUserService userService)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _userService = userService;
        }

        public Post Add(Post newPost)
        {
            // : Prevent users from adding to a blog that isn't theirs
            //     Use the _userService to get the current users id.
            //     You may have to retrieve the blog in order to check user id
            // : assign the current date to DatePublished
            var currentUserId = _userService.CurrentUserId;
            var blog = _blogRepository.Get(newPost.BlogId);
            if (currentUserId == blog.UserId)
            {
                newPost.DatePublished = DateTime.Now;
                return _postRepository.Add(newPost);
            }
            else
            {
                throw new ApplicationException("This is not your blog!");
            }

        }

        public Post Get(int id)
        {
            return _postRepository.Get(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }
        
        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            return _postRepository.GetBlogPosts(blogId);
        }

        public void Remove(int id)
        {
            var post = this.Get(id);
            // : prevent user from deleting from a blog that isn't theirs
            var currentId = _userService.CurrentUserId;
            var blog = _blogRepository.Get(post.BlogId);
            if (currentId == blog.UserId)
            {
                _postRepository.Remove(id);
            }
            else
            {
                throw new ApplicationException("You do not have permission to change this blog!");
            }
            _postRepository.Remove(id);
        }

        public Post Update(Post updatedPost)
        {
            // : prevent user from updating a blog that isn't theirs
            var currentId = _userService.CurrentUserId;
            var blog = _blogRepository.Get(updatedPost.BlogId);
            if (currentId == blog.UserId)
            {
                return _postRepository.Update(updatedPost);
            }
            else
            {
                throw new ApplicationException("You do not have permission to change this blog!");
            }
            return _postRepository.Update(updatedPost);
        }

    }
}
