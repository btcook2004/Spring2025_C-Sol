using System.ComponentModel;
using Library.eCommerce.Services;
using Maiu.eCommerce.ViewModels;

namespace Maiu.eCommerce.Views;
public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
		BindingContext = new InventoryManagementViewModel();
	}
    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }
	private void EditClicked(object sender, EventArgs e)
	{
		var productId = (BindingContext as InventoryManagementViewModel)?.SelectedProduct?.Model.Id;
		Shell.Current.GoToAsync($"//Product?productId={productId}");
	}
	private void DeleteClicked(object sender, EventArgs e)
	{
		(BindingContext as InventoryManagementViewModel)?.Delete();
	}
    private void AddClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Product");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		(BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }
    private void SearchClicked(object sender, EventArgs e)
    {
		(BindingContext as InventoryManagementViewModel)?.Search();
    }
    private void InlineEditClicked(object sender, EventArgs e)
    {
		//(BindingContext as InventoryManagementViewModel)?.InlineSet();
		Shell.Current.GoToAsync("//Product");
    }
    private void InlineDeleteClicked(object sender, EventArgs e)
    {
		(BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }
}