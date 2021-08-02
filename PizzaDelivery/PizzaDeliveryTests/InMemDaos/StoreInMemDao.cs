using PizzaDelivery.Models;
using PizzaDelivery.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDeliveryTests.InMemDaos
{
    class StoreInMemDao : IStoreRepo
    {
        public Store Add(Store store)
        {
            throw new NotImplementedException();
        }

        public List<Store> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<Store> FindByCityAndState(string city, string state)
        {
            throw new NotImplementedException();
        }

        public Store FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Store> FindByZip(string zip)
        {
            throw new NotImplementedException();
        }

        public void Remove(Store store)
        {
            throw new NotImplementedException();
        }

        public Store Update(Store store)
        {
            throw new NotImplementedException();
        }
    }
}
