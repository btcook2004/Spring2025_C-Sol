using Maiu.eCommerce.ViewModels;

namespace Maiu.eCommerce
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }



        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }

        private void ShopClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShoppingManagement");
        }
    }

}
