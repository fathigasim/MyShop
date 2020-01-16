using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {

        IBasketService basketService;

        public BasketController(IBasketService BasketService)
        {
            this.basketService = BasketService;
        }
        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }
        //add to basket
        public ActionResult AddToBasket(HttpContextBase httpContext,string Id) {
            basketService.AddToBasket(this.HttpContext,Id);
            return View();
        }


        public ActionResult RemoveBasket(HttpContextBase httpContext, string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return View();
        }

        public PartialViewResult BasketSummary() {

            var basketsummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketsummary);
        }
    }
}