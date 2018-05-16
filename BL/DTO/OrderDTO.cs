using System;

namespace BL.DTO
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public decimal Sum { get; set; }
        public string PhoneNumber { get; set; }

        public int AdvertisingID { get; set; }

        public DateTime? Date { get; set; }
    }
}