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

        public ICommand? AddCommand { get; set; }
        public ICommand? RemoveCommand { get; set; }
        public ICommand? IEditCommand { get; set; }
        public ICommand? IDeleteCommand { get; set; }

        public string Display
        {
            get
            {
                string dis = Product?.Display ?? string.Empty;
                return $"{dis} {Quantity}";
            }
        }

        public Item()
        {
            Product = new ProductDTO();
            Quantity = 0;
            AddCommand = new Command(DoAdd);
            RemoveCommand = new Command(DoRemove);
            IEditCommand = new Command(DoEdit);
            IDeleteCommand = new Command(DoDelete);
        }

        public override string ToString()
        {
            return $"{Product?.Display ?? string.Empty} Quantity:{Quantity}";
        }

        private void DoAdd()
        {
            ShoppingCartServiceProxy.Current.AddOrUpdate(this);
        }

        private void DoRemove()
        {
            ShoppingCartServiceProxy.Current.ReturnItem(this);
        }

        public void DoEdit()
        {

        }

        public void DoDelete()
        {
            int idToDelete = this.Id;
            ProductServiceProxy.Current.Delete(idToDelete);
        }

        public Item(Item i)
        {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;
            AddCommand = new Command(DoAdd);
            RemoveCommand = new Command(DoRemove);
            IEditCommand = new Command(DoEdit);
            IDeleteCommand = new Command(DoDelete);
        }
    }
}
