using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryManagerController : Controller
    {
        // GET: ProductCategoryManager
        //ProductCategoryRepository context;
        //InMemoryRepository<ProductCategory> context;
        IRepository<ProductCategory> context;
        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            //context = new ProductCategoryRepository();
            //context = new InMemoryRepository<ProductCategory>();
            this.context = context;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productcategories = context.Collection().ToList();
            return View(productcategories);
        }
        //create product
        public ActionResult Create()
        {
            ProductCategory productcategory = new ProductCategory();

            return View(productcategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productcategory)
        {

            if (!ModelState.IsValid)
            {

                return View(productcategory);
            }

            else
            {
                context.Insert(productcategory);
                context.Commit();
                return RedirectToAction("Index");
            }
            //Product productCreate = new Product();
            //product.Id = Guid.NewGuid().ToString();
            //product.Name = product.Name;
            //product.Price = product.Price;
            //product.Image = product.Image;
            //return View(products);

        }

        //Editing
        public ActionResult Edit(string Id)
        {
            ProductCategory productcategory = context.Find(Id);
            if (productcategory == null)
            {
                HttpNotFound();
            }

            return View(productcategory);

        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productcategory, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid )
                {
                    return View(productcategory);
                }
                else
                {

                    //productCategoryToEdit = productcategory.Id.ToString();
                    productCategoryToEdit.Category = productcategory.Category;


                    context.Commit();

                    return RedirectToAction("Index");
                }
            }
        }
        //delete method

        public ActionResult Delete(string Id)
        {

            ProductCategory productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {

            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}