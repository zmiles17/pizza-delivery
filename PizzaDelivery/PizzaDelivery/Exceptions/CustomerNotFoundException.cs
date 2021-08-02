using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string message, Exception ex) : base(message, ex) { }
        public CustomerNotFoundException(string message) : base(message) { }
    }
}
