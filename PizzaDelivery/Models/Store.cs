using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Models
{
    public class Store
    {
        public int Id { get; set; }

        public string StoreNumber { get; set; }
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
        public List<Inventory> Inventory { get; set; }
        public List<Order> Orders { get; set; }
    }
}
