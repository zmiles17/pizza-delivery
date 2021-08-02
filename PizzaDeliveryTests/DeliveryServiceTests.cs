using NUnit.Framework;
using PizzaDelivery;
using PizzaDelivery.Exceptions;
using PizzaDelivery.Models;
using PizzaDelivery.Repos;
using PizzaDelivery.Services;
using PizzaDeliveryTests.InMemDaos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDeliveryTests
{
    class DeliveryServiceTests
    {
        PizzaDeliveryService service;

        [SetUp]
        public void Setup()
        {
            IIngredientRepo ingredientRepo = new IngredientInMemDao();
            IItemRepo itemRepo = new ItemInMemDao();
            ICustomerRepo customerRepo = new CustomerInMemDao();
            IOrderRepo orderRepo = new OrderInMemDao();
            IInventoryRepo inventoryRepo = new InventoryInMemDao();
            IStoreRepo storeRepo = new StoreInMemDao();
            service = new PizzaDeliveryService(customerRepo, ingredientRepo, inventoryRepo, itemRepo, orderRepo, storeRepo);
        }

        [Test]
        public void AddIngredient()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "Pepperoni"
            };

            ingredient = service.CreateIngredient(ingredient);

            Assert.That(ingredient.Id == 1);
            Assert.That(ingredient.Name == "Pepperoni");
        }

        [Test]
        public void AddIngredientWithInvalidName()
        {
            string longIngredientName = "";
            for (int i = 0; i <= 100; i++) longIngredientName += i;
            Assert.Throws<InvalidIngredientNameException>(() => service.CreateIngredient(new Ingredient { Name = null }));
            Assert.Throws<InvalidIngredientNameException>(() => service.CreateIngredient(new Ingredient { Name = "" }));
            Assert.Throws<InvalidIngredientNameException>(() => service.CreateIngredient(new Ingredient { Name = longIngredientName}));
        }

        [Test]
        public void AddIngredientWithPrimaryKeyDefined()
        {
            Assert.Throws<PrimaryKeyReferenceException>(() => service.CreateIngredient(new Ingredient { Id = 1, Name = "Jello" }));
        }

        [Test]
        public void FindIngredientById()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "Pepperoni"
            };

            ingredient = service.CreateIngredient(ingredient);

            Ingredient found = service.GetIngredientById(1);

            Assert.AreEqual(ingredient, found);
        }

        [Test]
        public void GetIngredientByIdThatDoesNotExistReturnsNull()
        {
            var result = service.GetIngredientById(2);

            Assert.IsNull(result);
        }

        [Test]
        public void FindAllIngredients()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "Pepperoni"
            };

            ingredient = service.CreateIngredient(ingredient);
            List<Ingredient> ingredients = service.GetAllIngredients();

            Assert.That(ingredients.Count == 1);
            Assert.AreEqual(ingredient, ingredients[0]);
        }

        [Test]
        public void FindAllIngredientsWithNoIngredientsReturnsEmptyList()
        {
            var ingredients = service.GetAllIngredients();
            Assert.That(ingredients.Count == 0);
        }

        [Test]
        public void UpdateIngredient()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "Pepperoni"
            };

            ingredient = service.CreateIngredient(ingredient);

            ingredient.Name = "Dough";

            ingredient = service.UpdateIngredient(ingredient);

            Assert.That(ingredient.Id == 1);
            Assert.That(ingredient.Name == "Dough");
        }

        [Test]
        public void UpdateIngredientToChangeIdThrowsPrimaryKeyReferenceException()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "Pepperoni"
            };

            ingredient = service.CreateIngredient(ingredient);

            ingredient.Id = 2;

            Assert.Throws<PrimaryKeyReferenceException>(() => service.UpdateIngredient(ingredient));
        }

        [Test]
        public void DeleteIngredient()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "Pepperoni"
            };

            ingredient = service.CreateIngredient(ingredient);

            service.DeleteIngredient(ingredient.Id);

            Assert.That(service.GetAllIngredients().Count == 0);
        }
    }
}
