﻿using Lurker.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Lurker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PostPage : Page
    {
        public PostPage()
        {
            this.InitializeComponent();

        }

        public CommentListViewModel clvm { get; set; }
        public CommentListViewModel rlvm { get; set; }
        private PostViewModel pvm;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pvm = (PostViewModel)e.Parameter;
            clvm = new CommentListViewModel(pvm.permalink, pvm.id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String myValue = (String)((Button)sender).Tag;
            rlvm = new CommentListViewModel(pvm.permalink, myValue);     
        }
    }
}
