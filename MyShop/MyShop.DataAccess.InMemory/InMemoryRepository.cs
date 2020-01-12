using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
   public class InMemoryRepository <T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;

        string ClassName;

        public InMemoryRepository()
        {
            ClassName = typeof(T).Name;
            items = cache[ClassName] as List<T>;
            if (items == null) {

                items = new List<T>();
            }
        }
        //save as generics type
        public void Commit() {
            cache[ClassName] = items;
        }
        //insert as generic type

        public void Insert(T t) {

            items.Add(t);
        }

        //Update generic type
        public void Update(T t)
        {
            T tToUpdate = items.Find(p => p.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else {
                throw new Exception(ClassName+"has not been found");
            }            
            
        }
        //Find generic type
        public T Find(string Id) {
            T tToFind = items.Find(p => p.Id == Id);
            if (tToFind == null)
            {
                throw new Exception(ClassName + "Not found");
            }
            else {
                return tToFind;
            }
                    
                    
        }
        //return collection of list

        public IQueryable <T>Collection()
        {
            return items.AsQueryable();
        }

        //Delete generic type
        public void Delete(string Id)
        {
            T tToDelete = items.Find(p => p.Id == Id);
            if (tToDelete == null)
            {
                  throw new Exception(ClassName + "Not found");
            }
            else
            {
                items.Remove(tToDelete);
            }


        }
    }
}
