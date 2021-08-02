using Microsoft.EntityFrameworkCore;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Repos
{
    public class StoreRepo : IStoreRepo
    {
        private PizzaDeliveryDbContext context;

        public StoreRepo(PizzaDeliveryDbContext context)
        {
            this.context = context;
        }

        public Store Add(Store store)
        {
            context.Stores.Add(store);
            context.SaveChanges();
            return store;
        }

        public Store Update(Store store)
        {
            context.Attach(store);
            context.Entry(store).State = EntityState.Modified;
            context.SaveChanges();
            return store;
        }

        public void Remove(Store store)
        {
            context.Stores.Remove(store);
            context.SaveChanges();
        }

        public Store FindById(int id)
        {
            return context.Stores
                .Where(s => s.Id == id)
                .Include(s => s.Inventory)
                //.ThenInclude(inv => inv.Ingredient)
                //.ThenInclude(ing => ing.ItemIngredients)
                //.ThenInclude(ig => ig.Item)
                .Include(s => s.Orders)
                //.ThenInclude(ord => ord.OrderItems)
                //.Include(s => s.Orders)
                //.ThenInclude(ord => ord.Customer)
                .FirstOrDefault();
        }

        public List<Store> FindAll()
        {
            return context.Stores
                .Include(s => s.Inventory)
                //.ThenInclude(inv => inv.Ingredient)
                //.ThenInclude(ing => ing.ItemIngredients)
                //.ThenInclude(ig => ig.Item)
                //.Include(s => s.Orders)
                //.ThenInclude(ord => ord.OrderItems)
                //.Include(s => s.Orders)
                //.ThenInclude(ord => ord.Customer)
                .ToList();
        }

        public List<Store> FindByZip(string zip)
        {
            return context.Stores
                .Where(store => store.Zip == zip)
                .ToList();
        }

        public List<Store> FindByCityAndState(string city, string state)
        {
            return context.Stores
                .Where(store => store.City == city && store.State == state)
                .ToList();
        }
    }
}
