using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Models
{
    public class OrderItem
    {
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Item Item { get; set; }
    }
}
