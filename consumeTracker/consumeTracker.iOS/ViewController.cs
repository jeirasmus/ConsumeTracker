using consumeTracker.Models;
using consumeTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using Xamarin.Auth;


namespace consumeTracker.iOS
{
    public partial class ViewController : UIViewController
    {
        //LoginViewController loginView;
        List<ConsumeItem> consumeList = new List<ConsumeItem>();
        UIBarButtonItem rightButton;
        private Authenticator authenticator;
        AddViewController addView;

        private string CREDENTIALS_NAME = "";

        public ViewController(IntPtr handle) : base(handle)
        {
            authenticator = Application.getAutheticator();
            addView = this.Storyboard.InstantiateViewController("AddViewController") as AddViewController;
            //loginView = this.Storyboard.InstantiateViewController("LoginViewController") as LoginViewController;

            //Core.GetPostGreSqlService().ConnectionStatus += SQLConnectionStatus;

            //var accounts = AccountStore.Create().FindAccountsForService(CREDENTIALS_NAME);
            //var account = accounts.FirstOrDefault();


            //try
            //{
            //    Core.Connect(USERNAME, PASSWORD);
            //}
            //catch (Exception ex)
            //{

            //}

            //ConsumeItem _item = new ConsumeItem
            //{
            //    Id = 5,
            //    LineNumber = 3,
            //    Date = DateTime.Now,
            //    Amount = 203,
            //    Store = "Verkkokauppa.com"
            //};

            //try
            //{
            //    Core.updateData(_item);
            //}

            //catch (Exception ex)
            //{
            //}


            //try
            //{
            //    Core.Disconnect();
            //}

            //catch (Exception ex)
            //{

            //}


            //var connection = Core.Connect(USERNAME, PASSWORD);
            //if (connection)
            //{
            //    if (account.Properties.Count == 0)
            //    {
            //        Account ac = new Account(DBNAME, Core.populateCredentials(USERNAME, PASSWORD));
            //        AccountStore.Create().Save(ac, CREDENTIALS_NAME);
            //    }

            //    try
            //    {
            //        consumeList = Core.getData();
            //    }
            //    catch (Exception ex)
            //    {

            //    }



            //    ConsumeItem _item = new ConsumeItem
            //    {
            //        Id = 3,
            //        LineNumber = 2,
            //        Date = DateTime.Now,
            //        Amount = 400,
            //        Store = "Karkkainen"
            //    };

            //    try
            //    {
            //        Core.updateData(_item);
            //    }

            //    catch (Exception ex)
            //    {
            //    }



            //try
            //{
            //    Core.createData(_item);
            //}

            //catch (Exception ex)
            //{
            //}

            //try
            //{
            //    Core.deleteData(_item);
            //}

            //catch (Exception ex)
            //{

            //}

            //try
            //{
            //    consumeList = Core.getData();
            //}
            //catch (Exception ex)
            //{

            //}

            //    try
            //    {
            //        Core.CloseConnection();
            //    }

            //    catch (Exception ex)
            //    {

            //    }
            //}
        }

        //private void SQLConnectionStatus(int status)
        //{
        //    if (status == SQLStates.CONNECTED)
        //    {
        //        try
        //        {
        //            if (Core.GetData() != null)
        //            {
        //                // populate the consume list
        //                ConsumeTableView.Source = new ConsumeTableSource(Core.GetData(), this);

        //                if (Core.GetConnectionStatus())
        //                {
        //                    Core.Disconnect();
        //                }
        //            }

        //        }

        //        catch (Exception ex)
        //        {

        //        }
        //    }

        //    if (status == SQLStates.DISCONNECTED)
        //    {
        //    }

        //}

        public async override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //var accounts = AccountStore.Create().FindAccountsForService(CREDENTIALS_NAME);
            //var account = accounts.FirstOrDefault();

            var account = authenticator.GetAccount();

            if (account == null)
            {
                // TODO JR: go to login view and clean view history stack
            }
            else
            {
                Core.Connect(account.Username, account.Properties["Password"]);

                // populate the consume list
                ConsumeTableView.Source = new ConsumeTableSource(await Core.GetData(), this);
                ConsumeTableView.ReloadData();



                //try
                //{
                //    //Core.Connect(USERNAME, PASSWORD);
                //    if (!Core.GetConnectionStatus())
                //    {
                //        Core.Connect(account.Username, account.Properties["Password"]);
                //    }
                //}
                //catch (Exception ex)
                //{

                //}


            }




        }


        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // disable back button
            NavigationItem.HidesBackButton = true;
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) =>
            {
                this.NavigationController.PushViewController(addView, true);
            }), true);

            rightButton = new UIBarButtonItem(UIImage.FromFile("logout.png"), UIBarButtonItemStyle.Plain, (s, e) =>
            {
                // Create logout alert
                var logOutAlertController = UIAlertController.Create("Kirjaudu ulos", "Haluatko varmasti kirjautua ulos?", UIAlertControllerStyle.Alert);

                // Add Actions
                logOutAlertController.AddAction(UIAlertAction.Create("Kyllä", UIAlertActionStyle.Default, alert =>
                {
                    authenticator.DeleteAccount();
                    this.NavigationController.PopViewController(false);
                }));
                logOutAlertController.AddAction(UIAlertAction.Create("Peruuta", UIAlertActionStyle.Cancel, alert => Console.WriteLine("Cancel was clicked")));

                // Show alert
                PresentViewController(logOutAlertController, true, null);


            });
            NavigationItem.RightBarButtonItem = rightButton;

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

