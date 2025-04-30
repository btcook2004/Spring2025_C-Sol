using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring2025_P1.Models;

namespace Library.eCommerce.DTO
{
    public class ProductDTO
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
        public ProductDTO()
        {
            Name = string.Empty;
            Price = 0.0;
        }
        public override string ToString()
        {
            return Display ?? string.Empty;
        }
        public ProductDTO(ProductDTO p)
        {
            Id = p.Id;
            Name = p.Name;
            Price = p.Price;
        }
        public ProductDTO (Product p)
        {
            Name = p.Name;
            Id = p.Id;
        }
    }
}
