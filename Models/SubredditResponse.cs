using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lurker.Models.Subreddit
{

    public class SubredditResponse
    {
        public string kind { get; set; }
        public Data data { get; set; }
    }

    public class Oembed
    {
        public string provider_url { get; set; }
        public string version { get; set; }
        public string title { get; set; }
        public double thumbnail_width { get; set; }
        public double height { get; set; }
        public double width { get; set; }
        public string html { get; set; }
        public string author_name { get; set; }
        public double thumbnail_height { get; set; }
        public string thumbnail_url { get; set; }
        public string type { get; set; }
        public string provider_name { get; set; }
        public string author_url { get; set; }
    }

    public class SecureMedia
    {
        public Oembed oembed { get; set; }
        public string type { get; set; }
    }

    public class SecureMediaEmbed
    {
        public string content { get; set; }
        public double? width { get; set; }
        public bool? scrolling { get; set; }
        public double? height { get; set; }
    }

    public class Oembed2
    {
        public string provider_url { get; set; }
        public string version { get; set; }
        public string title { get; set; }
        public double thumbnail_width { get; set; }
        public double height { get; set; }
        public double width { get; set; }
        public string html { get; set; }
        public string author_name { get; set; }
        public double thumbnail_height { get; set; }
        public string thumbnail_url { get; set; }
        public string type { get; set; }
        public string provider_name { get; set; }
        public string author_url { get; set; }
    }

    public class Media
    {
        public Oembed2 oembed { get; set; }
        public string type { get; set; }
    }

    public class MediaEmbed
    {
        public string content { get; set; }
        public double? width { get; set; }
        public bool? scrolling { get; set; }
        public double? height { get; set; }
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

    public class Source2
    {
        public string url { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public class Resolution2
    {
        public string url { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public class Gif
    {
        public Source2 source { get; set; }
        public List<Resolution2> resolutions { get; set; }
    }

    public class Source3
    {
        public string url { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public class Resolution3
    {
        public string url { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public class Mp4
    {
        public Source3 source { get; set; }
        public List<Resolution3> resolutions { get; set; }
    }

    public class Variants
    {
        public Gif gif { get; set; }
        public Mp4 mp4 { get; set; }
    }

    public class Image
    {
        public Source source { get; set; }
        public List<Resolution> resolutions { get; set; }
        public Variants variants { get; set; }
        public string id { get; set; }
    }

    public class Preview
    {
        public List<Image> images { get; set; }
    }

    public class Data2
    {
        public bool contest_mode { get; set; }
        public object banned_by { get; set; }
        public string domain { get; set; }
        public string subreddit { get; set; }
        public string selftext_html { get; set; }
        public string selftext { get; set; }
        public object likes { get; set; }
        public object suggested_sort { get; set; }
        public List<object> user_reports { get; set; }
        public SecureMedia secure_media { get; set; }
        public bool saved { get; set; }
        public string id { get; set; }
        public double gilded { get; set; }
        public SecureMediaEmbed secure_media_embed { get; set; }
        public bool clicked { get; set; }
        public object report_reasons { get; set; }
        public string author { get; set; }
        public Media media { get; set; }
        public string name { get; set; }
        public double score { get; set; }
        public object approved_by { get; set; }
        public bool over_18 { get; set; }
        public object removal_reason { get; set; }
        public bool hidden { get; set; }
        public string thumbnail { get; set; }
        public string subreddit_id { get; set; }
        public object edited { get; set; }
        public string link_flair_css_class { get; set; }
        public string author_flair_css_class { get; set; }
        public double downs { get; set; }
        public List<object> mod_reports { get; set; }
        public bool archived { get; set; }
        public MediaEmbed media_embed { get; set; }
        public bool is_self { get; set; }
        public bool hide_score { get; set; }
        public bool spoiler { get; set; }
        public string permalink { get; set; }
        public bool locked { get; set; }
        public bool stickied { get; set; }
        public double created { get; set; }
        public string url { get; set; }
        public string author_flair_text { get; set; }
        public bool quarantine { get; set; }
        public string title { get; set; }
        public double created_utc { get; set; }
        public string link_flair_text { get; set; }
        public object distinguished { get; set; }
        public double num_comments { get; set; }
        public bool visited { get; set; }
        public object num_reports { get; set; }
        public double ups { get; set; }
        public Preview preview { get; set; }
        public string post_hdouble { get; set; }
    }

    public class Child
    {
        public string kind { get; set; }
        public Data2 data { get; set; }
    }

    public class Data
    {
        public string modhash { get; set; }
        public List<Child> children { get; set; }
        public string after { get; set; }
        public object before { get; set; }
    }


}
