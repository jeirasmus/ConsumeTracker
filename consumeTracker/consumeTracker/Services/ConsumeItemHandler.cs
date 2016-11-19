using consumeTracker.Models;

namespace consumeTracker.Services
{
    public class ConsumeItemHandler
    {
        private static ConsumeItem consumeItem;

        public static void SetSelectedConsumeItem(ConsumeItem item)
        {
            consumeItem = item;
        }
        public static ConsumeItem GetSelectedConsumeItem()
        {
            return consumeItem;
        }
    }
}
