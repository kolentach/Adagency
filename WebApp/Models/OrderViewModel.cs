namespace Web.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }
        public decimal Sum { get; set; }
        public string PhoneNumber { get; set; }

        public int AdvertisingID { get; set; }
    }
}