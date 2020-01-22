using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
  public  class OrderItemViewModel
    {
        public Order orders { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }
    }
}
