using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace consumeTracker.iOS
{
    public class NextPreviousToolBar : UIToolbar
    {
        public UIView prevTextField { get; set; }
        public UIView currentTextField { get; set; }
        public UIView nextTextField { get; set; }

        public NextPreviousToolBar() : base() { }

        public NextPreviousToolBar(UIView curr, UIView prev,
        UIView next)
        {
            this.currentTextField = curr;
            this.prevTextField = prev;
            this.nextTextField = next;
            AddButtonsToToolBar();
        }

        void AddButtonsToToolBar()
        {
            Frame = new CoreGraphics.CGRect(0.0f, 0.0f, 320, 44.0f);
            TintColor = UIColor.DarkGray;
            Translucent = false;
            Items = new UIBarButtonItem[]
            { new UIBarButtonItem("Edellinen", UIBarButtonItemStyle.Bordered, delegate
            {
            prevTextField.BecomeFirstResponder();
            }) { Enabled = prevTextField != null },
            new UIBarButtonItem("Seuraava",
            UIBarButtonItemStyle.Bordered, delegate
            {
            nextTextField.BecomeFirstResponder();
            }) { Enabled = nextTextField != null },
            new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
            new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
            {
            currentTextField.ResignFirstResponder();
            })
            };
        }
    }
}