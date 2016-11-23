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

        public CommentListViewModel(String permalink)
        {

            _SelectedIndex = -1;
            init(permalink);

        }
        public CommentListViewModel(String permalink, String commentId)
        {

            _SelectedIndex = -1;
            init(permalink, commentId);

        }
        public async void init(String permalink)
        {
            RedditSession reddit = new RedditSession();
            List<RedditComment> comments = await reddit.getComments(permalink);

            foreach (var comment in comments)
            {
                var np = new CommentViewModel(comment);
                np.replies = new List<CommentViewModel>();
                foreach(var reply in comment.replies)
                {
                    np.replies.Add(NestReplies(reply));
                }
                _Comments.Add(np);
            }
        }
        public async void init(String permalink, String commentId)
        {
            RedditSession reddit = new RedditSession();
            List<RedditComment> comments = await reddit.getComments(permalink, commentId);

            foreach (var comment in comments)
            {
                var np = new CommentViewModel(comment);
                np.replies = new List<CommentViewModel>();
                foreach (var reply in comment.replies)
                {
                    np.replies.Add(NestReplies(reply));
                }
                _Comments.Add(np);
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
        private CommentViewModel NestReplies(RedditComment c)
        {

            CommentViewModel ncvm = new CommentViewModel(c);
            ncvm.replies = new List<CommentViewModel>();
            foreach (var reply in c.replies)
            {              
                ncvm.replies.Add(NestReplies(reply));
            }
            return ncvm;
        }
    }
}
