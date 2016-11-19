using System;
using System.Collections.Generic;
using System.Text;

namespace consumeTracker.Services
{
    public class CredentialsUtils
    {
        public static IDictionary<string, string> PopulateCredentials(string username, string password)
        {
            IDictionary<string, string> d = new Dictionary<string, string>();
            d.Add("Username", username);
            d.Add("Password", password);
            return d;
        }
    }
}
