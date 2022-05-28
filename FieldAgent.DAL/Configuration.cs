using Microsoft.Extensions.Configuration;

namespace FieldAgent.DAL
{
    public class Configuration
    {
        public IConfigurationRoot Config { get; private set; }
        public Configuration()
        {
            var builder = new ConfigurationBuilder();

            builder.AddUserSecrets<DBFactory>();

            Config = builder.Build();
        }
    }
}