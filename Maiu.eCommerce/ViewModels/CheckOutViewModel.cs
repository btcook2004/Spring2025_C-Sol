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
                var cartItems = ShoppingCartServiceProxy.Current.CartItems.Where(p => p?.Quantity > 0);
                return new ObservableCollection<Item?>(cartItems);
                //return ShoppingCartServiceProxy.Current.CartItems;
            }
        }

        public double Subtotal
        {
            get
            {
                double subtotal = Items.Sum(i => i?.Product.Price * i?.Quantity) ?? 0;
                return subtotal;
            }
        }

        public double Grandtotal
        {
            get
            {
                double subtotal = Items.Sum(i => i?.Product.Price * i?.Quantity) ?? 0;
                double grandTotal = subtotal * 1.07;
                return grandTotal;
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
            // clear the cart
            ShoppingCartServiceProxy.Current.ClearCart();
        }
    }
}
