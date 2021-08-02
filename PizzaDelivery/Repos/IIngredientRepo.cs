using PizzaDelivery.Models;
using System.Collections.Generic;

namespace PizzaDelivery.Repos
{
    public interface IIngredientRepo
    {
        Ingredient Add(Ingredient ingredient);
        List<Ingredient> FindAll();
        Ingredient FindById(int id);
        List<Ingredient> FindIngredientsForItem(int itemId);
        void Remove(Ingredient ingredient);
        Ingredient Update(Ingredient ingredient);
    }
}