using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditLite.Data
{
    public class Request
    {
        public String subreddit { get; set; }
        public String before { get; set; }
        public String after { get; set; }
        public String limit { get; set; }
        public cats cat { get; set; }
    }
}
