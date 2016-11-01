﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lurker.Models
{
    public class RedditComment
    {
        public string subreddit_id { get; set; }
        public string link_id { get; set; }
        public string author { get; set; }
        public string parent_id { get; set; }
        public string body { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string author_flair_css_class { get; set; }
        public string body_html { get; set; }
        public string subreddit { get; set; }
        public string author_flair_text { get; set; }

        public bool saved { get; set; }
        public bool archived { get; set; }
        public bool score_hidden { get; set; }
        public bool stickied { get; set; }

        public double score { get; set; }
        public double gilded { get; set; }
        public double controversiality { get; set; }
        public double downs { get; set; }
        public double created { get; set; }
        public double created_utc { get; set; }
        public double ups { get; set; }
        public double count { get; set; }

        public List<object> mod_reports { get; set; }

        public object edited { get; set; }
        public object num_reports { get; set; }
        public object distinguished { get; set; }
        public object banned_by { get; set; }
        public object report_reasons { get; set; }
        public object removal_reason { get; set; }
        public object approved_by { get; set; }
        public object likes { get; set; }
        public List<RedditComment> replies { get; set; }
        public List<object> user_reports { get; set; }
    }
    public class Preview
    {
        public List<Image> images { get; set; }
    }
    public class Image
    {
        public Source source { get; set; }
        public List<Resolution> resolutions { get; set; }
        public Variants variants { get; set; }
        public string id { get; set; }
    }
    public class Source
    {
        public string url { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public class Resolution
    {
        public string url { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public class Variants
    {
    }
}
