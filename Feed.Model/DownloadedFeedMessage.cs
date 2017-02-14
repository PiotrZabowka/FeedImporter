using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    public interface IMessage
    {
        string Type { get; }
        int Version { get; }
    }

    public class DownloadedFeedMessage : IMessage
    {
        public Guid LocationId { get; set; }
        public string Type { get; set; }
        public int Version { get; set; }
        public byte[] RawData { get; set; }
    }


    public class ParsedFeedLineMessage: IMessage
    {
        public Guid LocationId { get; set; }
        public string Type { get; set; }
        public int Version { get; set; }

        public IDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
    }

}