namespace SartreServer.Repositories.MockRepositories
{
    public class MockPlatformConfigurationRepository : IPlatformConfigurationRepository
    {
        private string DefaultBlogId { get; set; }

        public MockPlatformConfigurationRepository(MockDataProvider dataProvider = null)
        {
            if (dataProvider == null)
            {
                DefaultBlogId = null;
            }
            else
            {
                DefaultBlogId = dataProvider.DefaultBlogId;
            }
        }
        public string GetDefaultBlogId()
        {
            return DefaultBlogId;
        }

        public void SetDefaultBlog(string id)
        {
            DefaultBlogId = id;
        }
    }
}
