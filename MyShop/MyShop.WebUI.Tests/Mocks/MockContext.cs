using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyShop.WebUI.Tests.Mocks
{
   public class MockContext<T> : IRepository <T> where T : BaseEntity
    {
        List<T> items;
        string ClassName;
       
        public MockContext()
        {
           items= new List<T>();
        }
        //save as generics type
        public void Commit()
        {
            return;
        }
        //insert as generic type
        //to test whether interface id working fine i am creating new mothed purely for test

        // public void DoSomething() { }
        public void Insert(T t)
        {

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
            else
            {
                throw new Exception(ClassName + "has not been found");
            }

        }
        //Find generic type
        public T Find(string Id)
        {
            T tToFind = items.Find(p => p.Id == Id);
            if (tToFind == null)
            {
                throw new Exception(ClassName + "Not found");
            }
            else
            {
                return tToFind;
            }


        }
        //return collection of list

        public IQueryable<T> Collection()
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
 