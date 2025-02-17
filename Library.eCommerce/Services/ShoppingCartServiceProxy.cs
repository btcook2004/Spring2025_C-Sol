using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Spring2025_P1.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartServiceProxy
    {
        private ShoppingCartServiceProxy()
        {
            Products = new List<Product?>();
        }

        //private int LastKey
        //{
        //    get
        //    {
        //        if (!Products.Any())
        //        {
        //            return 0;
        //        }
        //        return Products.Select(p => p?.Id ?? 0).Max();
        //    }
        //}

        private static ShoppingCartServiceProxy? instance;
        private static object instanceLock = new object();

        public static ShoppingCartServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartServiceProxy();
                    }
                }


                return instance;
            }
        }

        public List<Product?> Products { get; private set; }

        public Product AddOrUpdate(Product product)
        {
            return product;
        }
        public Product AddToCart(Product product)
        {
            var selection = product.Id;
            var prodInCart = ShoppingCartServiceProxy.Current.Products.FirstOrDefault(p => p.Id == selection);
            //prodInCart is the item currently in the shopping cart
            //product is the item in the inventory

            //need to decrement quantity of item in inventory
            //if quantity goes to 0, need to delete from inventory list
            product.Quantity--;
            if (product.Quantity == 0)
            {
                ProductServiceProxy.Current.Products.Remove(product);
            }

            //if item is not already in the cart, we need to add it
            //if it is already in the cart, need to increment its quantity
            if (prodInCart != null)
            {
                // the item is already in the cart, so need to update quantity
                prodInCart.Quantity++;
            }
            else
            {
                Product newProduct = new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity = 1,
                    Price = product.Price
                };
                ShoppingCartServiceProxy.Current.Products.Add(newProduct);
            }
            prodInCart = ShoppingCartServiceProxy.Current.Products.FirstOrDefault(p => p.Id == selection);
            return prodInCart;
        }

        public Product Delete(int id)
        {
            var prodInCart = ShoppingCartServiceProxy.Current.Products.FirstOrDefault(p => p.Id == id);
            var product = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == id);
            //prodInCart is the item currently in the shopping cart
            //product is the item in the inventory

            //if product in cart also exists in inventory
            if (product != null)
            {
                product.Quantity += prodInCart.Quantity;
            }
            else
            {
                ProductServiceProxy.Current.AddOrUpdate(prodInCart);
            }
            Products.Remove(prodInCart);
            return prodInCart;
        }

        public bool DecrementCart(Product product)
        {
            product.Quantity -= 1;
            if (product.Quantity == 0)
                Products.Remove(product);
            var prodInInventory = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == product.Id);
            if (prodInInventory != null)
                prodInInventory.Quantity++;
            else
                ProductServiceProxy.Current.Products.Add(new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
                
            return true;
        }

        public double? SubTotal()
        {
            //iterate through shopping list and multiple quantity by price for each item
            double? total = 0;
            for (int i = 0; i < Products.Count; i++)
            {
                var product = Products[i];
                total += product?.Quantity * product?.Price;
            }

            //make sure total only has 2 decimal places
            total = Math.Round(total ?? 0, 2);
            return total;
        }

        public double? GTotal()
        {
            return Math.Round(SubTotal() * 1.07 ?? 0, 2);
        }

        public void ClearCart()
        {
            Products.Clear();
        }

    }
}
