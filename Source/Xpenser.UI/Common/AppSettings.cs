
using System.IO;
using Xpenser.Models;

namespace Xpenser.UI
{
    public class AppSettings
    {
        public string ServiceBaseAddress { get; set; }
    }
    public class DocsWithFiles : SvcData
    {
        public Stream DocFile { get; set; }
    }
}
