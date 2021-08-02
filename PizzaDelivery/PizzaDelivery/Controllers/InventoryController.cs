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
    [Route("/api/inventory")]
    public class InventoryController : Controller
    {

        PizzaDeliveryService _service;

        public InventoryController(PizzaDeliveryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllInventory()
        {
            return Ok(_service.GetAllInventory());
        }

        [HttpGet("{id}")]
        public IActionResult GetInventoryById(int id)
        {
            return Ok(_service.GetInventoryById(id));
        }

        [HttpGet("store/{id}")]
        public IActionResult GetInventoryByStore(int storeId)
        {
            return Ok(_service.GetInventoryByStore(storeId));
        }

        [HttpPost]
        public IActionResult CreateInventory(Inventory inventory)
        {
            return Created("/api/inventory", _service.CreateInventory(inventory));
        }

        [HttpPut]
        public IActionResult UpdateInventory(Inventory inventory)
        {
            return Accepted(_service.UpdateInventory(inventory));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInventory(int id)
        {
            _service.DeleteInventory(id);
            return NoContent();
        }
    }
}
