using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PizzaDelivery.Models;
using PizzaDelivery.Models.Enums;
using PizzaDelivery.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDeliveryTests
{
    class OrderRepoTests
    {

        ServiceCollection services = new ServiceCollection();
        OrderRepo orderRepo;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<PizzaDeliveryDbContext>();
            var config = new ConfigurationBuilder().AddJsonFile("C:/Users/ZMiles/classwork-miles-zach/PizzaDelivery/PizzaDeliveryTests/appsettings.test.json").Build();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));
            services.AddDbContext<PizzaDeliveryDbContext>(options => options.UseSqlServer(config.GetConnectionString("TestDb")));
            var context = new PizzaDeliveryDbContext(builder.Options);
            orderRepo = new OrderRepo(context);
            context.Orders.RemoveRange(context.Orders);
            context.Stores.RemoveRange(context.Stores);
            context.Items.RemoveRange(context.Items);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Orders', RESEED, 0)");
        }

        //[Test]
        //public void AddOrder()
        //{
        //    Order order = new Order
        //    {
        //        TimeIn = DateTime.UtcNow,
        //        OrderStatus = OrderStatus.PREPARING,
        //        Store = new Store
        //        {
        //            StoreNumber = "1",
        //            Address = "123 Hunt Avenue",
        //            City = "Mobile",
        //            State = "AL",
        //            Zip = "45354",
        //            Phone = "3432432423"
        //        },
        //        OrderItems = new List<OrderItem>(new OrderItem[] {
        //            new OrderItem
        //            {
        //                Item = new Item
        //                {
        //                    Name = "Pepperoni Pizza",
        //                    Price = 9.99M,
        //                    ImageUrl = "https://imgur.com/test"
        //                },
        //                Quantity = 1
        //            }
        //        })
        //    };

        //    orderRepo.Add(order);

        //    Assert.That(order.Id == 1);
        //}
    }
}
