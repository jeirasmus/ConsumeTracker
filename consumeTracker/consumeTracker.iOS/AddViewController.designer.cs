// WARNING
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
    [Register ("AddViewController")]
    partial class AddViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView AddView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView storeTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField sumUiTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AddView != null) {
                AddView.Dispose ();
                AddView = null;
            }

            if (storeTableView != null) {
                storeTableView.Dispose ();
                storeTableView = null;
            }

            if (sumUiTextField != null) {
                sumUiTextField.Dispose ();
                sumUiTextField = null;
            }
        }
    }
}