using Microsoft.EntityFrameworkCore;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Repos
{
    public class OrderRepo : IOrderRepo
    {
        private PizzaDeliveryDbContext context;

        public OrderRepo(PizzaDeliveryDbContext context)
        {
            this.context = context;
        }

        public List<Order> FindAll()
        {
            return context.Orders
                //.Include(order => order.Customer)
                //.Include(order => order.Store)
                //.Include(order => order.OrderItems)
                //.ThenInclude(oi => oi.Item)
                .ToList();
        }

        public Order FindById(int id)
        {
            return context.Orders
                .Where(order => order.Id == id)
                .Include(order => order.Customer)
                .Include(order => order.Store)
                .Include(order => order.OrderItems)
                .ThenInclude(oi => oi.Item)
                .FirstOrDefault();
        }

        public List<Order> FindOrdersByCustomer(int customerId)
        {
            return context.Orders
                .Where(o => o.CustomerId == customerId)
                .ToList();
        }

        public Order FindByGuid(Guid guid)
        {
            return context.Orders
                .Where(order => order.Guid == guid)
                .Include(order => order.Customer)
                .Include(order => order.Store)
                .Include(order => order.OrderItems)
                .ThenInclude(oi => oi.Item)
                .Single();
        }

        public void Remove(Order order)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        public List<Order> FindStoreOrders(int storeId)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order order)
        {
            context.Attach(order);
            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
            return order;
        }

        public Order Add(Order order)
        {
            context.Customers.Attach(order.Customer);
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }
    }
}
