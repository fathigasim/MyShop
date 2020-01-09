﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository productCategories;

        public ProductManagerController()
        {
            context = new ProductRepository();
            productCategories = new ProductCategoryRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        //create product
        public ActionResult Create()
        {
           // Product product = new Product();
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
          //  viewModel.product = new Product();
            viewModel.productcategories = productCategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
           
            if (!ModelState.IsValid)
            {

                return View(product);
            }

            else {
                context.Insert(product);
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
        public ActionResult Edit(string Id) {
            Product product = context.Find(Id);
            if (product == null)
            {
             return   HttpNotFound();
            }
            else {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.product = new Product();
                viewModel.productcategories = productCategories.Collection();
                return View(viewModel);
            }
               
       
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
               return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid == null)
                {
                    return View(product);
                }
                else
                {



                    productToEdit.Category = product.Category;
                    productToEdit.Description = product.Description;
                    productToEdit.Image = product.Image;
                    productToEdit.Name = product.Name;
                    productToEdit.Price = product.Price;
                    context.Commit();

                    return RedirectToAction("Index");
                }
            }
            }
        //delete method

        public ActionResult Delete(string Id) {

            Product productToDelete=context.Find(Id);
            if (productToDelete == null)
            {
            return    HttpNotFound();
            }

            else {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {

            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
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