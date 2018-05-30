using System;
using NUnit.Framework;
using Moq;
using BL.Infrastructure;
using BL.DTO;
using Data.Interfaces;
using Data.Entities;
using BL.Interfaces;
using BL.Services;
using Data.EF;
using Data.Repositories;

namespace BLTest
{
    [TestFixture]
    public class BLTest
    {
        IOrderService serv;

        [Test]
        public void TestMakeOrder()
        {
            var uow = new EFUnitOfWork("defaultConnection");
            serv = new OrderService(uow);
            OrderDTO order = new OrderDTO { ID = 1, Date = DateTime.Now, AdvertisingID = 1, PhoneNumber = "12312321", Sum = 100 };
            serv.MakeOrder(order);

            uow.Orders.GetAll();
            Order test = uow.Orders.GetAll().GetEnumerator().Current;

            Assert.That(test.ID == order.ID);
            Assert.That(test.AdvertisingID == order.AdvertisingID);
            Assert.That(test.Date == order.Date);
            Assert.That(test.PhoneNumber == order.PhoneNumber);
        }

        [Test]
        public void TestMakePolygrahyOrder()
        {
            var uow = new EFUnitOfWork("defaultConnection");
            serv = new OrderService(uow);
            PolygraphyDTO order = new PolygraphyDTO { ID = 1, Date = DateTime.Now, AdvertisingID = 1, PhoneNumber = "12312321", Sum = 100, Amount = 20 };
            serv.MakePolygrahyOrder(order);

            uow.Orders.GetAll();
            Order test = uow.Orders.GetAll().GetEnumerator().Current;

            Assert.That(test.ID == order.ID);
            Assert.That(test.AdvertisingID == order.AdvertisingID);
            Assert.That(test.Date == order.Date);
            Assert.That(test.PhoneNumber == order.PhoneNumber);
            Assert.That(test.Amount == order.Amount);
        }

        [Test]
        public void TestGetAds()
        {
            var uow = new EFUnitOfWork("defaultConnection");
            serv = new OrderService(uow);
            Advertising ad = new Advertising { ID = 1, Name = "Advertising", Description = "Description", Price = 100, Type = "type" };
            uow.Ads.Create(ad);

            AdvertisingDTO test = serv.GetAds().GetEnumerator().Current;

            Assert.That(test.ID == ad.ID);
            Assert.That(test.Name == ad.Name);
            Assert.That(test.Price == ad.Price);
            Assert.That(test.Type == ad.Type);
            Assert.That(test.Description == ad.Description);
        }

        [Test]
        public void TestGetAdvertising()
        {
            var uow = new EFUnitOfWork("defaultConnection");
            serv = new OrderService(uow);
            Advertising ad = new Advertising { ID = 1, Name = "Advertising", Description = "Description", Price = 100, Type = "type" };
            uow.Ads.Create(ad);

            AdvertisingDTO test = serv.GetAdvertising(ad.ID);

            Assert.That(test.ID == ad.ID);
            Assert.That(test.Name == ad.Name);
            Assert.That(test.Price == ad.Price);
            Assert.That(test.Type == ad.Type);
            Assert.That(test.Description == ad.Description);
        }

        [Test]
        public void TestEditAd()
        {
            var uow = new EFUnitOfWork("defaultConnection");
            serv = new OrderService(uow);
            Advertising ad = new Advertising { ID = 1, Name = "Advertising", Description = "Description", Price = 100, Type = "type" };
            uow.Ads.Create(ad);

            serv.EditAd(ad.ID, (new AdvertisingDTO { ID = 1, Name = "ChangedAdvertising", Description = "ChangedDescription", Price = 100, Type = "type" }));

            AdvertisingDTO test = serv.GetAdvertising(ad.ID);
            Assert.That(test.ID == ad.ID);
            Assert.That(test.Name == ad.Name);
            Assert.That(test.Price == ad.Price);
            Assert.That(test.Type == ad.Type);
            Assert.That(test.Description == ad.Description);
        }

        [Test]
        public void TestFindAds()
        {
            var uow = new EFUnitOfWork("defaultConnection");
            serv = new OrderService(uow);
            Advertising ad = new Advertising { ID = 1, Name = "Advertising", Description = "Description", Price = 100, Type = "type" };
            uow.Ads.Create(ad);

            AdvertisingDTO test = serv.FindAds(ad.Name).GetEnumerator().Current;

            Assert.That(test.ID == ad.ID);
            Assert.That(test.Name == ad.Name);
            Assert.That(test.Price == ad.Price);
            Assert.That(test.Type == ad.Type);
            Assert.That(test.Description == ad.Description);
        }
    }
}
