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
    [Route("/api/ingredient")]
    public class IngredientController : Controller
    {

        PizzaDeliveryService _service;

        public IngredientController(PizzaDeliveryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllIngredients()
        {
            return Ok(_service.GetAllIngredients());
        }

        [HttpGet("{id}")]
        public IActionResult GetIngredientById(int id)
        {
            return Ok(_service.GetIngredientById(id));
        }

        [HttpGet("item/{id}")]
        public IActionResult GetIngredientsForItem(int itemId)
        {
            return Ok(_service.GetIngredientsForItem(itemId));
        }

        [HttpPost]
        public IActionResult CreateIngredient(Ingredient ingredient)
        {
            return Created("/api/ingredient", _service.CreateIngredient(ingredient));
        }

        [HttpPut]
        public IActionResult UpdateIngredient(Ingredient ingredient)
        {
            return Accepted(_service.UpdateIngredient(ingredient));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIngredient(int id)
        {
            _service.DeleteIngredient(id);
            return NoContent();
        }
    }
}
