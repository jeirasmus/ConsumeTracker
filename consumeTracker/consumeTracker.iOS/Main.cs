using UIKit;

namespace consumeTracker.iOS
{
    public class Application
	{
        // This is the main entry point of the application.

        private static Authenticator authenticator = new Authenticator();

        public static Authenticator getAutheticator()
        {
            return authenticator;
        }

        static void Main (string[] args)
		{          
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main (args, null, "AppDelegate");

		}   
	}
}
