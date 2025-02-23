﻿using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _dbContext;

        public BlogRepository(AppDbContext dbContext) 
        {
            // : inject AppDbContext
            _dbContext = dbContext;
        }

        public IEnumerable<Blog> GetAll()
        {
            //: Retrieve all blgs. Include Blog.User.
            return _dbContext.Blogs.Include(b => b.User);
        }

        public Blog Get(int id)
        {
            // : Retrieve the blog by id. Include Blog.User.
            return _dbContext.Blogs.Include(b => b.User)
                .FirstOrDefault(b => b.Id == id);
        }

        public Blog Add(Blog blog)
        {
            // : Add new blog
            _dbContext.Blogs.Add(blog);
            _dbContext.SaveChanges();
            return blog;
        }

        public Blog Update(Blog updatedItem)
        {
            // : update blog
            var existingItem = _dbContext.Blogs.Find(updatedItem.Id);
            if (existingItem == null) return null;
            _dbContext.Entry(existingItem)
               .CurrentValues
               .SetValues(updatedItem);
            _dbContext.Blogs.Update(existingItem);
            _dbContext.SaveChanges();
            return existingItem;
        }

        public void Remove(int id)
        {
            // : remove blog
            var currentBlog = this.Get(id);
            if (currentBlog != null)
            {
                _dbContext.Blogs.Remove(currentBlog);
                _dbContext.SaveChanges();
            }
            //else
            //{
            //    throw new Exception("Cannot remove a Blog that does not exist!");
            //}
        }
    }
}
