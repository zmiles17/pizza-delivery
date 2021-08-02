using PizzaDelivery.Models;
using System.Collections.Generic;

namespace PizzaDelivery.Repos
{
    public interface IItemRepo
    {
        Item Add(Item item);
        List<Item> FindAll();
        Item FindById(int id);
        void Remove(Item item);
        Item Update(Item item);
    }
}