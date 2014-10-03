using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.Owin.Testing;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using webapi.atom.Models;
using Xunit;

namespace webapi.atom.Tests
{
    public class TicketEventControllerIntegrationTests
    {
        /// <summary>
        /// Basic happy path. Get returns OK.
        /// </summary>
        [Fact]
        public async void GetReturns200Ok()
        {
            TestServer owinTestServer = TestServer.Create(TestHttpConfiguration.Configure);
            HttpResponseMessage response = await owinTestServer.HttpClient.GetAsync("/api/TicketEvents");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Tests that get returns any json content in the right Event structure.
        /// </summary>
        [Fact]
        public async void GetReturnsJsonContent()
        {
            TestServer owinTestServer = TestServer.Create(TestHttpConfiguration.Configure);
            HttpResponseMessage response = await owinTestServer.HttpClient.GetAsync("/api/TicketEvents");

            string content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<@Event>>(content);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Get returns any ATOM content.
        /// </summary>
        [Fact]
        public async void GetReturnsAtomContent()
        {
            TestServer owinTestServer = TestServer.Create(TestHttpConfiguration.Configure);

            using (var message = new HttpRequestMessage(HttpMethod.Get, "/api/TicketEvents"))
            {
                message.Headers.Add("accept", new[] { "application/atom+xml" });

                HttpResponseMessage response = await owinTestServer.HttpClient.SendAsync(message);

                string content = await response.Content.ReadAsStringAsync();

                Assert.NotNull(content);
            }
        }

        /// <summary>
        /// Get returns atom content and it can be deserialized.
        /// </summary>
        [Fact]
        public async void GetReturnsAtomContentCorrectFormat()
        {
            TestServer owinTestServer = TestServer.Create(TestHttpConfiguration.Configure);

            using (var message = new HttpRequestMessage(HttpMethod.Get, "/api/TicketEvents"))
            {
                message.Headers.Add("accept", new[] { "application/atom+xml" });

                HttpResponseMessage response = await owinTestServer.HttpClient.SendAsync(message);

                using (var reader = XmlReader.Create(await response.Content.ReadAsStreamAsync()))
                {
                    var formatter = new Atom10FeedFormatter();
                    formatter.ReadFrom(reader);
                    
                    Assert.NotEmpty(formatter.Feed.Items);
                }
            }
        }
    }
}
