using Microsoft.EntityFrameworkCore;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Repos
{
    public class IngredientRepo : IIngredientRepo
    {
        private PizzaDeliveryDbContext context;

        public IngredientRepo(PizzaDeliveryDbContext context)
        {
            this.context = context;
        }

        public void Remove(Ingredient ingredient)
        {
            context.Ingredients.Remove(ingredient);
            context.SaveChanges();
        }

        public Ingredient Update(Ingredient ingredient)
        {
            context.Attach(ingredient);
            context.Entry(ingredient).State = EntityState.Modified;
            context.SaveChanges();
            return ingredient;
        }

        public Ingredient Add(Ingredient ingredient)
        {
            context.Ingredients.Add(ingredient);
            context.SaveChanges();
            return ingredient;
        }

        public List<Ingredient> FindAll()
        {
            return context.Ingredients
                .ToList();
        }

        public Ingredient FindById(int id)
        {
            return context.Ingredients
                .Where(ingredient => ingredient.Id == id)
                .FirstOrDefault();
        }

        public List<Ingredient> FindIngredientsForItem(int itemId)
        {
            return context.ItemIngredients
                .Where(ig => ig.ItemId == itemId)
                .Include(ig => ig.Ingredient)
                .Select(ig => new Ingredient(ig.Ingredient))
                .ToList();

        }
    }
}
