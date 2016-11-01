using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lurker.Models
{
    public class RedditCommentTree
    {
        public class MediaEmbed
        {
        }

        public class SecureMediaEmbed
        {
        }
        public class Child
        {
            public string kind { get; set; }
            public RedditComment data { get; set; }
        }

        public class Data
        {
            public string modhash { get; set; }
            public List<Child> children { get; set; }
            public object after { get; set; }
            public object before { get; set; }
        }

        public class RootObject
        {
            public string kind { get; set; }
            public Data data { get; set; }
        }
    }
}
