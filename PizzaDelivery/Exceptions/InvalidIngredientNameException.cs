using System;

namespace PizzaDelivery
{
    public class InvalidIngredientNameException : Exception
    {

        public InvalidIngredientNameException(string message) : base(message)
        {

        }

        public InvalidIngredientNameException(string message, Exception ex) : base(message, ex) { }
    }
}