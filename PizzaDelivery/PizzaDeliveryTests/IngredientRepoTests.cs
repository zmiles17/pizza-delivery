using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PizzaDelivery.Models;
using PizzaDelivery.Repos;

namespace PizzaDeliveryTests
{
    class IngredientRepoTests
    {

        ServiceCollection services = new ServiceCollection();
        IngredientRepo ingredientRepo;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<PizzaDeliveryDbContext>();
            var config = new ConfigurationBuilder().AddJsonFile("C:/Users/ZMiles/classwork-miles-zach/PizzaDelivery/PizzaDeliveryTests/appsettings.test.json").Build();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));
            services.AddDbContext<PizzaDeliveryDbContext>(options => options.UseSqlServer(config.GetConnectionString("TestDb")));
            var context = new PizzaDeliveryDbContext(builder.Options);
            ingredientRepo = new IngredientRepo(context);
            context.Ingredients.RemoveRange(context.Ingredients);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Ingredients', RESEED, 0)");
        }

        [Test]
        public void AddIngredient()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "Pepperoni"
            };

            ingredientRepo.Add(ingredient);

            Assert.That(ingredient.Id == 1);
            Assert.That(ingredient.Name == "Pepperoni");
        }

        [Test]
        public void FindIngredientById()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "Pepperoni"
            };

            ingredientRepo.Add(ingredient);

            Assert.AreEqual(ingredientRepo.FindById(1), ingredient);
        }

        [Test]
        public void FindAllIngredients()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "Pepperoni"
            };

            ingredientRepo.Add(ingredient);

            Assert.That(ingredientRepo.FindAll().Count == 1);
        }

        [Test]
        public void UpdateIngredient()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "Pepperoni"
            };

            ingredientRepo.Add(ingredient);

            ingredient.Name = "Dough";

            ingredientRepo.Update(ingredient);

            Assert.That(ingredient.Id == 1);
            Assert.That(ingredient.Name == "Dough");
        }

        [Test]
        public void DeleteIngredient()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "Pepperoni"
            };

            ingredientRepo.Add(ingredient);

            ingredientRepo.Remove(ingredient);

            Assert.That(ingredientRepo.FindAll().Count == 0);
        }
    }
}
