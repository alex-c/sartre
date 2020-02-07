using SartreServer.Models;

namespace SartreServer.Repositories.MockRepositories
{
    public class MockPlatformConfigurationRepository : IPlatformConfigurationRepository
    {
        private PlatformConfiguration PlatformConfiguration { get; set; }

        public MockPlatformConfigurationRepository(MockDataProvider dataProvider = null)
        {
            if (dataProvider == null)
            {
                PlatformConfiguration = new PlatformConfiguration()
                {
                    PlatformName = "Sartre",
                    DefaultBlogId = null
                };
            }
            else
            {

                PlatformConfiguration = new PlatformConfiguration()
                {
                    PlatformName = "Sartre",
                    DefaultBlogId = dataProvider.DefaultBlogId
                };
            }
        }

        public PlatformConfiguration GetPlatformConfiguration()
        {
            return PlatformConfiguration;
        }

        public void SetPlatformConfiguration(PlatformConfiguration platformConfiguration)
        {
            PlatformConfiguration = platformConfiguration;
        }
    }
}
