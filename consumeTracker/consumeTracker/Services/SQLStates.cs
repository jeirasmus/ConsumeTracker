using System;
using System.Collections.Generic;
using System.Text;

namespace consumeTracker.Services
{
    public class SQLStates
    {
        public static int CONNECTED = 1;
        public static int CONNECTING = 2;
        public static int DISCONNECTED = 3;
        public static int EXECUTING = 4;
        public static int FETCHING = 5;
        public static int BROKEN = 6;
    }
}
