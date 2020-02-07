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
        /// Cached platform configuration.
        /// </summary>
        private PlatformConfiguration PlatformConfiguration { get; set; }

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

            // Get default blog ID into cache
            PlatformConfiguration = PlatformConfigurationRepository.GetPlatformConfiguration();
        }
        
        /// <summary>
        /// Gets the full platform configuration.
        /// </summary>
        /// <returns>Returns the platform configuration.</returns>
        public PlatformConfiguration GetPlatformConfiguration()
        {
            return PlatformConfiguration;
        }

        /// <summary>
        /// Sets the platform configuration.
        /// </summary>
        /// <param name="platformConfiguration">The platform configuration to set.</param>
        public void SetPlatformConfiguration(PlatformConfiguration platformConfiguration)
        {
            PlatformConfigurationRepository.SetPlatformConfiguration(platformConfiguration);
            PlatformConfiguration = platformConfiguration;
        }

        /// <summary>
        /// Gets the platform name.
        /// </summary>
        /// <returns></returns>
        public string GetPlaformName()
        {
            return PlatformConfiguration.PlatformName;
        }

        /// <summary>
        /// Sets the platform name.
        /// </summary>
        /// <param name="platformName">The name to set.</param>
        public void SetPlatformName(string platformName)
        {
            if (string.IsNullOrWhiteSpace(platformName))
            {
                throw new System.ArgumentException("The submitted platform name is not valid.", nameof(platformName));
            }

            PlatformConfiguration.PlatformName = platformName;
            PlatformConfigurationRepository.SetPlatformConfiguration(PlatformConfiguration);
        }

        /// <summary>
        /// Gets the default blog or null.
        /// </summary>
        /// <returns>Returns the default blog or null.</returns>
        /// <exception cref="BlogNotFoundException">This should not happen! Thrown if there is an inconsistent state: there is a default blog ID set, but no matching blog could be found.</exception>
        public Blog GetDefaultBlog()
        {
            if (PlatformConfiguration.DefaultBlogId == null)
            {
                return null;
            }
            else
            {
                Blog defaultBlog = BlogRepository.GetBlog(PlatformConfiguration.DefaultBlogId);
                if (defaultBlog == null)
                {
                    throw new BlogNotFoundException(PlatformConfiguration.DefaultBlogId);
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
                PlatformConfiguration.DefaultBlogId = blogId;
                PlatformConfigurationRepository.SetPlatformConfiguration(PlatformConfiguration);
            }
        }
    }
}
