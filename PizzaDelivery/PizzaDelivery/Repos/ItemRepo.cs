using Microsoft.EntityFrameworkCore;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Repos
{
    public class ItemRepo : IItemRepo
    {
        private PizzaDeliveryDbContext context;

        public ItemRepo(PizzaDeliveryDbContext context)
        {
            this.context = context;
        }

        public void Remove(Item item)
        {
            context.Items.Remove(item);
            context.SaveChanges();
        }

        public Item FindById(int id)
        {
            return context.Items
                .Where(item => item.Id == id)
                .Include(item => item.ItemIngredients)
                .FirstOrDefault();
        }

        public Item Update(Item item)
        {
            context.Attach(item);
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
            return item;
        }

        public List<Item> FindAll()
        {
            return context.Items
                .Include(i => i.ItemIngredients)
                .ToList();
        }

        //public List<Item> FindItemsForOrder(int orderId)
        //{
        //    return context.OrderItems
        //        .Where(oi => oi.OrderId == orderId)
        //        .Include(oi => oi.Item)
        //        .Select(oi => new Item(oi.Item))
        //        .ToList();
        //}

        public Item Add(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();
            return item;
        }
    }
}
