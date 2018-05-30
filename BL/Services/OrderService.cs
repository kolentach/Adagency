using System;
using BL.DTO;
using Data.Entities;
using BL.BusinessModels;
using Data.Interfaces;
using BL.Infrastructure;
using BL.Interfaces;
using System.Collections.Generic;
using AutoMapper;

namespace BL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void MakeOrder(OrderDTO orderDto)
        {
            Advertising ad = Database.Ads.Get(orderDto.AdvertisingID);
            
            if (ad == null)
                throw new ValidationException("No ads found", "");

            decimal sum = new Discount(0.1m).GetDiscountedPrice(ad.Price);
            Order order = new Order
            {
                Date = DateTime.Now,
                Amount = 1,
                AdvertisingID = ad.ID,
                Sum = sum,
                PhoneNumber = orderDto.PhoneNumber
            };
            Database.Orders.Create(order);
            Database.Save();
        }

        public void MakePolygrahyOrder(PolygraphyDTO polygraphyDto)
        {
            Advertising ad = Database.Ads.Get(polygraphyDto.AdvertisingID);

            if (ad == null)
                throw new ValidationException("No ads found", "");

            decimal sum = new Polygraphy(polygraphyDto.Amount).GetSummarizedPrice(ad.Price);
            Order order = new Order
            {
                Date = DateTime.Now,
                Amount = polygraphyDto.Amount,
                AdvertisingID = ad.ID,
                Sum = sum,
                PhoneNumber = polygraphyDto.PhoneNumber
            };
            Database.Orders.Create(order);
            Database.Save();
        }

        public IEnumerable<AdvertisingDTO> GetAds()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Advertising, AdvertisingDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Advertising>, List<AdvertisingDTO>>(Database.Ads.GetAll());
        }

        public AdvertisingDTO GetAdvertising(int? id)
        {
            if (id == null)
                throw new ValidationException("Advertising ID is not set", "");
            var ad = Database.Ads.Get(id.Value);
            if (ad == null)
                throw new ValidationException("No ads found", "");

            return new AdvertisingDTO { ID = ad.ID, Name = ad.Name, Price = ad.Price, Description = ad.Description, Type = ad.Type };
        }

        public void EditAd(int? id, AdvertisingDTO ad)
        {
            if (id == null)
                throw new ValidationException("Advertising ID is not set", "");
            var ad_old = Database.Ads.Get(id.Value);
            if (ad_old == null)
                throw new ValidationException("No ads found", "");
            Advertising temp = new Advertising { ID = ad.ID, Name = ad.Name, Price = ad.Price, Description = ad.Description, Type = ad.Type };
            Database.Ads.Update(temp);
        }

        public IEnumerable<AdvertisingDTO> FindAds(string s_word)
        {
            List<Advertising> ad_list = new List<Advertising>();
            foreach (Advertising a in Database.Ads.GetAll())
            {
                if (a.Name.Contains(s_word) || a.Type.Contains(s_word) || a.Description.Contains(s_word))
                    ad_list.Add(a);
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Advertising, AdvertisingDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Advertising>, List<AdvertisingDTO>>(ad_list);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}