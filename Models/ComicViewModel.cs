using System;

namespace nicasource_netcore.Models
{
    public class ComicViewModel
    {
        public ComicModel Comic { get; set; }
        public string Previous { get; set; }
        public string Next { get; set; }
    }
}
