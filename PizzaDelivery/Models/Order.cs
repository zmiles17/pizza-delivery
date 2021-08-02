using PizzaDelivery.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public DateTime? TimeInOven { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Customer Customer { get; set; }
        public Store Store { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public Order() { }
        public Order(Order copy)
        {
            Id = copy.Id;
            TimeIn = copy.TimeIn;
            TimeOut = copy.TimeOut;
            DeliveryTime = copy.DeliveryTime;
            Customer = new Customer(copy.Customer);
            //OrderItems = copy.OrderItems.Select(item => new OrderItem(item)).ToList();
        }
    }
}
