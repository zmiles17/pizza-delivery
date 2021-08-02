using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PizzaDelivery.Models;
using PizzaDelivery.Repos;

namespace PizzaDeliveryTests
{
    class ItemRepoTests
    {

        ServiceCollection services = new ServiceCollection();
        ItemRepo itemRepo;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<PizzaDeliveryDbContext>();
            var config = new ConfigurationBuilder().AddJsonFile("C:/Users/ZMiles/classwork-miles-zach/PizzaDelivery/PizzaDeliveryTests/appsettings.test.json").Build();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));
            services.AddDbContext<PizzaDeliveryDbContext>(options => options.UseSqlServer(config.GetConnectionString("TestDb")));
            var context = new PizzaDeliveryDbContext(builder.Options);
            itemRepo = new ItemRepo(context);
            context.Items.RemoveRange(context.Items);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Items', RESEED, 0)");
        }

        [Test]
        public void AddItem()
        {
            Item item = new Item
            {
                Name = "Pepperoni Pizza",
                Price = 9.99M,
                ImageUrl = "https://imgur.com/test"
            };

            itemRepo.Add(item);

            Assert.That(item.Id == 1);
            Assert.That(item.Name == "Pepperoni Pizza");
            Assert.That(item.Price == 9.99M);
            Assert.That(item.ImageUrl == "https://imgur.com/test");
        }

        [Test]
        public void FindItemById()
        {
            Item item = new Item
            {
                Name = "Pepperoni Pizza",
                Price = 9.99M,
                ImageUrl = "https://imgur.com/test"
            };

            itemRepo.Add(item);

            Item retrievedItem = itemRepo.FindById(item.Id);

            Assert.AreEqual(retrievedItem, item);
        }

        [Test]
        public void FindAllItems()
        {
            Item item = new Item
            {
                Name = "Pepperoni Pizza",
                Price = 9.99M,
                ImageUrl = "https://imgur.com/test"
            };

            itemRepo.Add(item);

            Assert.That(itemRepo.FindAll().Count == 1);
        }

        [Test]
        public void UpdateItem()
        {
            Item item = new Item
            {
                Name = "Pepperoni Pizza",
                Price = 9.99M,
                ImageUrl = "https://imgur.com/test"
            };

            itemRepo.Add(item);

            item.Name = "Large Pepperoni Pizza";
            item.Price = 11.99M;
            item.ImageUrl = "https://imgur.com/largepepperoni";

            itemRepo.Update(item);

            Assert.That(item.Id == 1);
            Assert.That(item.Name == "Large Pepperoni Pizza");
            Assert.That(item.Price == 11.99M);
            Assert.That(item.ImageUrl == "https://imgur.com/largepepperoni");
        }

        [Test]
        public void RemoveItem()
        {
            Item item = new Item
            {
                Name = "Pepperoni Pizza",
                Price = 9.99M,
                ImageUrl = "https://imgur.com/test"
            };

            itemRepo.Add(item);

            itemRepo.Remove(item);

            Assert.That(itemRepo.FindAll().Count == 0);
        }
    }
}
