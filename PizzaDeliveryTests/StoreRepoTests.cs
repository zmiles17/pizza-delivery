using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PizzaDelivery.Models;
using PizzaDelivery.Repos;
using System.Collections.Generic;

namespace PizzaDeliveryTests
{
    public class StoreRepoTests
    {

        ServiceCollection services = new ServiceCollection();
        StoreRepo storeRepo;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<PizzaDeliveryDbContext>();
            var config = new ConfigurationBuilder().AddJsonFile("C:/Users/ZMiles/classwork-miles-zach/PizzaDelivery/PizzaDeliveryTests/appsettings.test.json").Build();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));
            services.AddDbContext<PizzaDeliveryDbContext>(options => options.UseSqlServer(config.GetConnectionString("TestDb")));
            var context = new PizzaDeliveryDbContext(builder.Options);
            storeRepo = new StoreRepo(context);
            context.Stores.RemoveRange(context.Stores);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Stores', RESEED, 0)");
        }

        [Test]
        public void AddStore()
        {
            Store store = new Store
            {
                StoreNumber = "1",
                Address = "123 Hunt Avenue",
                City = "Mobile",
                State = "AL",
                Zip = "45354",
                Phone = "3432432423"
            };

            storeRepo.Add(store);

            Assert.That(store.Id == 1);
            Assert.AreEqual(store.StoreNumber, "1");
            Assert.AreEqual(store.Address, "123 Hunt Avenue");
            Assert.AreEqual(store.City, "Mobile");
            Assert.AreEqual(store.State, "AL");
            Assert.AreEqual(store.Zip, "45354");
            Assert.AreEqual(store.Phone, "3432432423");
        }

        [Test]
        public void GetStoreById()
        {
            Store store = new Store
            {
                StoreNumber = "1",
                Address = "123 Hunt Avenue",
                City = "Mobile",
                State = "AL",
                Zip = "45354",
                Phone = "3432432423"
            };

            Store addedStore = storeRepo.Add(store);

            Store retrievedStore = storeRepo.FindById(addedStore.Id);

            Assert.AreEqual(retrievedStore, addedStore);
        }

        [Test]
        public void GetAllStores()
        {
            Store store = new Store
            {
                StoreNumber = "1",
                Address = "123 Hunt Avenue",
                City = "Mobile",
                State = "AL",
                Zip = "45354",
                Phone = "3432432423"
            };

            storeRepo.Add(store);

            List<Store> allStores = storeRepo.FindAll();

            Assert.That(allStores.Count == 1);
        }

        [Test]
        public void UpdateStore()
        {
            Store store = new Store
            {
                StoreNumber = "1",
                Address = "123 Hunt Avenue",
                City = "Mobile",
                State = "AL",
                Zip = "45354",
                Phone = "3432432423"
            };

            storeRepo.Add(store);

            store.StoreNumber = "2";
            store.Address = "345 1st St";
            store.City = "New York";
            store.State = "NY";
            store.Zip = "11111";
            store.Phone = "1234567890";

            storeRepo.Update(store);

            Assert.That(store.Id == 1);
            Assert.That(store.StoreNumber == "2");
            Assert.That(store.Address == "345 1st St");
            Assert.That(store.City == "New York");
            Assert.That(store.State == "NY");
            Assert.That(store.Zip == "11111");
            Assert.That(store.Phone == "1234567890");
        }

        [Test]
        public void DeleteStore()
        {
            Store store = new Store
            {
                StoreNumber = "1",
                Address = "123 Hunt Avenue",
                City = "Mobile",
                State = "AL",
                Zip = "45354",
                Phone = "3432432423"
            };

            storeRepo.Add(store);

            storeRepo.Remove(store);

            Assert.That(storeRepo.FindAll().Count == 0);
        }

        [Test]
        public void FindByZip()
        {
            Store store = new Store
            {
                StoreNumber = "1",
                Address = "123 Hunt Avenue",
                City = "Mobile",
                State = "AL",
                Zip = "45354",
                Phone = "3432432423"
            };

            storeRepo.Add(store);

            Assert.That(storeRepo.FindByZip("45354").Count == 1);
        }

        [Test]
        public void FindByCityAndState()
        {
            Store store = new Store
            {
                StoreNumber = "1",
                Address = "123 Hunt Avenue",
                City = "Mobile",
                State = "AL",
                Zip = "45354",
                Phone = "3432432423"
            };

            storeRepo.Add(store);

            Assert.That(storeRepo.FindByCityAndState("Mobile", "AL").Count == 1);
        }
    }
}