using SartreServer.Models;
using System.Collections.Generic;
using System.Linq;

namespace SartreServer.Repositories.MockRepositories
{
    public class MockPostRepository : IPostRepository
    {
        private Dictionary<string, Post> Posts { get; }

        public MockPostRepository(MockDataProvider dataProvider = null)
        {
            if (dataProvider == null)
            {
                Posts = new Dictionary<string, Post>();
            }
            else
            {
                Posts = dataProvider.Posts;
            }
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return Posts.Values;
        }

        public Post GetPost(string postId)
        {
            return Posts.GetValueOrDefault(postId);
        }

        public IEnumerable<Post> GetPostsOfBlog(string blogId, int page, int itemsPerPage)
        {
            return Posts.Values.Where(p => p.Blog.Id == blogId).Skip(itemsPerPage * page - itemsPerPage).Take(itemsPerPage);
        }

        public void CreatePost(Post post, string login)
        {
            Posts.Add(post.Id, post);
        }

        public void UpdatePost(Post post)
        {
            Posts[post.Id] = post;
        }

        public void DeletePost(string postId)
        {
            Posts.Remove(postId);
        }
    }
}
