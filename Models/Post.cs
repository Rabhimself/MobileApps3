using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditLite.Models
{
    public class Post
    {
        public bool contest_mode { get; set; }
        public object banned_by { get; set; }
        public string domain { get; set; }
        public string subreddit { get; set; }
        public object selftext_html { get; set; }
        public string selftext { get; set; }
        public object likes { get; set; }
        public object suggested_sort { get; set; }
        public List<object> user_reports { get; set; }
        public object secure_media { get; set; }
        public bool saved { get; set; }
        public string id { get; set; }
        public double gilded { get; set; }
        public SecureMediaEmbed secure_media_embed { get; set; }
        public bool clicked { get; set; }
        public object report_reasons { get; set; }
        public string author { get; set; }
        public object media { get; set; }
        public string name { get; set; }
        public double score { get; set; }
        public object approved_by { get; set; }
        public bool over_18 { get; set; }
        public object removal_reason { get; set; }
        public bool hidden { get; set; }
        public string thumbnail { get; set; }
        public string subreddit_id { get; set; }
        public object edited { get; set; }
        public object link_flair_css_class { get; set; }
        public object author_flair_css_class { get; set; }
        public double downs { get; set; }
        public List<object> mod_reports { get; set; }
        public bool archived { get; set; }
        public MediaEmbed media_embed { get; set; }
        public bool is_self { get; set; }
        public bool hide_score { get; set; }
        public string permalink { get; set; }
        public bool locked { get; set; }
        public bool stickied { get; set; }
        public double created { get; set; }
        public string url { get; set; }
        public object author_flair_text { get; set; }
        public bool quarantine { get; set; }
        public string title { get; set; }
        public double created_utc { get; set; }
        public object link_flair_text { get; set; }
        public object distinguished { get; set; }
        public double num_comments { get; set; }
        public bool visited { get; set; }
        public object num_reports { get; set; }
        public double ups { get; set; }
    }
    public class SecureMediaEmbed
    {
        String content { get; set; }
        double width { get; set; }
        bool scrolling { get; set; }
        double height { get; set; }
    }
    public class SecureMedia
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

    public class MediaEmbed
    {
    }

}
