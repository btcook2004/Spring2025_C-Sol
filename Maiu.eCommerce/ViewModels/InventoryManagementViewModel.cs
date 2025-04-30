using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Spring2025_P1.Models;

namespace Maiu.eCommerce.ViewModels
{
    internal class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public ItemViewModel? SelectedProduct { get; set; }
        public string Query { get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current;
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Products));
        }
        public async Task<bool> Search()
        {
            await _svc.Search(Query);
            NotifyPropertyChanged(nameof(Products));
            return true;
        }
        public ObservableCollection<ItemViewModel?> Products
        {
            get
            {
                var filteredList = _svc.Products
                    .Where(p => p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false)
                    .Select(m => new ItemViewModel(m))
                    .Where(p => p?.Model.Quantity > 0);
                return new ObservableCollection<ItemViewModel?>(filteredList);
            }
        }
        public Item? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Model.Id ?? 0);
            NotifyPropertyChanged("Products");
            return item;
        }
        public void InlineSet()
        {

        }
    }
}
