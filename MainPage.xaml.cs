﻿using Lurker.Data;
using Lurker.Models;
using Lurker.ViewModels;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lurker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private String firstPage;

        public MainPage()
        {
            this.InitializeComponent();
            Request r = new Request {subreddit=""};
            plvm = new PostListViewModel(r);
            PreviousPageButton.Visibility = Visibility.Collapsed;
            MainList.SelectionChanged += MainList_SelectionChanged;
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

        private void MainList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PostViewModel stuff = (PostViewModel)e.AddedItems.First();           
        }

        public PostListViewModel plvm { get; set; }

        private void CommentsClick(object sender, RoutedEventArgs e)
        {
            //var post = (PostViewModel)MainList.SelectedItems.First();
            Button b = (Button)e.OriginalSource;
            PostViewModel pvm = (PostViewModel)b.DataContext;
            Frame.Navigate(typeof(PostPage), pvm);
        }

        private void Subreddit_Click(object sender, RoutedEventArgs e)
        {
            Request r = new Request { subreddit = "/r/"+SubredditSearchBox.Text};
            plvm = new PostListViewModel(r);
            Bindings.Update();
        }

        private void FollowLink(object sender, RoutedEventArgs e)
        {
            String tag = (String)((Button)sender).Tag;
            Frame.Navigate(typeof(URLPage), tag);
        }

        private void App_BackRequested(object sender,
    Windows.UI.Core.BackRequestedEventArgs e)
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
            Request r = new Request { after = "t3_"+plvm.Posts.Last().id };
            plvm = new PostListViewModel(r);
            Bindings.Update();

            if(PreviousPageButton.Visibility == Visibility.Collapsed)
            {
                firstPage = plvm.Posts.First().id;
                PreviousPageButton.Visibility = Visibility.Visible;
            }
            //else if ()
            {
                //need to work out the previous button being hidden on page 1
            }
            
        }

        private void PrevPageClick(object sender, RoutedEventArgs e)
        {
            Request r = new Request { before = "t3_" + plvm.Posts.First().id };
            plvm = new PostListViewModel(r);
            Bindings.Update();
        }
    }
}
