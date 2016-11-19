using System;
using System.Collections.Generic;
using System.Text;

namespace consumeTracker.Models
{
    public class ConsumeItem
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Store { get; set; }
    }

    public class StoreItem
    {
        public int Id { get; set; }

        public string Store { get; set; }
    }


    public class ServiceResponse<T>
    {
        public string Status { get; set; }

        public T Data { get; set; }
    }
}
