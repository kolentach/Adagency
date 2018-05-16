using System;

namespace Data.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public decimal Sum { get; set; }
        public int Amount { get; set; }
        public string PhoneNumber { get; set; }

        public int AdvertisingID { get; set; }
        public Advertising Advertising { get; set; }

        public DateTime Date { get; set; }
    }
}