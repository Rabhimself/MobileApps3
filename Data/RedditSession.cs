﻿using RedditLite.Models;
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
    public enum PostCategory { HOT, NEW, RAND, TOP, CONTROV, FRONT, RISING };
    static class PostCategoryExtensions
    {
        public static String getCategory(this PostCategory cat)
        {
            
            switch (cat)
            {
                case PostCategory.FRONT:
                    return "/";
                case PostCategory.HOT:
                    return "/hot";
                case PostCategory.NEW:
                    return "/new";
                case PostCategory.RAND:
                    return "/random";
                case PostCategory.RISING:
                    return "/rising";
                case PostCategory.TOP:
                    return "/top";
                case PostCategory.CONTROV:
                    return "/controversial";
                default:
                    return "/hot";
            }
        }
    }

    class RedditSession
    {
        private const string reddit = "http://www.reddit.com";
       

        public async Task<List<Post>> getPosts(Request r)
        {
            StringBuilder url = new StringBuilder();
            url.Append(reddit);
            if (r.subreddit != null)
                url.Append("/r/" + r.subreddit);

            url.Append(r.cat.getCategory() + ".json");

            if (r.before != null)
                url.Append("?before=" + r.before);
            if (r.after != null)
                url.Append("?after=" + r.after);

            Uri requestUri = new Uri(url.ToString());

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
            if (jsonString.Equals(""))
                return posts;
            JsonObject top = JsonValue.Parse(jsonString).GetObject();
            JsonArray arr = top.GetNamedObject("data").GetNamedArray("children");
            for (uint i = 0; i < arr.Count; i++)
            {
                JsonObject entry = arr.GetObjectAt(i).GetNamedObject("data");
                bool lover_18 = entry.GetNamedBoolean("over_18");
                string lsubreddit = entry.GetNamedString("subreddit");
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
                double lnum_comments = entry.GetNamedNumber("num_comments");
                double lscore = entry.GetNamedNumber("score");
                double lups = entry.GetNamedNumber("ups");
                double lcreated_utc = entry.GetNamedNumber("created_utc");
                double lcreated = entry.GetNamedNumber("created");
                var post = new Post
                {
                    permalink = lpermalink,
                    over_18 = lover_18,
                    subreddit = lsubreddit,
                    id = lid,
                    author = lauthor,
                    name = lname,
                    url = lurl,
                    subreddit_id = lsubreddit_id,
                    title = ltitle,
                    thumbnail = lthumbnail,
                    num_comments = lnum_comments,
                    score = lscore,
                    ups = lups,
                    created_utc = lcreated_utc,
                    created = lcreated,
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
