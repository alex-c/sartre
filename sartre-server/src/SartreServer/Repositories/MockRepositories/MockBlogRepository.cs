using SartreServer.Models;
using System.Collections.Generic;

namespace SartreServer.Repositories.MockRepositories
{
    public class MockBlogRepository : IBlogRepository, IReadOnlyBlogRepository
    {
        private Dictionary<string, Blog> Blogs { get; }

        public MockBlogRepository(MockDataProvider dataProvider = null)
        {
            if (dataProvider == null)
            {
                Blogs = new Dictionary<string, Blog>();
            }
            else
            {
                Blogs = dataProvider.Blogs;
            }
        }

        public void CreateBlog(Blog blog)
        {
            Blogs.Add(blog.Id, blog);
        }

        public void DeleteBlog(string blogId)
        {
            Blogs.Remove(blogId);
        }

        public IEnumerable<Blog> GetAllBlogs()
        {
            return Blogs.Values;
        }

        public Blog GetBlog(string blogId)
        {
            return Blogs.GetValueOrDefault(blogId);
        }

        public void UpdateBlog(Blog blog)
        {
            Blogs[blog.Id] = blog;
        }
    }
}
