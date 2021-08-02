using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PizzaDelivery.Exceptions;
using PizzaDelivery.Models;
using PizzaDelivery.Repos;
using PizzaDelivery.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PizzaDelivery.Services
{
    public class PizzaDeliveryService
    {
        ICustomerRepo _customerRepo;
        IIngredientRepo _ingredientRepo;
        IInventoryRepo _inventoryRepo;
        IItemRepo _itemRepo;
        IOrderRepo _orderRepo;
        IStoreRepo _storeRepo;
        static readonly HttpClient client = new HttpClient();

        public PizzaDeliveryService(ICustomerRepo customerRepo, IIngredientRepo ingredientRepo, IInventoryRepo inventoryRepo, IItemRepo itemRepo,
            IOrderRepo orderRepo, IStoreRepo storeRepo)
        {
            _customerRepo = customerRepo;
            _ingredientRepo = ingredientRepo;
            _inventoryRepo = inventoryRepo;
            _itemRepo = itemRepo;
            _orderRepo = orderRepo;
            _storeRepo = storeRepo;
        }

        public Ingredient GetIngredientById(int id)
        {
            return _ingredientRepo.FindById(id);
        }

        public Order GetOrderByGuid(Guid guid)
        {
            return _orderRepo.FindByGuid(guid);
        }

        //public List<Item> GetItemsForOrder(int orderId)
        //{
        //    return _itemRepo.FindItemsForOrder(orderId);
        //}

        public Customer GetPreviousCustomerInfo(PreviousCustomerRequest request)
        {
            return _customerRepo.FindCustomerInfo(request.Name, request.Phone);
        }

        public List<Order> GetOrdersByCustomer(int customerId)
        {
            return _orderRepo.FindOrdersByCustomer(customerId);
        }

        public List<Order> GetStoreOrders(int storeId)
        {
            return _orderRepo.FindStoreOrders(storeId);
        }

        public List<Ingredient> GetIngredientsForItem(int itemId)
        {
            return _ingredientRepo.FindIngredientsForItem(itemId);
        }

        public List<Ingredient> GetAllIngredients()
        {
            return _ingredientRepo.FindAll();
        }

        public List<Inventory> GetInventoryByStore(int storeId)
        {
            return _inventoryRepo.FindInventoryForStore(storeId);
        }

        public Ingredient CreateIngredient(Ingredient ingredient)
        {
            if (ingredient.Id != 0) 
                throw new PrimaryKeyReferenceException("Primary identifier must not be defined.");
            if (ingredient.Name == null || ingredient.Name == "" || ingredient.Name.Length > 100) 
                throw new InvalidIngredientNameException("Invalid Ingredient Name");
            return _ingredientRepo.Add(ingredient);
        }

        public Ingredient UpdateIngredient(Ingredient ingredient)
        {
            if (GetIngredientById(ingredient.Id) == null) 
                throw new PrimaryKeyReferenceException($"Ingredient with ID of {ingredient.Id} was not found");
            else return _ingredientRepo.Update(ingredient);
        }

        public void DeleteIngredient(int id)
        {
            Ingredient ingredient = new Ingredient { Id = id };
            _ingredientRepo.Remove(ingredient);
        }

        public Item GetItemById(int id)
        {
            return _itemRepo.FindById(id);
        }

        public Item CreateItem(Item item)
        {
            return _itemRepo.Add(item);
        }

        public List<Item> GetAllItems()
        {
            return _itemRepo.FindAll();
        }

        public Item UpdateItem(Item item)
        {
            return _itemRepo.Update(item);
        }

        public void DeleteItem(int id)
        {
            Item item = new Item { Id = id };
            _itemRepo.Remove(item);
        }

        public Inventory GetInventoryById(int id)
        {
            return _inventoryRepo.FindById(id);
        }

        public List<Inventory> GetAllInventory()
        {
            return _inventoryRepo.FindAll();
        }

        public Inventory CreateInventory(Inventory inventory)
        {
            return _inventoryRepo.Add(inventory);
        }

        public Inventory UpdateInventory(Inventory inventory)
        {
            return _inventoryRepo.Update(inventory);
        }

        public Customer GetCustomerById(int id)
        {
            return _customerRepo.FindById(id);
        }

        public void DeleteInventory(int id)
        {
            Inventory inventory = new Inventory { Id = id };
            _inventoryRepo.Remove(inventory);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.FindAll();
        }

        public Customer CreateCustomer(Customer customer)
        {
            return _customerRepo.Add(customer);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            return _customerRepo.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            Customer customer = new Customer { Id = id };
            _customerRepo.Remove(customer);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            List<Store> storesByZip = _storeRepo.FindByZip(order.Customer.Zip);
            if(storesByZip.Count == 0)
            {
                var zipcodeApiRequest = await client.GetAsync($"https://www.zipcodeapi.com/rest/fgMQXzoHcQOKR5bgSK9M6bcDHSfyJDSgTlgXxxquj5kRQMqkGWZWW392C7XyOM1y/radius.json/{order.Customer.Zip}/5/mile");
                zipcodeApiRequest.EnsureSuccessStatusCode();
                string json = await zipcodeApiRequest.Content.ReadAsStringAsync();
                var zipcodes = JsonConvert.DeserializeObject<Zipcodes>(json);
                List<Zipcode> nearbyZipcodes = zipcodes.zip_codes.OrderBy(zip => zip.distance).ToList();
                List<Store> nearbyStoresByZip = new List<Store>();
                for(int i = 0; i < nearbyZipcodes.Count && nearbyStoresByZip.Count == 0; i++)
                {
                    nearbyStoresByZip = _storeRepo.FindByZip(nearbyZipcodes[i].zip_code);
                }
                order.StoreId = nearbyStoresByZip[0].Id;
                }
            else
            {
                order.StoreId = storesByZip[0].Id;
            }
            // Check to see if store has enough ingredients to make order items
            return _orderRepo.Add(order);
        }

        public Order UpdateOrder(Order order)
        {
            return _orderRepo.Update(order);
        }

        public void DeleteOrder(int id)
        {
            Order order = new Order { Id = id };
            _orderRepo.Remove(order);
        }

        public Order GetOrderById(int id)
        {
            return _orderRepo.FindById(id);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepo.FindAll();
        }

        public Store AddStore(Store store)
        {
            return _storeRepo.Add(store);
        }

        public Store UpdateStore(Store store)
        {
            return _storeRepo.Update(store);
        }

        public void RemoveStore(int id)
        {
            Store store = new Store { Id = id };
            try
            {
                _storeRepo.Remove(store);
            } 
            catch(DbUpdateConcurrencyException e)
            {
                throw new StoreNotFoundException($"Store with Id of {id} does not exist.", e);
            }
        }

        public Store GetStoreById(int id)
        {
            return _storeRepo.FindById(id);
        }

        public List<Store> GetAllStores()
        {
            return _storeRepo.FindAll();
        }
    }
}
