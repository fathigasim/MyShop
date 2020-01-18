using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.WebUI;
using MyShop.WebUI.Controllers;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class UnitTest1

    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepository<Product> productContext = new Mocks.MockContext<Product>();
            IRepository<ProductCategory> productCaregoryContext = new Mocks.MockContext<ProductCategory>();
            productContext.Insert(new Product());

            HomeController controller = new HomeController( productContext,  productCaregoryContext);

            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;


            Assert.AreEqual(1,viewModel.product.Count());
        }

      
    }
}
