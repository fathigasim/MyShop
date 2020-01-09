using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
  public  class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productcategories;
        public ProductCategoryRepository()
        {
            productcategories = cache["productcategories"] as List<ProductCategory>;

            if (productcategories == null)
            {
                productcategories = new List<ProductCategory>();

            }
        }

        public void Commit()
        {
            cache["productcategories"] = productcategories;
        }
        //inset to cache
        public void Insert(ProductCategory productcategory)
        {
            productcategories.Add(productcategory);
        }

        //update
        public void Update(ProductCategory productcategory)
        {
            ProductCategory productCategoryToUpdate = productcategories.Find(p => p.Id == productcategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productcategory;
            }
            else
            {
                throw new Exception("Product category has not been found");
            }
        }
        //search
        public ProductCategory Find(string Id)
        {
            ProductCategory productcategory = productcategories.Find(p => p.Id == Id);

            if (productcategory != null)
            {
                return productcategory;
            }
            else
            {
                throw new Exception("Product category has not been found");
            }
        }
        //return a list of products we use iquerable

        public IQueryable<ProductCategory> Collection()
        {

            return productcategories.AsQueryable();
        }
        //to delete product
        public void Delete(string Id)
        {
            ProductCategory productToDelete = productcategories.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                productcategories.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product category has not been found");
            }


        }
    }
}
