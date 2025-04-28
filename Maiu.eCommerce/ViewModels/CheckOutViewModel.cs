using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maiu.eCommerce.ViewModels
{
    public class CheckOutViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Item?> Items
        {
            get
            {
                return new ObservableCollection<Item?>(ShoppingCartServiceProxy.Current.CartItems.Where(p => p?.Quantity > 0));
            }
        }
        public double Subtotal
        {
            get
            {
                return Items.Sum(i => i?.Product.Price * i?.Quantity) ?? 0;
            }
        }
        public double Grandtotal
        {
            get
            {
                return Subtotal * 1.07;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void RefreshTotal()
        {
            NotifyPropertyChanged(nameof(Items));
            NotifyPropertyChanged(nameof(Subtotal));
            NotifyPropertyChanged(nameof(Grandtotal));
        }
        public void DoCheckout()
        {
            ShoppingCartServiceProxy.Current.ClearCart();
        }
    }
}
