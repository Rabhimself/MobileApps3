using Lurker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lurker.Models.RedditLink;

namespace Lurker.ViewModels
{
    public class PostViewModel : NotificationBase<Post>
    {

        public PostViewModel(Post post = null) : base(post) { }

        public bool contest_mode
        {
            get { return This.contest_mode; }
            set { SetProperty(This.contest_mode, value, () => This.contest_mode = value); }
        }
        public object banned_by
        {
            get { return This.banned_by; }
            set { SetProperty(This.banned_by, value, () => This.banned_by = value); }
        }
        public string domain
        {
            get { return This.domain; }
            set { SetProperty(This.domain, value, () => This.domain = value); }
        }
        public string subreddit
        {
            get { return This.subreddit; }
            set { SetProperty(This.subreddit, value, () => This.subreddit = value); }
        }
        public object selftext_html
        {
            get { return This.selftext_html; }
            set { SetProperty(This.selftext_html, value, () => This.selftext_html = value); }
        }
        public string selftext
        {
            get { return This.selftext; }
            set { SetProperty(This.selftext, value, () => This.selftext = value); }
        }
        public object likes
        {
            get { return This.likes; }
            set { SetProperty(This.likes, value, () => This.likes = value); }
        }
        public object suggested_sort
        {
            get { return This.suggested_sort; }
            set { SetProperty(This.suggested_sort, value, () => This.suggested_sort = value); }
        }
        public List<object> user_reports
        {
            get { return This.user_reports; }
            set { SetProperty(This.user_reports, value, () => This.user_reports = value); }
        }
        public object secure_media
        {
            get { return This.secure_media; }
            set { SetProperty(This.secure_media, value, () => This.secure_media = value); }
        }
        public bool saved
        {
            get { return This.saved; }
            set { SetProperty(This.saved, value, () => This.saved = value); }
        }
        public string id
        {
            get { return This.id; }
            set { SetProperty(This.id, value, () => This.id = value); }
        }
        public double gilded
        {
            get { return This.gilded; }
            set { SetProperty(This.gilded, value, () => This.gilded = value); }
        }
        public SecureMediaEmbed secure_media_embed
        {
            get { return This.secure_media_embed; }
            set { SetProperty(This.secure_media_embed, value, () => This.secure_media_embed = value); }
        }
        public bool clicked
        {
            get { return This.clicked; }
            set { SetProperty(This.clicked, value, () => This.clicked = value); }
        }
        public object report_reasons
        {
            get { return This.report_reasons; }
            set { SetProperty(This.report_reasons, value, () => This.report_reasons = value); }
        }
        public string author
        {
            get { return This.author; }
            set { SetProperty(This.author, value, () => This.author = value); }
        }
        public object media
        {
            get { return This.media; }
            set { SetProperty(This.media, value, () => This.media = value); }
        }
        public string name
        {
            get { return This.name; }
            set { SetProperty(This.name, value, () => This.name = value); }
        }
        public double score
        {
            get { return This.score; }
            set { SetProperty(This.score, value, () => This.score = value); }
        }
        public object approved_by
        {
            get { return This.approved_by; }
            set { SetProperty(This.approved_by, value, () => This.approved_by = value); }
        }
        public bool over_18
        {
            get { return This.over_18; }
            set { SetProperty(This.over_18, value, () => This.over_18 = value); }
        }
        public object removal_reason
        {
            get { return This.removal_reason; }
            set { SetProperty(This.removal_reason, value, () => This.removal_reason = value); }
        }
        public bool hidden
        {
            get { return This.hidden; }
            set { SetProperty(This.hidden, value, () => This.hidden = value); }
        }
        public string thumbnail
        {
            get { return This.thumbnail; }
            set { SetProperty(This.thumbnail, value, () => This.thumbnail = value); }
        }
        public string subreddit_id
        {
            get { return This.subreddit_id; }
            set { SetProperty(This.subreddit_id, value, () => This.subreddit_id = value); }
        }
        public object edited
        {
            get { return This.edited; }
            set { SetProperty(This.edited, value, () => This.edited = value); }
        }
        public object link_flair_css_class
        {
            get { return This.link_flair_css_class; }
            set { SetProperty(This.link_flair_css_class, value, () => This.link_flair_css_class = value); }
        }
        public object author_flair_css_class
        {
            get { return This.author_flair_css_class; }
            set { SetProperty(This.author_flair_css_class, value, () => This.author_flair_css_class = value); }
        }
        public double downs
        {
            get { return This.downs; }
            set { SetProperty(This.downs, value, () => This.downs = value); }
        }
        public List<object> mod_reports
        {
            get { return This.mod_reports; }
            set { SetProperty(This.mod_reports, value, () => This.mod_reports = value); }
        }
        public bool archived
        {
            get { return This.archived; }
            set { SetProperty(This.archived, value, () => This.archived = value); }
        }
        public MediaEmbed media_embed
        {
            get { return This.media_embed; }
            set { SetProperty(This.media_embed, value, () => This.media_embed = value); }
        }
        public bool is_self
        {
            get { return This.is_self; }
            set { SetProperty(This.is_self, value, () => This.is_self = value); }
        }
        public bool hide_score
        {
            get { return This.hide_score; }
            set { SetProperty(This.hide_score, value, () => This.hide_score = value); }
        }
        public string permalink
        {
            get { return This.permalink; }
            set { SetProperty(This.permalink, value, () => This.permalink = value); }
        }
        public bool locked
        {
            get { return This.locked; }
            set { SetProperty(This.locked, value, () => This.locked = value); }
        }
        public bool stickied
        {
            get { return This.stickied; }
            set { SetProperty(This.stickied, value, () => This.stickied = value); }
        }
        public double created
        {
            get { return This.created; }
            set { SetProperty(This.created, value, () => This.created = value); }
        }
        public string url
        {
            get { return This.url; }
            set { SetProperty(This.url, value, () => This.url = value); }
        }
        public object author_flair_text
        {
            get { return This.author_flair_text; }
            set { SetProperty(This.author_flair_text, value, () => This.author_flair_text = value); }
        }
        public bool quarantine
        {
            get { return This.quarantine; }
            set { SetProperty(This.quarantine, value, () => This.quarantine = value); }
        }
        public string title
        {
            get { return This.title; }
            set { SetProperty(This.title, value, () => This.title = value); }
        }
        public double created_utc
        {
            get { return This.created_utc; }
            set { SetProperty(This.created_utc, value, () => This.created_utc = value); }
        }
        public object link_flair_text
        {
            get { return This.link_flair_text; }
            set { SetProperty(This.link_flair_text, value, () => This.link_flair_text = value); }
        }
        public object distinguished
        {
            get { return This.distinguished; }
            set { SetProperty(This.distinguished, value, () => This.distinguished = value); }
        }
        public double num_comments
        {
            get { return This.num_comments; }
            set { SetProperty(This.num_comments, value, () => This.num_comments = value); }
        }
        public bool visited
        {
            get { return This.visited; }
            set { SetProperty(This.visited, value, () => This.visited = value); }
        }
        public object num_reports
        {
            get { return This.num_reports; }
            set { SetProperty(This.num_reports, value, () => This.num_reports = value); }
        }
        public double ups
        {
            get { return This.ups; }
            set { SetProperty(This.ups, value, () => This.ups = value); }
        }


    }
}
