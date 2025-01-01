using TrifSamuelLab7.Models;

namespace TrifSamuelLab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
   //Lab 9 Pas12
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });
    }

    /* Test */
    protected override async void OnAppearing()
    {
        base.OnAppearing();

            // Obtine lista de cumparaturi curente
            var shopl = (ShopList)BindingContext;

            // Incarca produsele asociate acestei liste
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
                   
    }
    //---- Sarcina Laborator 9 Begin------
    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem is Product selectedProduct)
        {
            // Confirmare stergere
            bool confirm = await DisplayAlert("Confirm",
                                              $"Are you sure you want to delete {selectedProduct.Description}?",
                                              "Yes", "No");
            if (confirm)
            {
                // sterge legatura dintre produs si lista curenta
                var shopList = (ShopList)BindingContext;
                var listProduct = await App.Database.GetListProductsAsync(shopList.ID);
                var productToDelete = listProduct.FirstOrDefault(p => p.ID == selectedProduct.ID);

                if (productToDelete != null)
                {
                    await App.Database.DeleteProductAsync(selectedProduct);

                    // actualizeaza lista de produse
                    listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
                }
            }
        }
        else
        {
            await DisplayAlert("Error", "Please select an item to delete.", "OK");
        }
    }
     //---- Sarcina Laborator 9 Endn------
}