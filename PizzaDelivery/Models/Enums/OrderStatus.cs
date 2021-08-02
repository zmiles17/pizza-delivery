using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Models.Enums
{
    public enum OrderStatus
    {
        ENQUEUED, PREPARING, BAKING, EN_ROUTE, DELIVERED
    }
}
