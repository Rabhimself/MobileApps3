using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Lurker.Models
{
    public class RedditLink
    {
    
        public RedditLink()
        {           
        }

        public class Media
        {
            String type { get; set; }
            public class oembed
            {
                String provider_url { get; set; }
                String description { get; set; }
                String title { get; set; }
                double thumbnail_width { get; set; }
                double height { get; set; }
                double width { get; set; }
                String html { get; set; }
                String version { get; set; }
                String provider_name { get; set; }
                String thumbnail_url { get; set; }
                String type { get; set; }
                double thumbnail_height { get; set; }
            }
        }

        
        public class Child
        {
            public string kind { get; set; }
            public Post data { get; set; }
        }

        public class Data
        {
            public string modhash { get; set; }
            public List<Child> children { get; set; }
            public string after { get; set; }
            public object before { get; set; }
        }

        public class entryObject
        {
            public string kind { get; set; }
            public Data data { get; set; }
        }

        

        
    }
}