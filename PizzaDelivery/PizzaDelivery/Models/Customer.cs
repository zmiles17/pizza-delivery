using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaDelivery.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }

        public List<Order> Orders { get; set; }

        public Customer() { }
        public Customer(Customer copy)
        {
            Id = copy.Id;
            Name = copy.Name;
        }

    }
}