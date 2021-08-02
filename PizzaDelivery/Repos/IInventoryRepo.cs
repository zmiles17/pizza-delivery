using PizzaDelivery.Models;
using System.Collections.Generic;

namespace PizzaDelivery.Repos
{
    public interface IInventoryRepo
    {
        Inventory Add(Inventory inventory);
        List<Inventory> FindAll();
        Inventory FindById(int id);
        List<Inventory> FindInventoryForStore(int storeId);
        void Remove(Inventory inventory);
        Inventory Update(Inventory inventory);
    }
}