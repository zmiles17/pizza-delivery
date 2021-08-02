using PizzaDelivery.Models;
using PizzaDelivery.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDeliveryTests.InMemDaos
{
    class InventoryInMemDao : IInventoryRepo
    {
        public Inventory Add(Inventory inventory)
        {
            throw new NotImplementedException();
        }

        public List<Inventory> FindAll()
        {
            throw new NotImplementedException();
        }

        public Inventory FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Inventory> FindInventoryForStore(int storeId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Inventory inventory)
        {
            throw new NotImplementedException();
        }

        public Inventory Update(Inventory inventory)
        {
            throw new NotImplementedException();
        }
    }
}
