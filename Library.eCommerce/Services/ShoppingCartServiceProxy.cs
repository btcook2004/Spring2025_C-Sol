using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using Library.eCommerce.Models;
using Library.eCommerce.Util;
using Library.eCommerce.Utilities;
using Newtonsoft.Json;
using Spring2025_P1.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartServiceProxy
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private List<Item> items;
        public List<Item> CartItems
        {
            get
            {
                return items;
            }
        }
        static public ShoppingCartServiceProxy Current
        {
            get
            {
                if(instance == null)
                {
                    instance = new ShoppingCartServiceProxy();
                }
                return instance;
            }
        }
        static private ShoppingCartServiceProxy? instance;
        private ShoppingCartServiceProxy() 
        {
            var cartPayload = new WebRequestHandler().Get("/Cart").Result;
            items = JsonConvert.DeserializeObject<List<Item>>(cartPayload) ?? new List<Item>();
        }
        public async Task<IEnumerable<Item>> Search(string? query)
        {
            if (query == null)
            {
                return new List<Item>();
            }
            var response = await new WebRequestHandler().Post("/Cart/Search", new QueryRequest { Query = query });
            items = JsonConvert.DeserializeObject<List<Item>>(response) ?? new List<Item>();
            return CartItems;
        }
        public Item? AddOrUpdate(Item item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if(existingInvItem == null || existingInvItem.Quantity == 0)
            {
                return null;
            }
            if (existingInvItem != null)
            {
                existingInvItem.Quantity--;
                var response = new WebRequestHandler().Get($"/Inventory/{existingInvItem.Id}").Result;
                var invItem = JsonConvert.DeserializeObject<Item>(response);
                invItem.Quantity--;
                response = new WebRequestHandler().Post("/Inventory", invItem).Result;
                invItem = JsonConvert.DeserializeObject<Item>(response);
            }
            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if(existingItem == null)
            {
                //case for add
                var newItem = new Item(item);
                newItem.Quantity = 1;
                CartItems.Add(new Item(newItem));
                var response = new WebRequestHandler().Post("/Cart", newItem).Result;
                var newCartItem = JsonConvert.DeserializeObject<Item>(response);
            }
            else
            {
                //case for update
                existingItem.Quantity++;
                var response = new WebRequestHandler().Post("/Cart", existingItem).Result;
                var updatedCartItem = JsonConvert.DeserializeObject<Item>(response);
            }
            return existingInvItem;
        }
        public Item? ReturnItem(Item item)
        {
            if (item.Id <= 0 || item == null)
            {
                return null;
            }
            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Quantity--;
                var response = new WebRequestHandler().Post("/Cart", itemToReturn).Result;
                var updatedCartItem = JsonConvert.DeserializeObject<Item>(response);

                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if(inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                    var invResponse = new WebRequestHandler().Post("/Inventory", itemToReturn).Result;
                    var newInventoryItem = JsonConvert.DeserializeObject<Item>(invResponse);
                }
                else
                {
                    inventoryItem.Quantity++;
                    var invResponse = new WebRequestHandler().Post("/Inventory", inventoryItem).Result;
                }
            }
            return itemToReturn;
        }
        public void ClearCart()
        {
            items.Clear();
            var response = new WebRequestHandler().Delete("/Cart").Result;
        }
        public double Subtotal
        {
            get
            {
                var result = new WebRequestHandler().Get("/Cart/Subtotal").Result;
                var subtotal = JsonConvert.DeserializeObject<double>(result);
                return subtotal;
            }
        }
        public double Grandtotal
        {
            get
            {
                var result = new WebRequestHandler().Get("/Cart/Grandtotal").Result;
                var grandtotal = JsonConvert.DeserializeObject<double>(result);
                return grandtotal;
            }
        }
    }
}
