using System.Collections.Generic;
using BL.DTO;

namespace BL.Interfaces
{
    public interface IOrderService
    {
        void MakeOrder(OrderDTO orderDto);
        void MakePolygrahyOrder(PolygraphyDTO orderDto);
        AdvertisingDTO GetAdvertising(int? id);
        IEnumerable<AdvertisingDTO> GetAds();
        void Dispose();
    }
}