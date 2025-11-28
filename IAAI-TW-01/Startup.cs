[assembly: Microsoft.Owin.OwinStartup(typeof(IAAI_TW_01.Startup))]

namespace IAAI_TW_01
{
    using System.Configuration;

    using CKSource.CKFinder.Connector.Config;
    using CKSource.CKFinder.Connector.Core.Builders;
    using CKSource.CKFinder.Connector.Core.Logs;
    using CKSource.CKFinder.Connector.Host.Owin;
    using CKSource.CKFinder.Connector.Logs.NLog;
    using CKSource.CKFinder.Connector.KeyValue.FileSystem;
    using CKSource.FileSystem.Dropbox;
    using CKSource.FileSystem.Local;
    using CKSource.FileSystem.Amazon;
    using CKSource.FileSystem.Azure;
    using CKSource.FileSystem.Ftp;

    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            LoggerManager.LoggerAdapterFactory = new NLogLoggerAdapterFactory();

            RegisterFileSystems();

            builder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                AuthenticationMode = AuthenticationMode.Active
            });

            var route = ConfigurationManager.AppSettings["ckfinderRoute"];
            builder.Map(route, SetupConnector);
        }

        private static void RegisterFileSystems()
        {
            FileSystemFactory.RegisterFileSystem<LocalStorage>();
            FileSystemFactory.RegisterFileSystem<DropboxStorage>();
            FileSystemFactory.RegisterFileSystem<AmazonStorage>();
            FileSystemFactory.RegisterFileSystem<AzureStorage>();
            FileSystemFactory.RegisterFileSystem<FtpStorage>();
        }

        private static void SetupConnector(IAppBuilder builder)
        {
            var allowedRoleMatcherTemplate = ConfigurationManager.AppSettings["ckfinderAllowedRole"];
            var authenticator = new RoleBasedAuthenticator(allowedRoleMatcherTemplate);
            
            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();
            var connector = connectorBuilder
                .LoadConfig()
                .SetAuthenticator(authenticator)
                .SetRequestConfiguration(
                    (request, config) =>
                    {
                        config.LoadConfig();

                        var defaultBackend = config.GetBackend("default");
                        var keyValueStoreProvider = new FileSystemKeyValueStoreProvider(defaultBackend);
                        config.SetKeyValueStoreProvider(keyValueStoreProvider);
                    })
                .Build(connectorFactory);

            builder.UseConnector(connector);
        }
    }
}
