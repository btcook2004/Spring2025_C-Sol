//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using Library.eCommerce.Services;
using Spring2025_P1.Models;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lastKey = 1;

            Console.WriteLine("Welcome to Amazon!");

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
                        list.Add(new Product
                        {
                            Id = lastKey++,
                            Name = Console.ReadLine()
                        });
                        break;
                    case 'R':
                    case 'r':
                        //print out all products in our list
                        list.ForEach(Console.WriteLine); //same
                        //foreach(var prod in list)  //can't do this if changing the size of list
                        //{
                        //    Console.WriteLine(prod);
                        //}
                        break;
                    case 'U':
                    case 'u':
                        //select one of the products
                        //replace the product with the new one
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = list.FirstOrDefault(p => p.Id == selection);
                        if(selectedProd != null)
                        {
                            selectedProd.Name = Console.ReadLine() ?? "ERROR";
                        }
                        break;
                    case 'D':
                    case 'd':
                        //select one of the products
                        //throw it away
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        selectedProd = list.FirstOrDefault(p => p.Id == selection);
                        list.Remove(selectedProd);
                        break;
                    case 'Q':
                    case 'q':
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');
            Console.ReadLine();


        }
        static void AddProduct(List<string?> list)
        {
            var newProduct = Console.ReadLine() ?? "UNK";
            list.Add(newProduct);
        }
    }
}