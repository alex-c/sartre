using SartreServer.Models;
using System.Collections.Generic;

namespace SartreServer.Repositories.MockRepositories
{
    public class MockPostRepository : IPostRepository
    {
        private Dictionary<string, Post> Posts { get; }

        public MockPostRepository()
        {
            Posts = new Dictionary<string, Post>();
        }

        public void CreatePost(Post post, string login)
        {
            Posts.Add(post.Id, post);
        }

        public void DeletePost(string postId)
        {
            Posts.Remove(postId);
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return Posts.Values;
        }

        public Post GetPost(string postId)
        {
            return Posts.GetValueOrDefault(postId);
        }

        public void UpdatePost(Post post)
        {
            Posts[post.Id] = post;
        }
    }
}
