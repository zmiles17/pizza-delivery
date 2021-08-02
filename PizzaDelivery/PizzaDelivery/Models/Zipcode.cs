using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Models
{
    public class Zipcode
    {
        [JsonProperty("zip_code")]
        public string zip_code { get; set; }
        [JsonProperty("distance")]
        public decimal distance { get; set; }
        [JsonProperty("city")]
        public string city { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
    }
}
