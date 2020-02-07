using SartreServer.Models;
using System.Collections.Generic;

namespace SartreServer.Contracts.Responses
{
    /// <summary>
    /// A response to a home page request, which can either contain a single blog or a list of blogs,
    /// depending on the platform configuration.
    /// </summary>
    public class HomePageResponse
    {
        /// <summary>
        /// The configured home page type.
        /// </summary>
        public HomePageType Type { get; set; }

        /// <summary>
        /// The name of the blogging platform.
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// The home page data, which can be either a single blog or a blog list.
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// Creates a home page response for a blog list.
        /// </summary>
        /// <param name="blogList">The blog list to attach to the response.</param>
        public HomePageResponse(string platformName, IEnumerable<Blog> blogList)
        {
            PlatformName = platformName;
            Type = HomePageType.BlogList;
            Data = blogList;
        }

        /// <summary>
        /// Creates a home page response for a single blog.
        /// </summary>
        /// <param name="blog">The blog to add to the response.</param>
        public HomePageResponse(string platformName, Blog blog)
        {
            PlatformName = platformName;
            Type = HomePageType.Blog;
            Data = blog;
        }
    }
}
