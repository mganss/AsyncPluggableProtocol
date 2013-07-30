using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsyncPluggableProtocol
{
    public class ResourceProtocol : IProtocol
    {
        public string Name
        {
            get
            {
                return "rsrc";
            }
        }

        private static string DefaultNamespace = typeof(Program).Namespace;

        public Task<Stream> GetStreamAsync(string url)
        {
            var resource = DefaultNamespace + "." + url.Substring(Name.Length + 1);
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            return Task.FromResult(stream);
        }
    }
}
