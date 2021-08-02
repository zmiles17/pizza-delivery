using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PizzaDelivery.Models;
using PizzaDelivery.Repos;

namespace PizzaDeliveryTests
{
    class CustomerRepoTests
    {

        ServiceCollection services = new ServiceCollection();
        CustomerRepo customerRepo;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<PizzaDeliveryDbContext>();
            var config = new ConfigurationBuilder().AddJsonFile("C:/Users/ZMiles/classwork-miles-zach/PizzaDelivery/PizzaDeliveryTests/appsettings.test.json").Build();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));
            services.AddDbContext<PizzaDeliveryDbContext>(options => options.UseSqlServer(config.GetConnectionString("TestDb")));
            var context = new PizzaDeliveryDbContext(builder.Options);
            customerRepo = new CustomerRepo(context);
            context.Customers.RemoveRange(context.Customers);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Customers', RESEED, 0)");
        }

        [Test]
        public void AddCustomer()
        {
            Customer customer = new Customer
            {
                Name = "Zack",
                Address = "123 Test St",
                City = "Atlanta",
                State = "GA",
                Zip = "30303",
                Phone = "1234567890"
            };

            customerRepo.Add(customer);

            Assert.That(customer.Id == 1);
            Assert.That(customer.Address == "123 Test St");
            Assert.That(customer.City == "Atlanta");
            Assert.That(customer.State == "GA");
            Assert.That(customer.Zip == "30303");
            Assert.That(customer.Phone == "1234567890");
        }

        [Test]
        public void FindCustomerById()
        {
            Customer customer = new Customer
            {
                Name = "Zack",
                Address = "123 Test St",
                City = "Atlanta",
                State = "GA",
                Zip = "30303",
                Phone = "1234567890"
            };

            customerRepo.Add(customer);

            Assert.AreEqual(customerRepo.FindById(1), customer);
        }

        [Test]
        public void FindAllCustomers()
        {
            Customer customer = new Customer
            {
                Name = "Zack",
                Address = "123 Test St",
                City = "Atlanta",
                State = "GA",
                Zip = "30303",
                Phone = "1234567890"
            };

            customerRepo.Add(customer);

            Assert.That(customerRepo.FindAll().Count == 1);
        }

        [Test]
        public void UpdateCustomer()
        {
            Customer customer = new Customer
            {
                Name = "Zack",
                Address = "123 Test St",
                City = "Atlanta",
                State = "GA",
                Zip = "30303",
                Phone = "1234567890"
            };

            customerRepo.Add(customer);

            customer.Name = "Zack Miles";
            customer.Address = "345 Test Dr";
            customer.City = "Dalton";
            customer.Zip = "34214";

            customerRepo.Update(customer);

            Assert.That(customer.Id == 1);
            Assert.That(customer.Name == "Zack Miles");
            Assert.That(customer.Address == "345 Test Dr");
            Assert.That(customer.City == "Dalton");
            Assert.That(customer.State == "GA");
            Assert.That(customer.Zip == "34214");
            Assert.That(customer.Phone == "1234567890");
        }

        [Test]
        public void RemoveCustomer()
        {
            Customer customer = new Customer
            {
                Name = "Zack",
                Address = "123 Test St",
                City = "Atlanta",
                State = "GA",
                Zip = "30303",
                Phone = "1234567890"
            };

            customerRepo.Add(customer);

            customerRepo.Remove(customer);

            Assert.That(customerRepo.FindAll().Count == 0);
        }
    }
}
