using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Models
{
    public class Zipcodes
    {
        [JsonProperty("zip_codes")]
        public IEnumerable<Zipcode> zip_codes { get; set; }
    }
}
