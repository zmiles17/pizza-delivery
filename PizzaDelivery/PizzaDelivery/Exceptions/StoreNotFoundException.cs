using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Exceptions
{
    public class StoreNotFoundException : Exception
    {
        public StoreNotFoundException(string message) : base(message) { }
        public StoreNotFoundException(string message, Exception ex) : base(message, ex) { }
    }
}
