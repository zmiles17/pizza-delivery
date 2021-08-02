using PizzaDelivery.Models;
using PizzaDelivery.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDeliveryTests.InMemDaos
{
    class OrderInMemDao : IOrderRepo
    {
        public Order Add(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> FindAll()
        {
            throw new NotImplementedException();
        }

        public Order FindByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Order FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> FindOrdersByCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public List<Order> FindStoreOrders(int storeId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Order order)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
