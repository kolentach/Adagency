using System.Collections.Generic;
using System.Web.Mvc;
using BL.Interfaces;
using BL.DTO;
using Web.Models;
using AutoMapper;
using BL.Infrastructure;
using System;
using System.Linq;

namespace NLayerApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        IOrderService orderService;
        public HomeController() { }

        public HomeController(IOrderService serv)
        {
            orderService = serv;
        }

        public ActionResult Index()
        {
            IEnumerable<AdvertisingDTO> adDtos = orderService.GetAds();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AdvertisingDTO, AdvertisingViewModel>()).CreateMapper();
            var ads = mapper.Map<IEnumerable<AdvertisingDTO>, List<AdvertisingViewModel>>(adDtos);
            return View(ads);
        }

       
        public ActionResult Index(string searchString)
        {
            var ads = from a in orderService.GetAds()
                           select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                ads = ads.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString) || s.Type.Contains(searchString));
            }

            return View(ads);
        }

        [HttpPost]
        public string Indexe(FormCollection fc, string searchString)
        {
            return "<h3> From [HttpPost]Index: " + searchString + "</h3>";
        }

        [Authorize(Roles = "manager, admin")]
        public ActionResult EditAd(int? id)
        {
            try
            {
                AdvertisingDTO ad = orderService.GetAdvertising(id);
                AdvertisingViewModel adVM = new AdvertisingViewModel { ID = ad.ID, Description = ad.Description, Name = ad.Name, Price = ad.Price, Type = ad.Type };
                return View(adVM);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [Authorize(Roles="manager, admin")]
        [HttpPost]
        public ActionResult EditAd(AdvertisingViewModel ad)
        {
            try
            {
                AdvertisingDTO new_ad = new AdvertisingDTO { ID = ad.ID, Description = ad.Description, Name = ad.Name, Price = ad.Price, Type = ad.Type };

                orderService.EditAd(new_ad.ID, new_ad);

                return RedirectToAction("Index", "Home");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [Authorize(Roles="user")]
        public ActionResult MakeOrder(int? id)
        {
            try
            {
                AdvertisingDTO ad = orderService.GetAdvertising(id);
                var order = new OrderViewModel { AdvertisingID = ad.ID };

                return View(order);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            try
            {
                var orderDto = new OrderDTO { AdvertisingID = order.AdvertisingID, PhoneNumber = order.PhoneNumber };
                orderService.MakeOrder(orderDto);
                return RedirectToAction("Index", "Home");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(order);
        }

        [Authorize(Roles = "user")]
        public ActionResult MakePolygraphyOrder(int? id)
        {
            try
            {
                AdvertisingDTO ad = orderService.GetAdvertising(id);
                var order = new PolygraphyViewModel { AdvertisingID = ad.ID };

                return View(order);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult MakePolygraphyOrder(PolygraphyViewModel order)
        {
            try
            {
                var orderDto = new PolygraphyDTO { AdvertisingID = order.AdvertisingID, PhoneNumber = order.PhoneNumber, Amount = order.Amount };
                orderService.MakePolygrahyOrder(orderDto);
                return RedirectToAction("Index", "Home");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(order);
        }
        protected override void Dispose(bool disposing)
        {
            orderService.Dispose();
            base.Dispose(disposing);
        }

    }
}
