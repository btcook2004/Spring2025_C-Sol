using System.Security.Cryptography.X509Certificates;
using Maiu.eCommerce.ViewModels;

namespace Maiu.eCommerce.Views;

public partial class CheckOutView : ContentPage
{
	public CheckOutView()
	{
		InitializeComponent();
		BindingContext = new CheckOutViewModel();
	}

    private void ExitSavingClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//ShoppingManagement");
    }

    private void CompleteClicked(object sender, EventArgs e)
    {
        (BindingContext as CheckOutViewModel).DoCheckout();
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        //(BindingContext as CheckOutViewModel).DoCheckout();
        (BindingContext as CheckOutViewModel).RefreshTotal();
    }
}