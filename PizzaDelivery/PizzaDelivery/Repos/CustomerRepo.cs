using Microsoft.EntityFrameworkCore;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private PizzaDeliveryDbContext context;

        public CustomerRepo(PizzaDeliveryDbContext context)
        {
            this.context = context;
        }

        public Customer FindById(int id)
        {
            return context.Customers
                .Where(customer => customer.Id == id)
                .Include(customer => customer.Orders)
                .ThenInclude(order => order.OrderItems)
                .FirstOrDefault();
        }

        public Customer Update(Customer customer)
        {
            context.Attach(customer);
            context.Entry(customer).State = EntityState.Modified;
            context.SaveChanges();
            return customer;
        }

        public Customer Add(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer;
        }

        public List<Customer> FindAll()
        {
            return context.Customers
                .ToList();
        }

        public Customer FindCustomerInfo(string name, string phone)
        {
            return context.Customers
                .Where(customer => customer.Name == name && customer.Phone == phone)
                .FirstOrDefault();
        }

        public void Remove(Customer customer)
        {
            context.Customers.Remove(customer);
            context.SaveChanges();
        }
    }
}
