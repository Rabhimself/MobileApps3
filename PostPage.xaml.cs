using RedditLite.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RedditLite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PostPage : Page
    {
        public PostPage()
        {
            this.InitializeComponent();
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        public CommentListViewModel clvm { get; set; }
        public CommentListViewModel rlvm { get; set; }
        public PostViewModel pvm { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pvm = (PostViewModel)e.Parameter;
            clvm = new CommentListViewModel(pvm.permalink, pvm.id);
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

        private void Minimize(object sender, RoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)((FrameworkElement)sender).Parent;
            ListView lv = (ListView)((StackPanel)sp.Parent).Children.Last();
            Visibility v = lv.Visibility;
            if (v.Equals(Visibility.Visible))
                lv.Visibility = Visibility.Collapsed;
            else
                lv.Visibility = Visibility.Visible;

        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            CommentViewModel cvm = (CommentViewModel)((FrameworkElement)sender).DataContext;
            if (cvm == null)
                ((FrameworkElement)sender).Visibility = Visibility.Collapsed;
            else if (cvm.replies.Count == 0)
                ((FrameworkElement)sender).Visibility = Visibility.Collapsed;
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock fe = (TextBlock)sender;
            StackPanel sp = (StackPanel)fe.Parent;
            ListView lv = (ListView)((StackPanel)sp.Parent).Children.Last();

            if (lv.Items.Count > 0)
            {
                TextBlock tb = new TextBlock();
                tb.Text = "Hide Replies";
                tb.Margin = new Thickness(0, 10, 0, 0);
                tb.Tapped += Minimize;
                sp.Children.Add(tb);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RepliesPanel.IsOpen = false;
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            String tag = (String)((FrameworkElement)sender).Tag;
            rlvm = new CommentListViewModel(pvm.permalink, tag);
            Binding b = new Binding();
            b.Source = rlvm.Comments;
            b.Mode = BindingMode.OneWay;
            RepliesListView.SetBinding(ListView.ItemsSourceProperty, b);
            RepliesListView.Width = ApplicationView.GetForCurrentView().VisibleBounds.Width - 100;
            RepliesListView.Height = ApplicationView.GetForCurrentView().VisibleBounds.Height - 150;
            RepliesPanel.IsOpen = true;
        }
    }
}
