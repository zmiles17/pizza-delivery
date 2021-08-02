using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Exceptions
{
    public class InventoryOutOfStockException : Exception
    {
        public InventoryOutOfStockException(string message, Exception ex) : base(message, ex) { }
        public InventoryOutOfStockException(string message) : base(message) { }
    }
}
