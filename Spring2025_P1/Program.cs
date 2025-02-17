//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Principal;
using Library.eCommerce.Services;
using Spring2025_P1.Models;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? role = "Z";
            do
            {
                Console.WriteLine("Welcome to Amazing!");
                Console.WriteLine("Are you an exmployee or a customer? E for Employee, C for Customer, Q to Quit");
                role = Console.ReadLine() ?? "Z";
                if (role[0] == 'E' || role[0] == 'e')
                {
                    Console.WriteLine("Mode: Employee");
                    Employee();
                }
                else if (role[0] == 'C' || role[0] == 'c')
                {
                    Console.WriteLine("Mode: Customer");
                    Customer();
                }
                else //invalid choice
                    ;
            } while (role[0] != 'Q' && role[0] != 'q');
        }

        static void Employee()
        {
            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("R. Read all inventory items");
            Console.WriteLine("U. Update an inventory item");
            Console.WriteLine("D. Delete an inventory item");
            Console.WriteLine("Q. Quit");

            List<Product?> list = ProductServiceProxy.Current.Products;

            char choice;
            do
            {
                string? input = Console.ReadLine();
                choice = input[0];
                switch (choice)
                {
                    case 'C':
                    case 'c':
                        Console.Write("Enter a product name: ");
                        string? name = Console.ReadLine() ?? string.Empty;
                        Console.Write("Enter a quantity: ");
                        int? quantity = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Enter a price: ");
                        double? price = double.Parse(Console.ReadLine() ?? "0");
                        ProductServiceProxy.Current.AddOrUpdate(new Product
                        {
                            Name = name,
                            Quantity = quantity,
                            Price = price
                        });
                        break;
                    case 'R':
                    case 'r':
                        list.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                    case 'u':
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = list.FirstOrDefault(p => p.Id == selection);
                        if (selectedProd != null)
                        {
                            Console.Write("Enter a new product name: ");
                            name = Console.ReadLine() ?? string.Empty;
                            Console.Write("Enter a new quantity: ");
                            quantity = int.Parse(Console.ReadLine() ?? "0");
                            Console.Write("Enter a new price: ");
                            price = double.Parse(Console.ReadLine() ?? "0");
                            selectedProd.Name = name;
                            selectedProd.Quantity = quantity;
                            selectedProd.Price = price;
                            ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                        }
                        break;
                    case 'D':
                    case 'd':
                        Console.WriteLine("Which product would you like to update?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection);
                        break;
                    case 'Q':
                    case 'q':
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');
            return;
        }
        static void Customer()
        {
            Console.WriteLine("S. Show all inventory items to choose from");
            Console.WriteLine("C. Add an item from inventory to shopping cart");
            Console.WriteLine("R. Read all items in shopping cart");
            Console.WriteLine("U. Edit how many of a product are in shopping cart");
            Console.WriteLine("D. Delete an item from your cart");
            Console.WriteLine("O. CheckOut");
            Console.WriteLine("Q. Quit");

            List<Product?> inventory = ProductServiceProxy.Current.Products;
            List<Product?> shoppingCart = ShoppingCartServiceProxy.Current.Products;

            char choice;
            do
            {
                string? input = Console.ReadLine() ?? "Z";
                choice = input[0];
                switch (choice)
                {
                    case 'S':
                    case 's':
                        Console.WriteLine("Inventory: ");
                        inventory.ForEach(Console.WriteLine);
                        Console.WriteLine("End of Inventory");
                        break;
                    case 'C':
                    case 'c':
                        Console.WriteLine("Which inventory item would you like to add to cart?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == selection);
                        ShoppingCartServiceProxy.Current.AddToCart( selectedProd );
                        break;
                    case 'R':
                    case 'r':
                        shoppingCart.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                    case 'u':
                        Console.WriteLine("Which product would you like change quantity?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        selectedProd = ShoppingCartServiceProxy.Current.Products.FirstOrDefault(p => p.Id == selection);
                        Console.WriteLine("Would you like to Increase or Decrease the quantity?");
                        string incDec = Console.ReadLine() ?? "Z";
                        char incDecC = incDec[0];
                        if (incDecC == 'I')
                            ShoppingCartServiceProxy.Current.AddToCart(selectedProd);
                        else if (incDecC == 'D')
                            ShoppingCartServiceProxy.Current.DecrementCart(selectedProd);
                        else
                            Console.WriteLine("Invalid Choice");
                        break;
                    case 'D':
                    case 'd':
                        Console.WriteLine("Which product would you like to remove from your cart?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ShoppingCartServiceProxy.Current.Delete(selection);
                        break;
                    case 'O':
                    case 'o':
                        Console.WriteLine("Receipt: ");
                        //Console.WriteLine(shoppingCart);
                        for (int i = 0; i < shoppingCart.Count(); i++)
                        {
                            //print out each item with price and quantity
                            Console.WriteLine(shoppingCart[i]);
                        }
                        Console.Write("Sub-Total: $");
                        Console.WriteLine(ShoppingCartServiceProxy.Current.SubTotal());
                        Console.Write("Grand Total: $");
                        Console.WriteLine(ShoppingCartServiceProxy.Current.GTotal());
                        ShoppingCartServiceProxy.Current.ClearCart();
                        choice = 'Q';
                        break;
                    case 'Q':
                    case 'q':
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');
            return;



            return;
        }
    }
}