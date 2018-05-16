using System.Web;
using System.Collections.Generic;
using System.Web.Mvc;
using BL.Interfaces;
using BL.DTO;
using Web.Models;
using AutoMapper;
using BL.Infrastructure;
using UserStore.BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;

using Microsoft.Owin.Security;
using System.Threading.Tasks;
using UserStore.Models;
using UserStore.BLL.DTO;
using System.Security.Claims;
using UserStore.BLL.Infrastructure;

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

        [Authorize]
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

        [Authorize]
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
