using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Exceptions
{
    public class PrimaryKeyReferenceException : Exception
    {
        public PrimaryKeyReferenceException(string message) : base(message) { }

        public PrimaryKeyReferenceException(string message, Exception ex) : base(message, ex) { }
    }
}
