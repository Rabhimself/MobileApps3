using RedditLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace RedditLite.Data
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
    public enum cats { HOT, NEW, RAND, TOP, CONTROV, FRONT };
    class RedditSession
    {
        private const string reddit = "http://www.reddit.com";
       

        public async Task<List<Post>> getPosts(Request r)
        {

            string cat = null;
            switch (r.cat)
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
                    cat = "/hot";
                    break;
            }
            string url = reddit + r.subreddit + cat + ".json";            
            if (r.before != null)
                url += "?before=" + r.before;
            if (r.after != null)
                url += "?after=" + r.after;



            Uri requestUri = new Uri(url);

            return parseT3Response(await doRequest(requestUri));
        }

        //[/r/subreddit]/comments/articleid.json
        public async Task<List<RedditComment>> getComments(String permalink)
        {
            Uri requestUri = new Uri(reddit + permalink+ ".json?limit=100&depth=4");

            return parseCommentTreeResponse(await doRequest(requestUri));
        }
        public async Task<List<RedditComment>> getComments(String permalink, String commentId)
        {
            Uri requestUri = new Uri(reddit + permalink +commentId +".json");

            return parseCommentTreeResponse(await doRequest(requestUri));
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
                if (!lthumbnail.Contains("http://"))
                    lthumbnail = "ms-appx://ReditLite.App/Assets/default.png";
                string lpermalink = entry.GetNamedString("permalink");
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
                    permalink = lpermalink,
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

        private List<RedditComment> parseCommentTreeResponse(String jsonString)
        {
            //the response is actually an array, the first object is post data, the second is the comment tree. Restructure the response all the way back to the viewmodel, THEN grab the comment tree.

            List<RedditComment> comments = new List<RedditComment>();
            JsonObject top = JsonValue.Parse(jsonString).GetArray().GetObjectAt(1);//this is actually an array
            JsonObject mid = top.GetNamedObject("data");
            JsonArray arr = mid.GetNamedArray("children");
            for (uint i = 0; i < arr.Count; i++)
            {
                JsonObject entry = arr.GetObjectAt(i).GetNamedObject("data");

                var redditComment = BuildComment(entry);
                comments.Add(redditComment);
            }
            return comments;
        }
        private List<RedditComment> parseRepliesTreeResponse(String jsonString)
        {
            //the response is actually an array, the first object is post data, the second is the comment tree. Restructure the response all the way back to the viewmodel, THEN grab the comment tree.

            List<RedditComment> comments = new List<RedditComment>();
            JsonObject top = JsonValue.Parse(jsonString).GetArray().GetObjectAt(1);//this is actually an array
            JsonObject mid = top.GetNamedObject("data");
            JsonArray arr = mid.GetNamedArray("children");
            for (uint i = 0; i < arr.Count; i++)
            {
                JsonObject entry = arr.GetObjectAt(i).GetNamedObject("data");

                var redditComment = BuildComment(entry);
                comments.Add(redditComment);
            }
            return comments;
        }
        private RedditComment BuildComment(JsonObject entry)
        {
            Dictionary<String, object> stuff = new Dictionary<string, object>();
            List<String> keys = new List<String> { };
            string _author_flair_css_class, _subreddit_id, _link_id, _author, _parent_id, _body, _id, _name, _body_html, _subreddit, _author_flair_text;
            bool _saved, _archived, _score_hidden, _stickied;
            double _score, _gilded, _controversiality, _downs, _created, _created_utc, _ups, _count;
            getJsonValue(entry, "author_flair_css_class", out _author_flair_css_class);
            getJsonValue(entry, "subreddit_id", out _subreddit_id);
            getJsonValue(entry, "link_id", out _link_id);
            getJsonValue(entry, "author", out _author);
            getJsonValue(entry, "parent_id", out _parent_id);
            getJsonValue(entry, "body", out _body);
            getJsonValue(entry, "id", out _id);
            getJsonValue(entry, "name", out _name);
            getJsonValue(entry, "body_html", out _body_html);
            getJsonValue(entry, "subreddit", out _subreddit);
            getJsonValue(entry, "author_flair_text", out _author_flair_text);
            getJsonValue(entry, "saved", out _saved);
            getJsonValue(entry, "out_archived", out _archived);
            getJsonValue(entry, "score_hidden", out _score_hidden);
            getJsonValue(entry, "stickied", out _stickied);
            getJsonValue(entry, "score", out _score);
            getJsonValue(entry, "gilded", out _gilded);
            getJsonValue(entry, "controversiality", out _controversiality);
            getJsonValue(entry, "downs", out _downs);
            getJsonValue(entry, "created", out _created);
            getJsonValue(entry, "created_utc", out _created_utc);
            getJsonValue(entry, "ups", out _ups);
            getJsonValue(entry, "count", out _count);

            bool success;
            IJsonValue _dump;
            success = entry.TryGetValue("replies", out _dump);
            JsonArray jsonReplies = null;
            
            //handling the inconsistent returns of reddit's api
            if (success && _dump.ValueType.Equals(JsonValueType.Object))
            {
                JsonObject _repliesObject = entry.GetNamedObject("replies");

                success = _repliesObject.TryGetValue("data", out _dump);
                if (success && _dump.ValueType.Equals(JsonValueType.Object))
                {
                    JsonObject _dataObject = _repliesObject.GetNamedObject("data");

                    success = _dataObject.TryGetValue("children", out _dump);
                    if (success && _dump.ValueType.Equals(JsonValueType.Array)){
                        jsonReplies = _dataObject.GetNamedArray("children");
                    }


                }
            }


            List<RedditComment> _replies = new List<RedditComment>();
            if (jsonReplies != null)
            {
                for (uint j = 0; j < jsonReplies.Count; j++)
                {
                    string kind = jsonReplies.GetObjectAt(j).GetNamedString("kind");
                    if (!kind.Equals("more"))
                    {
                        JsonObject reply = jsonReplies.GetObjectAt(j).GetNamedObject("data");
                        RedditComment comment = BuildComment(reply);
                        _replies.Add(comment);
                    }
                }

            }


            return new RedditComment
            {
                subreddit_id = _subreddit_id,
                link_id = _link_id,
                author = _author,
                parent_id = _parent_id,
                body = _body,
                id = _id,
                name = _name,
                author_flair_css_class = _author_flair_css_class,
                body_html = _body_html,
                subreddit = _subreddit,
                author_flair_text = _author_flair_text,
                saved = _saved,
                archived = _archived,
                score_hidden = _score_hidden,
                stickied = _stickied,
                score = _score,
                gilded = _gilded,
                controversiality = _controversiality,
                downs = _downs,
                created = _created,
                created_utc = _created_utc,
                ups = _ups,
                count = _count,
                replies = _replies
            };
        }

        private void getJsonValue(JsonObject source, string key, out bool value)
        {
            value = false;

            IJsonValue _value;
            if (source.TryGetValue(key, out _value))
            {
                JsonValueType t = _value.ValueType;
                if (t == JsonValueType.Boolean)
                    value = source.GetNamedBoolean(key);
            }






        }
        private void getJsonValue(JsonObject source, string key, out string value)
        {
            value = "";
            IJsonValue _value;


            if (source.TryGetValue(key, out _value))
            {
                JsonValueType t = _value.ValueType;
                if (t == JsonValueType.String)
                    value = source.GetNamedString(key);
            }



        }
        private void getJsonValue(JsonObject source, string key, out double value)
        {
            value = 0;

            IJsonValue _value;
            if (source.TryGetValue(key, out _value))
            {
                JsonValueType t = _value.ValueType;
                if (t == JsonValueType.Number)
                    value = source.GetNamedNumber(key);
            }

        }
    }
}
