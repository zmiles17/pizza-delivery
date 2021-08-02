using PizzaDelivery.Models;
using System;
using System.Collections.Generic;

namespace PizzaDelivery.Repos
{
    public interface IOrderRepo
    {
        Order Add(Order order);
        List<Order> FindAll();
        Order FindByGuid(Guid guid);
        Order FindById(int id);
        List<Order> FindOrdersByCustomer(int customerId);
        void Remove(Order order);
        Order Update(Order order);
        List<Order> FindStoreOrders(int storeId);
    }
}