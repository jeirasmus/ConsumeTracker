using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwinCoders.TouchUtils.Extensions;
using CoreGraphics;
using Foundation;
using UIKit;

namespace consumeTracker.iOS
{
    public partial class AddViewController : UIViewController
    {
        /// <summary>   
        /// Call this method from constructor, ViewDidLoad or ViewWillAppear to enable keyboard handling in the main partial class
        /// </summary>
        void InitKeyboardHandling()
        {
            //Only do this if required
            if (HandlesKeyboardNotifications())
            {
                RegisterForKeyboardNotifications();
            }
        }


        /// <summary>
        /// Set this field to any view inside the textfield to center this view instead of the current responder
        /// </summary>
        protected UIView ViewToCenterOnKeyboardShown;
        protected UIScrollView ScrollToCenterOnKeyboardShown;

        /// <summary>
        /// Override point for subclasses, return true if you want to handle keyboard notifications
        /// to center the active responder in the scroll above the keyboard when it appears
        /// </summary>
        public virtual bool HandlesKeyboardNotifications()
        {
            return false;
        }

        NSObject _keyboardShowObserver;
        NSObject _keyboardHideObserver;

       

        protected virtual void RegisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver == null)
                _keyboardShowObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
            if (_keyboardHideObserver == null)
                _keyboardHideObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
        }

        protected virtual void UnregisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardShowObserver);
                _keyboardShowObserver.Dispose();
                _keyboardShowObserver = null;
            }

            if (_keyboardHideObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardHideObserver);
                _keyboardHideObserver.Dispose();
                _keyboardHideObserver = null;
            }
        }

        /// <summary>
        /// Gets the UIView that represents the "active" user input control (e.g. textfield, or button under a text field)
        /// </summary>
        /// <returns>
        /// A <see cref="UIView"/>
        /// </returns>
        protected virtual UIView KeyboardGetActiveView()
        {
            return View.FindFirstResponder();
        }

        private void OnKeyboardNotification(NSNotification notification)
        {
            if (!IsViewLoaded) return;

            //Check if the keyboard is becoming visible
            var visible = notification.Name == UIKeyboard.WillShowNotification;

            //Start an animation, using values from the keyboard
            UIView.BeginAnimations("AnimateForKeyboard");
            UIView.SetAnimationBeginsFromCurrentState(true);
            UIView.SetAnimationDuration(UIKeyboard.AnimationDurationFromNotification(notification));
            UIView.SetAnimationCurve((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification(notification));

            //Pass the notification, calculating keyboard height, etc.
            var keyboardFrame = visible
                ? UIKeyboard.FrameEndFromNotification(notification)
                : UIKeyboard.FrameBeginFromNotification(notification);

            OnKeyboardChanged(visible, keyboardFrame);

            //Commit the animation
            UIView.CommitAnimations();
        }

        /// <summary>
        /// Override this method to apply custom logic when the keyboard is shown/hidden
        /// </summary>
        /// <param name='visible'>
        /// If the keyboard is visible
        /// </param>
        /// <param name='keyboardFrame'>
        /// Frame of the keyboard
        /// </param>
        protected virtual void OnKeyboardChanged(bool visible, CGRect keyboardFrame)
        {
            var activeView = ViewToCenterOnKeyboardShown ?? KeyboardGetActiveView();
            if (activeView == null)
                return;

            var scrollView = ScrollToCenterOnKeyboardShown ??
                activeView.FindTopSuperviewOfType(View, typeof(UIScrollView)) as UIScrollView;
            if (scrollView == null)
                return;

            if (!visible)
                scrollView.RestoreScrollPosition();
            else
                scrollView.CenterView(activeView, keyboardFrame);
        }


        /// <summary>
        /// Call it to force dismiss keyboard when background is tapped
        /// </summary>
        protected void DismissKeyboardOnBackgroundTap()
        {
            // Add gesture recognizer to hide keyboard
            var tap = new UITapGestureRecognizer { CancelsTouchesInView = false };
            tap.AddTarget(() => View.EndEditing(true));
            tap.ShouldReceiveTouch = (recognizer, touch) =>
                !(touch.View is UIControl || touch.View.FindSuperviewOfType(View, typeof(UITableViewCell)) != null);
            View.AddGestureRecognizer(tap);
        }
    }

}
