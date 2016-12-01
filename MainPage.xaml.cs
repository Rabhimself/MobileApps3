using RedditLite.Data;
using RedditLite.Models;
using RedditLite.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RedditLite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int pageNum = 0;
        private String subreddit;

        public MainPage()
        {
            this.InitializeComponent();
            Request r = new Request {};
            plvm = new PostListViewModel(r);
            PreviousPageButton.Visibility = Visibility.Collapsed;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }

        }

        public PostListViewModel plvm { get; set; }

        private void CommentsClick(object sender, RoutedEventArgs e)
        {
            //var post = (PostViewModel)MainList.SelectedItems.First();
            FrameworkElement b = (FrameworkElement)e.OriginalSource;
            PostViewModel pvm = (PostViewModel)b.DataContext;
            Frame.Navigate(typeof(PostPage), pvm);
        }

        private void Subreddit_Click(object sender, RoutedEventArgs e)
        {
            PostCategory c = PostCategory.HOT;

            switch (((Button)sender).Content.ToString())
            {

                case "NEW":
                    c = PostCategory.NEW;
                    break;
                case "TOP":
                    c = PostCategory.TOP;
                    break;
                case "RISING":
                    c = PostCategory.RISING;
                    break;
                case "CONTROVERSIAL":
                    c = PostCategory.CONTROV;
                    break;
                case "GILDED":
                    c = PostCategory.GILDED;
                    break;
                case "PROMOTED":
                    c = PostCategory.PROMO;
                    break;
                case "GO":
                    subreddit = SubredditSearchBox.Text;
                    break;
            }

            Request r = new Request { subreddit = subreddit, cat = c };
            plvm = new PostListViewModel(r);
            Bindings.Update();
            pageNum = 0;
        }
        private void DoRequest()
        {

        }

        private void FollowLink(object sender, RoutedEventArgs e)
        {
            String tag = (String)((FrameworkElement)sender).Tag;
            Frame.Navigate(typeof(URLPage), tag);
        }

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }



        }

        private void NextPageClick(object sender, RoutedEventArgs e)
        {
            //this if ensures the user cannot crash the app by clicking nextpage before any previous requests are done
            //otherwise the app cannot build the request without the 'after' param
            if (plvm.Posts.Count > 0)
            {
                if (PreviousPageButton.Visibility == Visibility.Collapsed)
                {
                    PreviousPageButton.Visibility = Visibility.Visible;
                }
                pageNum++;
                Request r = new Request { after = "t3_" + plvm.Posts.Last().id };
                plvm = new PostListViewModel(r);
                Bindings.Update();
            }
        }

        private void PrevPageClick(object sender, RoutedEventArgs e)
        {
            if (plvm.Posts.Count > 0)
            {
                if (--pageNum == 0)
                    PreviousPageButton.Visibility = Visibility.Collapsed;
                Request r = new Request { before = "t3_" + plvm.Posts.First().id };
                plvm = new PostListViewModel(r);
                Bindings.Update();
            }
        }

        private void Element_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Element_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

    }

}
