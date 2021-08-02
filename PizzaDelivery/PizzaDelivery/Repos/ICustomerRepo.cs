using PizzaDelivery.Models;
using System.Collections.Generic;

namespace PizzaDelivery.Repos
{
    public interface ICustomerRepo
    {
        Customer Add(Customer customer);
        List<Customer> FindAll();
        Customer FindById(int id);
        Customer FindCustomerInfo(string name, string phone);
        void Remove(Customer customer);
        Customer Update(Customer customer);
    }
}