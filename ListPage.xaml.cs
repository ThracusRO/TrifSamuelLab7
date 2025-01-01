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
}