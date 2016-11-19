using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Linq;
using UIKit;
using Xamarin.Auth;
using System.Collections.Generic;

namespace consumeTracker.iOS
{
    partial class LoginViewController : UIViewController
    {
        private NSObject _didShowNotificationObserver;
        private NSObject _willHideNotificationObserver;
        private UIView activeTextFieldView;
        private nfloat amountToScroll = 0.0f;
        private nfloat alreadyScrolledAmount = 0.0f;
        private nfloat bottomOfTheActiveTextField = 0.0f;
        private nfloat offsetBetweenKeybordAndTextField = 10.0f;
        private bool isMoveRequired = false;

        private string CREDENTIALS_NAME = "";

        //IEnumerable<Account> accounts;
        //Account account;
        ViewController mainView;

        private Authenticator authenticator;

        //ViewController mainView = UIStoryboard.InstantiateViewController("CallHistoryController") as ViewController;


        public LoginViewController(IntPtr handle) : base(handle)
        {
            authenticator = Application.getAutheticator();
            mainView = this.Storyboard.InstantiateViewController("ViewController") as ViewController;
            //accounts = AccountStore.Create().FindAccountsForService(CREDENTIALS_NAME);
            //account = accounts.FirstOrDefault();
            //AccountStore.Create().Delete(account, CREDENTIALS_NAME);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _didShowNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardDidShow);
            _willHideNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardWillHide);


            //accounts = AccountStore.Create().FindAccountsForService(CREDENTIALS_NAME);
            ////var account = accounts.FirstOrDefault();

            //await Core.GetData();
            var account = authenticator.GetAccount();
            if (account != null)
            {
                //authenticator.DeleteAccount();
                NavigationController.PushViewController(mainView, false);
            }
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



        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            //Button.AccessibilityIdentifier = "myButton";
            //Button.TouchUpInside += delegate {
            //	var title = string.Format ("{0} clicks!", count++);
            //	Button.SetTitle (title, UIControlState.Normal);
            //};

            #region Include Return Button in Keyboard
            this.UsernameTextField.ShouldReturn += (textField) =>
            {
                UsernameTextField.ResignFirstResponder();
                return true;
            };

            this.PasswordTextField.ShouldReturn += (textField) =>
            {
                PasswordTextField.ResignFirstResponder();
                return true;
            };
            #endregion

            #region When Clicked out side keyboard, Close the Keyboard
            UITapGestureRecognizer g_recognizer = new UITapGestureRecognizer(() =>
            {
                UsernameTextField.ResignFirstResponder();
                PasswordTextField.ResignFirstResponder();
            });
            this.View.AddGestureRecognizer(g_recognizer);
            #endregion

            #region Add Next Previous Buttons to Toolbar
            UsernameTextField.InputAccessoryView = new NextPreviousToolBar(UsernameTextField, null, PasswordTextField);
            PasswordTextField.InputAccessoryView = new NextPreviousToolBar(PasswordTextField, UsernameTextField, null);
            #endregion

            #region Move UI View Up Handling
            // Keyboard popup
            _didShowNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver
            (UIKeyboard.DidShowNotification, KeyBoardDidShow, this);

            // Keyboard Down
            _willHideNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver
            (UIKeyboard.WillHideNotification, KeyBoardWillHide, this);
            #endregion


            LoginButton.TouchUpInside += delegate
            {
                try
                {
                    Core.Connect(UsernameTextField.Text, PasswordTextField.Text);
                    authenticator.CreateAccount(UsernameTextField.Text, PasswordTextField.Text);
                    //account = new Account(UsernameTextField.Text, Core.PopulateCredentials(UsernameTextField.Text, PasswordTextField.Text));
                    //AccountStore.Create().Save(account, CREDENTIALS_NAME);
                    this.NavigationController.PushViewController(mainView, true);
                    UsernameTextField.Text = "";
                    PasswordTextField.Text = "";
                    View.EndEditing(true);

                    //Core.Login();
                }
                catch (Exception ex)
                {
                    //TODO JR: add error handling dialog
                }

            };

        }

    }
}
