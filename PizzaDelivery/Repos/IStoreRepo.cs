using PizzaDelivery.Models;
using System.Collections.Generic;

namespace PizzaDelivery.Repos
{
    public interface IStoreRepo
    {
        Store Add(Store store);
        List<Store> FindAll();
        List<Store> FindByCityAndState(string city, string state);
        Store FindById(int id);
        List<Store> FindByZip(string zip);
        void Remove(Store store);
        Store Update(Store store);
    }
}