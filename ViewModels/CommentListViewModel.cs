using Lurker.Data;
using Lurker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lurker.ViewModels
{
    public class CommentListViewModel : NotificationBase
    {
        RedditCommentTree t3 = new RedditCommentTree();

        public CommentListViewModel(String subreddit, String id)
        {

            _SelectedIndex = -1;
            init(subreddit, id);

        }
        public async void init(String subreddit, String id)
        {
            RedditSession reddit = new RedditSession();
            List<RedditComment> comments = await reddit.getComments(subreddit, id);

            foreach (var comment in comments)
            { 
                var np = new CommentViewModel(comment);
                np.replies = new List<CommentViewModel>();
                foreach(var reply in comment.replies)
                {
                    var nr = new CommentViewModel(reply);
                    np.replies.Add(nr);
                }
                Comments.Add(np);
            }
        }

        ObservableCollection<CommentViewModel> _Comments= new ObservableCollection<CommentViewModel>();
        public ObservableCollection<CommentViewModel> Comments
        {
            get { return _Comments; }
            set { SetProperty(ref _Comments, value); }
        }

        int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set { if (SetProperty(ref _SelectedIndex, value)) { RaisePropertyChanged(nameof(SelectedPost)); } }
        }
        public CommentViewModel SelectedPost
        {
            get { return (_SelectedIndex >= 0) ? _Comments[_SelectedIndex] : null; }
        }

    }
}
