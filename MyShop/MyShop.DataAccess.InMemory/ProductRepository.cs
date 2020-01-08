using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
  public  class ProductRepository
    {

        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null) {
                products=    new List<Product>();
                
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }
        //inset to cache
        public void Insert(Product product) {
            products.Add(product);
        }

        //update
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id ==product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else {
                throw new Exception("Product has not been found");
            }
        }
        //search
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product has not been found");
            }
        }
        //return a list of products we use iquerable

        public IQueryable<Product> Collection() {

            return products.AsQueryable();
        }
        //to delete product
        public void Delete(string Id) {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product has not been found");
            }


        }
    }
}
