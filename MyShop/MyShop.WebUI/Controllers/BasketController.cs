using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Customer> customers;
        IBasketService basketService;
        IOrderService orderService;

        public BasketController(IBasketService BasketService,IOrderService OrderService, IRepository<Customer> Customers)
        {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customers = Customers;
        }
        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext );
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext,Id);  
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            
            basketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }
        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);

        }
        [Authorize]
        public ActionResult CheckOut()
        {
            Customer Customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if (Customer != null)
            {
                Order order = new Order() {
                    Email=Customer.Email,
                    City=Customer.City,
                    FirstName = Customer.FirstName,
                    SurName = Customer.LastName,
                    State = Customer.State,
                    Street=Customer.Street,
                    ZipCode=Customer.ZipCode
                };
                return View(order);
            }
            else
            {

                return RedirectToAction("Error NO Order Available");
            }
          
        }
        [HttpPost]
        [Authorize]
        public ActionResult CheckOut(Order order)
        {
            var basketItem = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;
            //Processing Payment
            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, basketItem);
            basketService.ClearBasket(this.HttpContext);
            return RedirectToAction("ThankYou", new  {OrderId=order.Id });
        }

        public ActionResult ThankYou(string OrderId) {
            ViewBag.OrderId = OrderId;
            return View();

        }
    }
    }
