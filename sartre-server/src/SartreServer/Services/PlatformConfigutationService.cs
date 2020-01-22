using Microsoft.Extensions.Logging;
using SartreServer.Models;
using SartreServer.Repositories;
using SartreServer.Services.Exceptions;

namespace SartreServer.Services
{
    /// <summary>
    /// Provides information and administration of the Sartre platform configuration.
    /// </summary>
    public class PlatformConfigutationService
    {
        /// <summary>
        /// The logger for local logging.
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Provides access to the Sartre platform configuration.
        /// </summary>
        private IPlatformConfigurationRepository PlatformConfigurationRepository { get; }

        /// <summary>
        /// Provides access to blog information.
        /// </summary>
        private IReadOnlyBlogRepository BlogRepository { get; }

        /// <summary>
        /// Sets up the service.
        /// </summary>
        /// <param name="loggerFactroy">Logger factory to create a logger from.</param>
        /// <param name="platformConfigurationRepository">Repository for access to the platform configuration.</param>
        /// <param name="blogRepository">Repository of blog information.</param>
        public PlatformConfigutationService(ILoggerFactory loggerFactroy, IPlatformConfigurationRepository platformConfigurationRepository, IReadOnlyBlogRepository blogRepository)
        {
            Logger = loggerFactroy.CreateLogger<AuthService>();
            PlatformConfigurationRepository = platformConfigurationRepository;
            BlogRepository = blogRepository;
        }

        /// <summary>
        /// Gets the default blog ID or null.
        /// </summary>
        /// <returns>Returns the default blog ID or null.</returns>
        public string GetDefaultBlogId()
        {
            return PlatformConfigurationRepository.GetDefaultBlogId();
        }

        /// <summary>
        /// Gets the default blog or null.
        /// </summary>
        /// <returns>Returns the default blog or null.</returns>
        /// <exception cref="BlogNotFoundException">This should not happen! Thrown if there is an inconsistent state: there is a default blog ID set, but no matching blog could be found.</exception>
        public Blog GetDefaultBlog()
        {
            string defaultBlogId = GetDefaultBlogId();

            if (defaultBlogId == null)
            {
                return null;
            }
            else
            {
                Blog defaultBlog = BlogRepository.GetBlog(defaultBlogId);
                if (defaultBlog == null)
                {
                    throw new BlogNotFoundException(defaultBlogId);
                }
                return defaultBlog;
            }
        }

        /// <summary>
        /// Sets the default blog.
        /// </summary>
        /// <param name="blogId">ID of the blog to set as default blog. Can be null!</param>
        /// <exception cref="BlogNotFoundException">The submitted ID is not a valid blog ID.</exception>
        public void SetDefaultBlog(string blogId)
        {
            if (string.IsNullOrWhiteSpace(blogId))
            {
                blogId = null;
            }

            if (blogId != null && BlogRepository.GetBlog(blogId) == null)
            {
                throw new BlogNotFoundException(blogId);
            }
            else
            {
                PlatformConfigurationRepository.SetDefaultBlog(blogId);
            }
        }
    }
}
