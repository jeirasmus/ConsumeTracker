﻿// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace consumeTracker.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView ConsumeTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MainView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ConsumeTableView != null) {
                ConsumeTableView.Dispose ();
                ConsumeTableView = null;
            }

            if (MainView != null) {
                MainView.Dispose ();
                MainView = null;
            }
        }
    }
}