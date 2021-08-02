using PizzaDelivery.Models;
using PizzaDelivery.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaDeliveryTests.InMemDaos
{
    class IngredientInMemDao : IIngredientRepo
    {
        List<Ingredient> ingredients = new List<Ingredient>();

        public Ingredient Add(Ingredient ingredient)
        {
            if (ingredients.Count == 0) ingredient.Id = 1;
            else
            {
                ingredient.Id = ingredients.TakeLast(1).Single().Id + 1;
            }
            ingredients.Add(new Ingredient(ingredient));
            return ingredient;
        }

        public Ingredient FindById(int id)
        {
            return ingredients.Where(i => i.Id == id).SingleOrDefault();
        }

        public List<Ingredient> FindAll()
        {
            return ingredients;
        }

        public Ingredient Update(Ingredient ingredient)
        {
            int index = ingredients.FindIndex(ing => ing.Id == ingredient.Id);
            ingredients.RemoveAt(index);
            ingredients.Insert(index, new Ingredient(ingredient));
            return ingredient;
        }

        public List<Ingredient> FindIngredientsForItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Ingredient ingredient)
        {
            ingredients = ingredients.Where(ing => ing.Id != ingredient.Id).ToList();
        }
    }
}
