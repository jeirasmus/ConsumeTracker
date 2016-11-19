using consumeTracker.Models;
using CoreGraphics;
using Foundation;
using System;
using System.Drawing;
using UIKit;


namespace consumeTracker.iOS
{
    public partial class AddViewController : UIViewController
    {
        private NSObject _didShowNotificationObserver;
        private NSObject _willHideNotificationObserver;
        private UIView activeTextFieldView;
        private nfloat amountToScroll = 0.0f;
        private nfloat alreadyScrolledAmount = 0.0f;
        private nfloat bottomOfTheActiveTextField = 0.0f;
        private nfloat offsetBetweenKeybordAndTextField = 10.0f;
        private bool isMoveRequired = false;

        UIBarButtonItem rightButton;

        // for the keyboard shifting
        //private UIView activeview;             // Controller that activated the keyboard
        //private nfloat scroll_amount = 0.0f;    // amount to scroll 
        //private nfloat bottom = 0.0f;           // bottom point
        //private nfloat offset = 10.0f;          // extra offset
        //private bool moveViewUp = false;           // which direction are we moving

        public AddViewController(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            #region Include Return Button in Keyboard
            this.sumUiTextField.ShouldReturn += (textField) =>
            {
                sumUiTextField.ResignFirstResponder();
                return true;
            };
            #endregion

            #region When Clicked out side keyboard, Close the Keyboard
            var tap = new UITapGestureRecognizer { CancelsTouchesInView = false };
            tap.AddTarget(() => View.EndEditing(true));
            tap.ShouldReceiveTouch += (recognizer, touch) => !(touch.View is UIControl);
            View.AddGestureRecognizer(tap);
            #endregion

            #region Move UI View Up Handling
            // Keyboard popup
            _didShowNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver
            (UIKeyboard.DidShowNotification, KeyBoardDidShow, this);

            // Keyboard Down
            _willHideNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver
            (UIKeyboard.WillHideNotification, KeyBoardWillHide, this);
            #endregion


            //Xamarin.IQKeyboardManager.SharedManager.Enable = true;
            //Xamarin.IQKeyboardManager.SharedManager.EnableAutoToolbar = false;
            //Xamarin.IQKeyboardManager.SharedManager.ShouldResignOnTouchOutside = true;
            //Xamarin.IQKeyboardManager.SharedManager. = true;
            //sumUiTextField.InputAccessoryVie

            //var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            //g.CancelsTouchesInView = false; //for iOS5
            //View.AddGestureRecognizer(g);

            // keyboard shifting
            // Keyboard popup
            //NSNotificationCenter.DefaultCenter.AddObserver
            //(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

            //// Keyboard Down
            //NSNotificationCenter.DefaultCenter.AddObserver
            //(UIKeyboard.WillHideNotification, KeyBoardDownNotification);

            // disable back button
            NavigationItem.HidesBackButton = true;

            NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIImage.FromFile("close.png"), UIBarButtonItemStyle.Plain, (s, e) =>
            {
                resetViewState();
            }), true);

            rightButton = new UIBarButtonItem(UIImage.FromFile("save.png"), UIBarButtonItemStyle.Plain, async (s, e) =>
            {
                ConsumeItem item = new ConsumeItem();
                var date = DateTime.Now;
                var id = date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + date.Millisecond.ToString() + date.Month.ToString() + date.Year.ToString();
                item.Id = 3334;
                item.Store = "Ideapark";
                item.Date = date.Date;
                item.Amount = Convert.ToDouble(sumUiTextField.Text);
                var status = await Core.CreateItem(item);
                resetViewState();
            });
            NavigationItem.RightBarButtonItem = rightButton;
        }

        private void resetViewState()
        {
            View.EndEditing(true);
            NavigationController.PopViewController(true);
            sumUiTextField.Text = "";
        }

        //private void KeyBoardUpNotification(NSNotification notification)
        //{
        //    // get the keyboard size
        //    var val = (NSValue)notification.UserInfo.ValueForKey(UIKeyboard.FrameEndUserInfoKey);
        //    CGRect r = val.CGRectValue;

        //    // Find what opened the keyboard
        //    foreach (UIView view in this.View.Subviews)
        //    {
        //        if (view.IsFirstResponder)
        //            activeview = view;
        //    }

        //    // Bottom of the controller = initial position + height + offset      
        //    bottom = (activeview.Frame.Y + activeview.Frame.Height + offset);

        //    // Calculate how far we need to scroll
        //    scroll_amount = (r.Height - (View.Frame.Size.Height - bottom));

        //    // Perform the scrolling
        //    if (scroll_amount > 0)
        //    {
        //        moveViewUp = true;
        //        ScrollTheView(moveViewUp);
        //    }
        //    else
        //    {
        //        moveViewUp = false;
        //    }

        //}

        //private void KeyBoardDownNotification(NSNotification notification)
        //{
        //    if (moveViewUp) { ScrollTheView(false); }
        //}

        //private void ScrollTheView(bool move)
        //{

        //    // scroll the view up or down
        //    UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
        //    UIView.SetAnimationDuration(0.3);

        //    CGRect frame = View.Frame;

        //    if (move)
        //    {
        //        frame.Y -= scroll_amount;
        //    }
        //    else
        //    {
        //        frame.Y += scroll_amount;
        //        scroll_amount = 0;
        //    }

        //    View.Frame = frame;
        //    UIView.CommitAnimations();
        //}


        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            storeTableView.Source = new StoreTableSource(Core.GetStaticStores(), this);
            storeTableView.ReloadData();

            _didShowNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardDidShow);
            _willHideNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardWillHide);

        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            if (_didShowNotificationObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_didShowNotificationObserver);
            }

            if (_willHideNotificationObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_willHideNotificationObserver);
            }
        }

        private void KeyBoardDidShow(NSNotification notification)
        {
            // get the keyboard size
            CoreGraphics.CGRect notificationBounds = UIKeyboard.BoundsFromNotification(notification);

            // Find what opened the keyboard
            foreach (UIView view in this.View.Subviews)
            {
                if (view.IsFirstResponder)
                    activeTextFieldView = view;
            }

            // Bottom of the controller = initial position + height + offset
            bottomOfTheActiveTextField = (activeTextFieldView.Frame.Y + activeTextFieldView.Frame.Height + offsetBetweenKeybordAndTextField);

            // Calculate how far we need to scroll
            amountToScroll = (notificationBounds.Height - (View.Frame.Size.Height - bottomOfTheActiveTextField));

            // Perform the scrolling
            if (amountToScroll > 0)
            {
                bottomOfTheActiveTextField -= alreadyScrolledAmount;
                amountToScroll = (notificationBounds.Height - (View.Frame.Size.Height - bottomOfTheActiveTextField));
                alreadyScrolledAmount += amountToScroll;
                isMoveRequired = true;
                ScrollTheView(isMoveRequired);
            }
            else
            {
                isMoveRequired = false;
            }

        }

        private void KeyBoardWillHide(NSNotification notification)
        {
            //bool wasViewMoved = !isMoveRequired;
            //if (isMoveRequired) { ScrollTheView(wasViewMoved); }
            ScrollTheView(false);
        }

        private void ScrollTheView(bool move)
        {
            // scroll the view up or down
            UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
            UIView.SetAnimationDuration(0.3);

            CoreGraphics.CGRect frame = View.Frame;

            if (move)
            {
                frame.Y -= amountToScroll;
            }
            else
            {
                frame.Y += alreadyScrolledAmount;
                amountToScroll = 0;
                alreadyScrolledAmount = 0;
            }

            View.Frame = frame;
            UIView.CommitAnimations();
        }


    }
}