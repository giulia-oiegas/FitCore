using FitCore.Mobile.Services;
using Microsoft.Maui.Storage;

namespace FitCore.Mobile.Views;

public partial class LoginPage : ContentPage
{
    ApiService _api;

    public LoginPage(ApiService api)
    {
        InitializeComponent();
        _api = api;
    }

    async void OnLogin(object sender, EventArgs e)
    {
        try
        {
            var user = await _api.Login(
                EmailEntry.Text,
                PasswordEntry.Text);

            App.CurrentUser = await _api.GetMember(user.Id);

            Preferences.Set("UserId", user.Id);

            await Shell.Current.GoToAsync("///HomePage");
        }
        catch
        {
            await DisplayAlert("Eroare", "Email sau parolă greșită", "OK");
        }
    }


    async void OnGoToRegister(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///RegisterPage");
    }
}
