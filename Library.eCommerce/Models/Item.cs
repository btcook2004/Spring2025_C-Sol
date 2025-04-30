using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.eCommerce.DTO;
using Library.eCommerce.Services;
using Spring2025_P1.Models;

namespace Library.eCommerce.Models
{
    public class Item
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public int? Quantity { get; set; }
        public string Display
        {
            get
            {
                return $"{Product?.Display ?? string.Empty} {Quantity}";
            }
        }
        public Item()
        {
            Product = new ProductDTO();
            Quantity = 0;
        }
        public override string ToString()
        {
            return $"{Product?.Display ?? string.Empty} Quantity:{Quantity}";
        }
        public Item(Item i)
        {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;
        }
    }
}
