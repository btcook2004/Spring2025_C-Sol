using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//using Java.Security;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Library.eCommerce.Util;
using Library.eCommerce.Utilities;
using Newtonsoft.Json;
using Spring2025_P1.Models;

namespace Maiu.eCommerce.ViewModels
{
    public class ShoppingManagementViewModel : INotifyPropertyChanged
    {
        private ProductServiceProxy _invSvc = ProductServiceProxy.Current;
        private ShoppingCartServiceProxy _cartSvc = ShoppingCartServiceProxy.Current;
        public ItemViewModel? SelectedItem { get; set; }
        public ItemViewModel? SelectedCartItem { get; set; }
        public string Query { get; set; }
        public ObservableCollection<ItemViewModel?> Inventory
        {
            get
            {
                return new ObservableCollection<ItemViewModel?>(_invSvc.Products.Where(i => i?.Quantity > 0).Select( m => new ItemViewModel(m)));
            }
        }
        public ObservableCollection<ItemViewModel?> ShoppingCart
        {
            get
            {
                var filteredList = _cartSvc.CartItems.Where(p => p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false).Select(m => new ItemViewModel(m));
                var filteredList2 = filteredList.Where(p => p?.Model.Quantity > 0);
                return new ObservableCollection<ItemViewModel?>(filteredList2);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void RefreshUX()
        {
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }
        public void PurchaseItem()
        {
            if (SelectedItem != null)
            {
                var shouldRefresh = SelectedItem.Model.Quantity >= 1;
                var updatedItem = _cartSvc.AddOrUpdate(SelectedItem.Model);
                if(updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }
        public void ReturnItem()
        {
            if(SelectedCartItem != null)
            {
                var shouldRefresh = SelectedCartItem.Model.Quantity >= 1;
                var updatedItem = _cartSvc.ReturnItem(SelectedCartItem.Model);
                if(updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }
        public async Task<bool> Search()
        {
            await _cartSvc.Search(Query);
            NotifyPropertyChanged(nameof(ShoppingCart));
            return true;
        }
    }
}
