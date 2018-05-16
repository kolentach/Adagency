using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BL.DTO
{
    public class PolygraphyDTO
    {
        public int ID { get; set; }
        public decimal Sum { get; set; }
        public int Amount { get; set; }
        public string PhoneNumber { get; set; }

        public int AdvertisingID { get; set; }

        public DateTime? Date { get; set; }
    }
}