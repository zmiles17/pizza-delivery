using Microsoft.EntityFrameworkCore;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Repos
{
    public class InventoryRepo : IInventoryRepo
    {
        private PizzaDeliveryDbContext context;

        public InventoryRepo(PizzaDeliveryDbContext context)
        {
            this.context = context;
        }

        public Inventory Update(Inventory inventory)
        {
            context.Attach(inventory);
            context.Entry(inventory).State = EntityState.Modified;
            context.SaveChanges();
            return inventory;
        }

        public Inventory Add(Inventory inventory)
        {
            context.Inventories.Add(inventory);
            context.SaveChanges();
            return inventory;
        }

        public Inventory FindById(int id)
        {
            return context.Inventories
                .Where(inventory => inventory.Id == id)
                .Include(inventory => inventory.Ingredient)
                .Include(inventory => inventory.Store)
                .FirstOrDefault();
        }

        public List<Inventory> FindAll()
        {
            return context.Inventories
                .Include(inventory => inventory.Ingredient)
                .Include(inventory => inventory.Store)
                .ToList();
        }

        public void Remove(Inventory inventory)
        {
            context.Inventories.Remove(inventory);
            context.SaveChanges();
        }

        public List<Inventory> FindInventoryForStore(int storeId)
        {
            return context.Inventories
                .Where(inv => inv.StoreId == storeId)
                .ToList();
        }
    }
}
