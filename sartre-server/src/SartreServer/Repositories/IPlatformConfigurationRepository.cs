using SartreServer.Models;

namespace SartreServer.Repositories
{
    /// <summary>
    /// A repository for the Sartre platform configuration.
    /// </summary>
    public interface IPlatformConfigurationRepository
    {
        /// <summary>
        /// Gets the Sartre platform configuration.
        /// </summary>
        /// <returns>Returns the configuration.</returns>
        PlatformConfiguration GetPlatformConfiguration();

        /// <summary>
        /// Sets the Sartre platform configuration.
        /// </summary>
        /// <param name="platformConfiguration">The configuration to save.</param>
        void SetPlatformConfiguration(PlatformConfiguration platformConfiguration);
    }
}
