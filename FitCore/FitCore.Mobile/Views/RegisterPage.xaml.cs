using FitCore.Mobile.Services;
using FitCore.Data.Models;

namespace FitCore.Mobile.Views;

public partial class RegisterPage : ContentPage
{
    ApiService _api;

    public RegisterPage(ApiService api)
    {
        InitializeComponent();
        _api = api;
    }

    async void OnRegister(object sender, EventArgs e)
    {
        try
        {
            var data = new
            {
                FirstName = FirstNameEntry.Text,
                LastName = LastNameEntry.Text,
                Email = EmailEntry.Text,
                PhoneNumber = PhoneEntry.Text,
                Password = PasswordEntry.Text
            };

            var user = await _api.Register(data);
            App.CurrentUser = user;

            await DisplayAlert("Succes", "Cont creat cu succes!", "OK");
            await Shell.Current.GoToAsync("///HomePage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare la înregistrare", ex.Message, "OK");
        }
    }



    async void OnGoToLogin(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
