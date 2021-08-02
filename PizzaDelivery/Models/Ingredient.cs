using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace PizzaDelivery.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public List<ItemIngredient> ItemIngredients { get; set; }

        public Ingredient() { }
        public Ingredient(Ingredient copy)
        {
            Id = copy.Id;
            Name = copy.Name;
            //Items = copy.Items.Select(item => new Item(item)).ToList();
        }

        public override bool Equals(object obj)
        {
            return obj is Ingredient ingredient &&
                   Id == ingredient.Id &&
                   Name == ingredient.Name &&
                   EqualityComparer<List<ItemIngredient>>.Default.Equals(ItemIngredients, ingredient.ItemIngredients);
        }
    }
}