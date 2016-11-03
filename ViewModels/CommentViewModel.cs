using Lurker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lurker.ViewModels
{
    public class CommentViewModel : NotificationBase<RedditComment>
    {
        public CommentViewModel(RedditComment comment = null) : base(comment) { }

        public string subreddit_id
        {
            get { return This.subreddit_id; }
            set { SetProperty(This.subreddit_id, value, () => This.subreddit_id = value); }
        }
        public string link_id
        {
            get { return This.link_id; }
            set { SetProperty(This.link_id, value, () => This.link_id = value); }
        }
        public string author
        {
            get { return This.author; }
            set { SetProperty(This.author, value, () => This.author = value); }
        }
        public string parent_id
        {
            get { return This.parent_id; }
            set { SetProperty(This.parent_id, value, () => This.parent_id = value); }
        }
        public string body
        {
            get { return This.body; }
            set { SetProperty(This.body, value, () => This.body = value); }
        }
        public string id
        {
            get { return This.id; }
            set { SetProperty(This.id, value, () => This.id = value); }
        }
        public string name
        {
            get { return This.name; }
            set { SetProperty(This.name, value, () => This.name = value); }
        }
        public string author_flair_css_class
        {
            get { return This.author_flair_css_class; }
            set { SetProperty(This.author_flair_css_class, value, () => This.author_flair_css_class = value); }
        }
        public string body_html
        {
            get { return This.body_html; }
            set { SetProperty(This.body_html, value, () => This.body_html = value); }
        }
        public string subreddit
        {
            get { return This.subreddit; }
            set { SetProperty(This.subreddit, value, () => This.subreddit = value); }
        }
        public string author_flair_text
        {
            get { return This.author_flair_text; }
            set { SetProperty(This.author_flair_text, value, () => This.author_flair_text = value); }
        }

        public bool saved
        {
            get { return This.saved; }
            set { SetProperty(This.saved, value, () => This.saved = value); }
        }
        public bool archived
        {
            get { return This.archived; }
            set { SetProperty(This.archived, value, () => This.archived = value); }
        }
        public bool score_hidden
        {
            get { return This.score_hidden; }
            set { SetProperty(This.score_hidden, value, () => This.score_hidden = value); }
        }
        public bool stickied
        {
            get { return This.stickied; }
            set { SetProperty(This.stickied, value, () => This.stickied = value); }
        }
        public double score
        {
            get { return This.score; }
            set { SetProperty(This.score, value, () => This.score = value); }
        }
        public double gilded
        {
            get { return This.gilded; }
            set { SetProperty(This.gilded, value, () => This.gilded = value); }
        }
        public double controversiality
        {
            get { return This.controversiality; }
            set { SetProperty(This.controversiality, value, () => This.controversiality = value); }
        }
        public double downs
        {
            get { return This.downs; }
            set { SetProperty(This.downs, value, () => This.downs = value); }
        }
        public double created
        {
            get { return This.created; }
            set { SetProperty(This.created, value, () => This.created = value); }
        }
        public double created_utc
        {
            get { return This.created_utc; }
            set { SetProperty(This.created_utc, value, () => This.created_utc = value); }
        }
        public double ups
        {
            get { return This.ups; }
            set { SetProperty(This.ups, value, () => This.ups = value); }
        }
        public double count
        {
            get { return This.count; }
            set { SetProperty(This.count, value, () => This.count = value); }
        }
        //public List<RedditComment> rawReplies
        //{
        //    get { return This.replies; }
        //    set { SetProperty(This.replies, value, () => This.replies = value); }
        //}

        public List<CommentViewModel> replies{ get; set; }
        //public object edited { get; set; }
        //public object num_reports { get; set; }
        //public object distinguished { get; set; }
        //public object banned_by { get; set; }
        //public object report_reasons { get; set; }
        //public object removal_reason { get; set; }
        //public object approved_by { get; set; }
        //public object likes { get; set; }

        //public List<object> user_reports { get; set; }
    }
}
