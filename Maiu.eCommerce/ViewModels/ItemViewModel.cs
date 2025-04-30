using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maiu.eCommerce.ViewModels
{
    public class ItemViewModel
    {
        public Item Model { get; set; }
        public ItemViewModel()
        {
            Model = new Item();
            SetupCommands();
        }
        public ItemViewModel(Item model)
        {
            Model = model;
            SetupCommands();
        }
        void SetupCommands()
        {
            AddCommand = new Command(DoAdd);
            RemoveCommand = new Command(DoRemove);
            IEditCommand = new Command(DoEdit);
            IDeleteCommand = new Command(DoDelete);
        }
        private void DoAdd()
        {
            ShoppingCartServiceProxy.Current.AddOrUpdate(Model);
        }
        private void DoRemove()
        {
            ShoppingCartServiceProxy.Current.ReturnItem(Model);
        }
        public void DoEdit()
        {

        }
        public void DoDelete()
        {
            int idToDelete = Model.Id;
            ProductServiceProxy.Current.Delete(idToDelete);
        }
        public ICommand? AddCommand { get; set; }
        public ICommand? RemoveCommand { get; set; }
        public ICommand? IEditCommand { get; set; }
        public ICommand? IDeleteCommand { get; set; }
    }
}
