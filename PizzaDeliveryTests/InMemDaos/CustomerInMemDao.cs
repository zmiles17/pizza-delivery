using PizzaDelivery.Models;
using PizzaDelivery.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDeliveryTests.InMemDaos
{
    class CustomerInMemDao : ICustomerRepo
    {
        public Customer Add(Customer customer)
        {
            throw new NotImplementedException();
        }

        public List<Customer> FindAll()
        {
            throw new NotImplementedException();
        }

        public Customer FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Customer FindCustomerInfo(string name, string phone)
        {
            throw new NotImplementedException();
        }

        public void Remove(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Customer Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
