using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Auth;

namespace consumeTracker.iOS
{
    public class Authenticator
    {
        private AccountStore accountStore;
        //private IEnumerable<Account> accounts;
        private Account account;

        private string CREDENTIALS_NAME = "";

        public Authenticator()
        {
            accountStore = AccountStore.Create();
            var accounts = accountStore.FindAccountsForService(CREDENTIALS_NAME);
            account = accounts.FirstOrDefault();
        }         

        public Account GetAccount()
        {
            return account;
        }

        public void CreateAccount(string username, string password)
        {
            account = new Account(username, Core.PopulateCredentials(username, password));
            //accountStore.Save(account, CREDENTIALS_NAME);
        }

        public void DeleteAccount()
        {
            if (account != null)
            {
                accountStore.Delete(account, CREDENTIALS_NAME);
                account = null;         
            }
        }



    }
}