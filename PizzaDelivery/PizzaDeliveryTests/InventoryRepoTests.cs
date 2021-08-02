using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PizzaDelivery.Models;
using PizzaDelivery.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDeliveryTests
{
    class InventoryRepoTests
    {

        ServiceCollection services = new ServiceCollection();
        InventoryRepo InventoryRepo;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<PizzaDeliveryDbContext>();
            var config = new ConfigurationBuilder().AddJsonFile("C:/Users/ZMiles/classwork-miles-zach/PizzaDelivery/PizzaDeliveryTests/appsettings.test.json").Build();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));
            services.AddDbContext<PizzaDeliveryDbContext>(options => options.UseSqlServer(config.GetConnectionString("TestDb")));
            var context = new PizzaDeliveryDbContext(builder.Options);
            InventoryRepo = new InventoryRepo(context);
            context.Inventories.RemoveRange(context.Inventories);
            context.Ingredients.RemoveRange(context.Ingredients);
            context.Stores.RemoveRange(context.Stores);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Inventories', RESEED, 0)");
        }

        [Test]
        public void AddInventory()
        {
            Inventory inventory = new Inventory
            {
                Quantity = 50,
                Ingredient = new Ingredient
                {
                    Name = "Cheese"
                },
                Store = new Store
                {
                    StoreNumber = "1",
                    Address = "123 Hunt Avenue",
                    City = "Mobile",
                    State = "AL",
                    Zip = "45354",
                    Phone = "3432432423"
                }
            };

            InventoryRepo.Add(inventory);

            Assert.That(inventory.Id == 1);
            Assert.IsNotNull(inventory.StoreId);
            Assert.IsNotNull(inventory.IngredientId);
            Assert.That(inventory.Quantity == 50);
        }

        [Test]
        public void FindInventoryById()
        {
            Inventory inventory = new Inventory
            {
                Quantity = 50,
                Ingredient = new Ingredient
                {
                    Name = "Cheese"
                },
                Store = new Store
                {
                    StoreNumber = "1",
                    Address = "123 Hunt Avenue",
                    City = "Mobile",
                    State = "AL",
                    Zip = "45354",
                    Phone = "3432432423"
                }
            };

            InventoryRepo.Add(inventory);

            Assert.AreEqual(InventoryRepo.FindById(1), inventory);
        }

        [Test]
        public void FindAllInventory()
        {
            Inventory inventory = new Inventory
            {
                Quantity = 50,
                Ingredient = new Ingredient
                {
                    Name = "Cheese"
                },
                Store = new Store
                {
                    StoreNumber = "1",
                    Address = "123 Hunt Avenue",
                    City = "Mobile",
                    State = "AL",
                    Zip = "45354",
                    Phone = "3432432423"
                }
            };

            InventoryRepo.Add(inventory);

            List<Inventory> inventories = InventoryRepo.FindAll();
            Assert.That(inventories.Count == 1);
            Assert.That(inventories[0] == inventory);
        }

        [Test]
        public void UpdateInventory()
        {
            Inventory inventory = new Inventory
            {
                Quantity = 50,
                Ingredient = new Ingredient
                {
                    Name = "Cheese"
                },
                Store = new Store
                {
                    StoreNumber = "1",
                    Address = "123 Hunt Avenue",
                    City = "Mobile",
                    State = "AL",
                    Zip = "45354",
                    Phone = "3432432423"
                }
            };

            InventoryRepo.Add(inventory);

            inventory.Store = new Store
            {
                StoreNumber = "2",
                Address = "234 Yellow Lane",
                City = "Montgomery",
                State = "AL",
                Zip = "40012",
                Phone = "1029348756"
            };

            inventory.Quantity = 30;

            InventoryRepo.Update(inventory);

            Assert.That(inventory.Quantity == 30);
            Assert.That(inventory.Ingredient.Name == "Cheese");
            Assert.That(inventory.Store.StoreNumber == "2");
            Assert.That(inventory.Store.Address == "234 Yellow Lane");
            Assert.That(inventory.Store.City == "Montgomery");
            Assert.That(inventory.Store.State == "AL");
            Assert.That(inventory.Store.Zip == "40012");
            Assert.That(inventory.Store.Phone == "1029348756");
        }

        [Test]
        public void RemoveInventory()
        {
            Inventory inventory = new Inventory
            {
                Quantity = 50,
                Ingredient = new Ingredient
                {
                    Name = "Cheese"
                },
                Store = new Store
                {
                    StoreNumber = "1",
                    Address = "123 Hunt Avenue",
                    City = "Mobile",
                    State = "AL",
                    Zip = "45354",
                    Phone = "3432432423"
                }
            };

            InventoryRepo.Add(inventory);

            InventoryRepo.Remove(inventory);

            Assert.That(InventoryRepo.FindAll().Count == 0);
        }
    }
}
