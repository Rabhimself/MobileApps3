using Lurker.Data;
using Lurker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lurker.ViewModels
{
    public class PostListViewModel : NotificationBase
    {
        RedditLink t3 = new RedditLink();

        public PostListViewModel()
        {
             
            _SelectedIndex = -1;
            // Use the model to return a task?
            init();
            
        }
        public async void init()
        {
            RedditSession reddit = new RedditSession();
            List<Post> posts = await reddit.getPosts("",RedditSession.cats.FRONT);

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
