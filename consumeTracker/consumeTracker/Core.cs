using consumeTracker.Models;
using consumeTracker.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace consumeTracker
{
    public class Core
    {
        private static readonly PostGreSqlService postGreSqlService = new PostGreSqlService();
        private static readonly RestService restService = new RestService();

        public static void Connect(string username, string password)
        {
            //postGreSqlService.Connect(username, password);
            restService.InitializeLogin(username, password);
        }

        public static void Disconnect()
        {
            postGreSqlService.Disconnect();
        }

        public static bool GetConnectionStatus()
        {
            return postGreSqlService.GetConnectionStatus();
        }


        public static async Task<List<ConsumeItem>> GetData()
        {
            return await restService.GetData();
            //return postGreSqlService.GetData();
        }

        public static async Task<bool> CreateItem(ConsumeItem item)
        {
            return await restService.CreateItem(item);
        }

        public static List<StoreItem> GetStaticStores()
        {
            // TODO JR: table to be created to backend
            List<StoreItem> staticStoreList = new List<StoreItem>();
            StoreItem storeItem = new StoreItem();
            storeItem.Id = 1;
            storeItem.Store = "Prisma";
            staticStoreList.Add(storeItem);

            StoreItem storeItem2 = new StoreItem();
            storeItem2.Id = 2;
            storeItem2.Store = "Citymarket";
            staticStoreList.Add(storeItem2);

            StoreItem storeItem3 = new StoreItem();
            storeItem3.Id = 3;
            storeItem3.Store = "Valkea";
            staticStoreList.Add(storeItem3);

            return staticStoreList;
        }

        public static void CreateData(ConsumeItem item)
        {
            postGreSqlService.CreateData(item);
        }

        public static void DeleteData(ConsumeItem item)
        {
            postGreSqlService.DeleteData(item);
        }

        public static void UpdateData(ConsumeItem item)
        {
            postGreSqlService.UpdateData(item);
        }

        public static void SetSelectedConsumeItem(ConsumeItem item)
        {
            ConsumeItemHandler.SetSelectedConsumeItem(item);
        }

        public static ConsumeItem GetSelectedConsumeItem()
        {
            return ConsumeItemHandler.GetSelectedConsumeItem();
        }

        public static IDictionary<string, string> PopulateCredentials(string username, string password)
        {
            return CredentialsUtils.PopulateCredentials(username, password);
        }

        public static PostGreSqlService GetPostGreSqlService()
        {
            return postGreSqlService;
        }

        //public static event ConnectionStateStatus ConnectionStatus;

        //public delegate ConnectionStateStatus GetPostGreSqlConnectionStatus()
        //{

        //    return PostGreSqlService.GetConnectionEventStatus;
        //    //return PostGreSqlService.GetConnectionStatus;
        //}

    }

}

