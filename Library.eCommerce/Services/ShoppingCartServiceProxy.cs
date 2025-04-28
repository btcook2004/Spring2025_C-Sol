using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Library.eCommerce.Models;
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
            items = new List<Item>();
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
            }
            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if(existingItem == null)
            {
                //case for add
                var newItem = new Item(item);
                newItem.Quantity = 1;
                CartItems.Add(new Item(newItem));
            }
            else
            {
                //case for update
                existingItem.Quantity++;
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
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if(inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                }
                else
                {
                    inventoryItem.Quantity++;
                }
            }
            return itemToReturn;
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}
