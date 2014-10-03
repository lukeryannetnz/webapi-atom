using System.Web.Http;
using Microsoft.Owin.Infrastructure;
using Owin;

namespace webapi.atom.Tests
{
    /// <summary>
    /// A test OWIN HTTP configuration.
    /// </summary>
    public class TestHttpConfiguration : HttpConfiguration
    {
        public static void Configure(IAppBuilder app)
        {
            SignatureConversions.AddConversions(app);
            var config = new TestHttpConfiguration();
            app.UseWebApi(config);
        }

        public TestHttpConfiguration()
        {
            ConfigureRoutes();
        }

        private void ConfigureRoutes()
        {
            WebApiConfig.Register(this);
        }
    }
}