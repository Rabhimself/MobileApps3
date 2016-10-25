using Lurker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Lurker.Data
{
//    /by_id/names
//    /comments/article
//    /controversial
//    /duplicates/article
//    /hot
//    /new
//    /random
//    /top
//    /sort
    class RedditSession
    {
        private const string reddit = "http://www.reddit.com/";
        public enum cats { HOT,NEW,RAND,TOP,CONTROV,FRONT };
       
        public async Task<List<Post>> getPosts(String subreddit , cats g)
        {
            
            string cat = null;
            switch (g)
            {
                case cats.FRONT:
                    cat = "/";
                    break;
                case cats.HOT:
                    cat = "/hot";
                    break;
                case cats.NEW:
                    cat = "/new";
                    break;
                case cats.RAND:
                    cat = "/random";
                    break;
                case cats.TOP:
                    cat = "/top";
                    break;
                case cats.CONTROV:
                    cat = "/controversial";
                    break;
                default:
                    break;
            }
            Uri requestUri = new Uri(reddit+subreddit+cat+".json");

            return parseT3Response(await doRequest(requestUri));
        }
        private async Task<String> doRequest(Uri requestUri)
        {
            //Create an HTTP client object
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            //Add a user-agent header to the GET request. 
            var headers = httpClient.DefaultRequestHeaders;

            //The safe way to add a header value is to use the TryParseAdd method and verify the return value is true,
            //especially if the header value is coming from user input.
            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";
            var jsonString = "";
            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                jsonString = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
            return jsonString;
        }

        private List<Post> parseT3Response(String jsonString)
        {
            List<Post> posts = new List<Post>();
            JsonObject top = JsonValue.Parse(jsonString).GetObject();
            JsonArray arr = top.GetNamedObject("data").GetNamedArray("children");
            for (uint i = 0; i < arr.Count; i++)
            {
                JsonObject entry = arr.GetObjectAt(i).GetNamedObject("data");
                bool lcontest_mode = entry.GetNamedBoolean("contest_mode");
                bool larchived = entry.GetNamedBoolean("archived");
                bool lis_self = entry.GetNamedBoolean("is_self");
                bool lhide_score = entry.GetNamedBoolean("hide_score");
                bool llocked = entry.GetNamedBoolean("locked");
                bool lstickied = entry.GetNamedBoolean("stickied");
                bool lvisited = entry.GetNamedBoolean("visited");
                bool lclicked = entry.GetNamedBoolean("clicked");
                bool lsaved = entry.GetNamedBoolean("saved");
                bool lhidden = entry.GetNamedBoolean("hidden");
                bool lquarantine = entry.GetNamedBoolean("quarantine");
                bool lover_18 = entry.GetNamedBoolean("over_18");
                string ldomain = entry.GetNamedString("domain");
                string lsubreddit = entry.GetNamedString("subreddit");
                string lselftext = entry.GetNamedString("selftext");
                string lid = entry.GetNamedString("id");
                string lauthor = entry.GetNamedString("author");
                string lname = entry.GetNamedString("name");
                string lurl = entry.GetNamedString("url");
                string lsubreddit_id = entry.GetNamedString("subreddit_id");
                string ltitle = entry.GetNamedString("title");
                string lthumbnail = entry.GetNamedString("thumbnail");
                double ldowns = entry.GetNamedNumber("downs");
                double lnum_comments = entry.GetNamedNumber("num_comments");
                double lscore = entry.GetNamedNumber("score");
                double lups = entry.GetNamedNumber("ups");
                double lgilded = entry.GetNamedNumber("gilded");
                double lcreated_utc = entry.GetNamedNumber("created_utc");
                double lcreated = entry.GetNamedNumber("created");
                //          object llikes = entry.GetNamedObject("likes");
                //String lsuggested_sort = entry.GetNamedString("suggested_sort");
                // object lsecure_media = entry.GetNamedObject("secure_media");
                // object lselftext_html = entry.GetNamedObject("selftext_html");
                //object lmedia = entry.GetNamedObject("media");
                //object lnum_reports = entry.GetNamedObject("num_reports");
                //object llink_flair_text = entry.GetNamedObject("link_flair_text");
                //object ldistinguished = entry.GetNamedObject("distinguished");
                //object ledited = entry.GetNamedObject("edited");
                //object llink_flair_css_class = entry.GetNamedObject("link_flair_css_class");
                //object lauthor_flair_css_class = entry.GetNamedObject("author_flair_css_class");
                //object lauthor_flair_text = entry.GetNamedObject("author_flair_text");
                //object lapproved_by = entry.GetNamedObject("approved_by");
                //object lreport_reasons = entry.GetNamedObject("report_reasons");
                //object lremoval_reason = entry.GetNamedObject("removal_reason");
                var post = new Post
                {
                    contest_mode = lcontest_mode,
                    archived = larchived,
                    is_self = lis_self,
                    hide_score = lhide_score,
                    locked = llocked,
                    stickied = lstickied,
                    visited = lvisited,
                    clicked = lclicked,
                    saved = lsaved,
                    hidden = lhidden,
                    quarantine = lquarantine,
                    over_18 = lover_18,
                    domain = ldomain,
                    subreddit = lsubreddit,
                    selftext = lselftext,
                    id = lid,
                    author = lauthor,
                    name = lname,
                    url = lurl,
                    subreddit_id = lsubreddit_id,
                    title = ltitle,
                    thumbnail = lthumbnail,
                    downs = ldowns,
                    num_comments = lnum_comments,
                    score = lscore,
                    ups = lups,
                    gilded = lgilded,
                    created_utc = lcreated_utc,
                    created = lcreated,
                    //                    likes = llikes,
                    //suggested_sort = lsuggested_sort
                    //secure_media = lsecure_media,
                    //selftext_html = lselftext_html,
                    //media = lmedia,
                    //num_reports = lnum_reports,
                    //link_flair_text = llink_flair_text,
                    //distinguished = ldistinguished,
                    //edited = ledited,
                    //link_flair_css_class = llink_flair_css_class,
                    //author_flair_css_class = lauthor_flair_css_class,
                    //author_flair_text = lauthor_flair_text,
                    //approved_by = lapproved_by,
                    //report_reasons = lreport_reasons,
                    //removal_reason = lremoval_reason
                };
                posts.Add(post);
            }
            return posts;
        }
    }
}
