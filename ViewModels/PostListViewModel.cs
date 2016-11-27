using RedditLite.Data;
using RedditLite.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditLite.ViewModels
{
    public class PostListViewModel : NotificationBase
    {
        RedditLink t3 = new RedditLink();
        //loads a specific subreddit
        public PostListViewModel(Request r)
        {
            _SelectedIndex = -1;
            init(r);
        }
        public async void init(Request r)
        {
            RedditSession reddit = new RedditSession();

            List<Post> posts = await reddit.getPosts(r);

            foreach (var post in posts)
            {

                var np = new PostViewModel(post);
                Posts.Add(np);
            }
        }

        ObservableCollection<PostViewModel> _Posts = new ObservableCollection<PostViewModel>();
        public ObservableCollection<PostViewModel> Posts
        {
            get { return _Posts; }
            set { SetProperty(ref _Posts, value); }
        }

        int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set { if (SetProperty(ref _SelectedIndex, value)) { RaisePropertyChanged(nameof(SelectedPost)); } }
        }
        public PostViewModel SelectedPost
        {
            get { return (_SelectedIndex >= 0) ? _Posts[_SelectedIndex] : null; }
        }

    }
}
