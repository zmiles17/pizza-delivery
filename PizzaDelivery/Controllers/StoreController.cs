using Microsoft.AspNetCore.Mvc;
using PizzaDelivery.Models;
using PizzaDelivery.Repos;
using PizzaDelivery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Controllers
{
    [ApiController]
    [Route("/api/store")]
    public class StoreController : Controller
    {

        PizzaDeliveryService _service;

        public StoreController(PizzaDeliveryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllStores()
        {
            return Ok(_service.GetAllStores());
        }

        [HttpGet("{id}")]
        public IActionResult GetStoreById(int id)
        {
            return Ok(_service.GetStoreById(id));
        }

        [HttpPost]
        public IActionResult AddStore(Store store)
        {
            return Created("/api/store", _service.AddStore(store));
        }

        [HttpPut]
        public IActionResult UpdateStore(Store store)
        {
            return Accepted(_service.UpdateStore(store));
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveStore(int id)
        {
            _service.RemoveStore(id);
            return NoContent();
        }
    }
}
