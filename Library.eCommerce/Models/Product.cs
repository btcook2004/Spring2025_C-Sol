using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.DTO;

namespace Spring2025_P1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public string? Display
        {
            get
            {
                return $"{Id}. {Name}, ${Price}";
            }
        }
        public Product()
        {
            Name = string.Empty;
            Price = 0.0;
        }
        public override string ToString()
        {
            return Display ?? string.Empty;
        }
        public Product (Product p)
        {
            Id = p.Id;
            Name = p.Name;
            Price = p.Price;
        }
        public Product(ProductDTO p)
        {
            Name = p.Name;
            Id = p.Id;
        }
    }
}
