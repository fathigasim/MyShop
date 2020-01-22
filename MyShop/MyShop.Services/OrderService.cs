﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.Services
{
   public class OrderService : IOrderService
    {
        IRepository<Order> orderContext;
        public OrderService(IRepository<Order>  OrderContext)
        {
            this.orderContext = OrderContext;
        }

        public void CreateOrder(Order BaseOrder, List<BasketItemViewModel> BasketItems)
        {
            foreach (var item in BasketItems)
            {
                BaseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.Name,
                    Quantity = item.Quantity

                });
            }
            orderContext.Insert(BaseOrder);
            orderContext.Commit();
        }

        public List<Order> GetOrderList() {
            return orderContext.Collection().ToList();
        }

        public Order GetOrder(string Id) {
           

            return orderContext.Find(Id);
        }

        public void UpdateOrder(Order UpdatedOrder) {
            orderContext.Update(UpdatedOrder);
            orderContext.Commit();
        }
    }
}
