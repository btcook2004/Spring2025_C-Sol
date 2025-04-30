using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using Api.eCommerce.Database;
using Library.eCommerce.Models;
using Library.eCommerce.Utilities;
using Newtonsoft.Json;

namespace Api.eCommerce.EC
{
    public class CartEC
    {
        public List<Item?> Get()
        {
            return FakeCartDatabase.Cart;
        }
        public IEnumerable<Item> Get(string? query)
        {
            return FakeCartDatabase.Search(query).Take(100) ?? new List<Item>();
        }
        public Item AddOrUpdate(Item item)
        {
            var existingItem = FakeCartDatabase.Cart.FirstOrDefault(p => p.Id == item.Id);
            if(existingItem == null)
            {
                //case for add
                FakeCartDatabase.Cart.Add(item);
                existingItem = FakeCartDatabase.Cart.FirstOrDefault(p => p.Id == item.Id);
            }
            else
            {
                //case for update
                var index = FakeCartDatabase.Cart.IndexOf(existingItem);
                FakeCartDatabase.Cart.RemoveAt(index);
                FakeCartDatabase.Cart.Insert(index, new Item(item));
            }
            return existingItem;
        }
        public IEnumerable<Item> ClearCart()
        {
            FakeCartDatabase.Cart.Clear();
            return FakeCartDatabase.Cart;
        }
        public bool Delete()
        {
            FakeCartDatabase.Cart.Clear();
            return true;
        }
        public double Subtotal()
        {
            return FakeCartDatabase.Cart.Sum(i => i?.Product.Price * i?.Quantity) ?? 0;
        }
        public double Grandtotal()
        {
            return Subtotal() * 1.07;
        }
    }
}
