using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PizzaDelivery.Models;
using PizzaDelivery.Models.Enums;
using PizzaDelivery.Repos;
using PizzaDelivery.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PizzaDelivery.Controllers
{
    [ApiController]
    [Route("/api/order")]
    public class OrderController : Controller
    {

        PizzaDeliveryService _service;
        static readonly HttpClient client = new HttpClient();

        public OrderController(PizzaDeliveryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_service.GetAllOrders());
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            return Ok(_service.GetOrderById(id));
        }

        [HttpGet("tracking/{guid}")]
        public IActionResult GetOrderByGuid(Guid guid)
        {
            return Ok(_service.GetOrderByGuid(guid));
        }

        [HttpGet("customer/{id}")]
        public IActionResult GetOrdersByCustomer(int customerId)
        {
            return Ok(_service.GetOrdersByCustomer(customerId));
        }

        [HttpGet("store/{id}")]
        public IActionResult GetStoreOrders(int storeId)
        {
            return Ok(_service.GetStoreOrders(storeId));
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            Order added = _service.CreateOrderAsync(order).Result;
            StartOrder(order);
            return Created("/api/order", added);
        }

        [HttpPut]
        public IActionResult UpdateOrder(Order order)
        {
            return Accepted(_service.UpdateOrder(order));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _service.DeleteOrder(id);
            return NoContent();
        }

        async void StartOrder(Order order)
        {
            //int delay = 6000;
            //if((order.TimeIn - DateTime.UtcNow).TotalMinutes > 30)
            //{
            //    delay = (int) ((order.TimeIn - DateTime.UtcNow).TotalMinutes - 30) * 60_000;
            //}
            await Task.Delay(15000); 

            order.OrderStatus = OrderStatus.PREPARING;
            
            var options = new DbContextOptionsBuilder<PizzaDeliveryDbContext>().UseSqlServer("Server=localhost;Database=PizzaDelivery;Trusted_Connection=true;").Options;
            //var service = new PizzaDeliveryService(new PizzaDeliveryDbContext(options));
            _service.UpdateOrder(order);
            //Figure out delivery time by using distance matrix API
            var origin = order.Store.Address.Replace(' ', '+') + "+" + order.Store.City + "+" + order.Store.State;
            var destination = order.Customer.Address.Replace(' ', '+') + "+" + order.Customer.City + "+" + order.Customer.State;
            var distanceMatrixRequest = await client.GetAsync(requestUri: $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&arrival_time={order.TimeIn.Ticks}&key=AIzaSyC0ulL_bmbNuY57-5Y61CvftToI9_vc6uc"); //fill in missing params
            distanceMatrixRequest.EnsureSuccessStatusCode();
            var response = distanceMatrixRequest.Content.ReadAsStringAsync();
            JsonTextReader reader = new JsonTextReader(new StringReader(response.Result));
            long deliveryTimeInSeconds = long.MaxValue;
            while(reader.Read())
            {
                if (reader.Value != null && reader.TokenType == JsonToken.PropertyName && reader.Value.ToString() == "duration")
                {
                    for(int i = 0; i <= 4; i++)
                    {
                        reader.Read();
                    }
                    deliveryTimeInSeconds = (long) reader.Value;
                    break;
                }
            }

            //await Task.Delay(300_000);
            await Task.Delay(15000);
            if ((order.TimeIn - DateTime.UtcNow).TotalMinutes <= 30)
                order.TimeInOven = DateTime.UtcNow.AddMinutes(3);
            else
                order.TimeInOven = order.TimeIn.AddMinutes(-27);
            order.OrderStatus = OrderStatus.BAKING;
            _service.UpdateOrder(order);

            //await Task.Delay(600_000);
            //order.TimeOut = DateTime.UtcNow;
            await Task.Delay(15000);
            order.TimeOut = order.TimeInOven.Value.AddMinutes(7.5);
            order.OrderStatus = OrderStatus.EN_ROUTE;
            _service.UpdateOrder(order);

            //await Task.Delay((int)(deliveryTimeInSeconds * 1000));
            //order.DeliveryTime = DateTime.UtcNow;
            await Task.Delay(15000);
            order.DeliveryTime = order.TimeOut.Value.AddSeconds(deliveryTimeInSeconds);
            order.OrderStatus = OrderStatus.DELIVERED;
            _service.UpdateOrder(order);
        }
    }
}
