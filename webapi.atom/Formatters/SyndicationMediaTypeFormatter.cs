using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace webapi.atom.Formatters
{
    /// <summary>
    /// From the most excellent book 
    /// http://chimera.labs.oreilly.com/books/1234000001708/ch13.html#_the_formatterparameterbinder_implementation
    /// </summary>
    public class SyndicationMediaTypeFormatter : MediaTypeFormatter
    {
        public const string Atom = "application/atom+xml";
        public const string Rss = "application/rss+xml";

        public SyndicationMediaTypeFormatter()
            : base()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(Atom)); 
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(Rss));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            var tsc = new TaskCompletionSource<AsyncVoid>();
            tsc.SetResult(default(AsyncVoid));

            var items = new List<SyndicationItem>();

            if (value is IEnumerable)
            {
                foreach (var model in (IEnumerable)value)
                {
                    var item = MapToItem(model);
                    items.Add(item);
                }
            }
            else
            {
                var item = MapToItem(value);
                items.Add(item);
            }

            var feed = new SyndicationFeed(items);

            SyndicationFeedFormatter formatter = null;
            if (content.Headers.ContentType.MediaType == Atom)
            {
                formatter = new Atom10FeedFormatter(feed);
            }
            else if (content.Headers.ContentType.MediaType == Rss)
            {
                formatter = new Rss20FeedFormatter(feed);
            }
            else
            {
                throw new Exception("Not supported media type");
            }

            using (var writer = XmlWriter.Create(writeStream))
            {
                formatter.WriteTo(writer);

                writer.Flush();
                writer.Close();
            }

            return tsc.Task;
        }

        protected SyndicationItem MapToItem(object model)
        {
            var item = new SyndicationItem();

            item.ElementExtensions.Add(model);

            return item;
        }

        private struct AsyncVoid
        {
        }
    }
}
