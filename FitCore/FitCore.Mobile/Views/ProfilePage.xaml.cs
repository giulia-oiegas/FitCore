using System.Xml;
using FitCore.Data.Models;
using FitCore.Mobile.Services;

namespace FitCore.Mobile.Views;

public partial class ProfilePage : ContentPage
{
    ApiService _api;

    public ProfilePage(ApiService api)
    {
        InitializeComponent();
        _api = api;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (App.CurrentUser == null)
        {
            var userId = Preferences.Get("UserId", 0);

            if (userId == 0)
            {
                await Shell.Current.GoToAsync("//LoginPage");
                return;
            }

            App.CurrentUser = await _api.GetMember(userId);
        }

        var user = App.CurrentUser;

        BindingContext = new
        {
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,

            MembershipType = user.MembershipType,
            MembershipEndDate = user.MembershipEndDate,

            HasMembership = user.MembershipType != null,
            ShowMembershipChooser = user.MembershipType == null
        };

        if (user.MembershipType == null)
        {
            MembershipsView.ItemsSource =
                await _api.GetMembershipTypes();
        }
    }


    async void OnSelectMembership(object sender, EventArgs e)
    {
        var membership = (MembershipType)
            ((Button)sender).CommandParameter;

        await _api.SelectMembership(
            App.CurrentUser.Id,
            membership.Id);

        App.CurrentUser = await _api.SelectMembership(
            App.CurrentUser.Id,
            membership.Id);

        OnAppearing();

        await DisplayAlert("Succes",
            $"Ai activat abonamentul {membership.Name}",
            "OK");
    }

    async void OnLogout(object sender, EventArgs e)
    {
        App.CurrentUser = null;
        Preferences.Remove("UserId");
        await Shell.Current.GoToAsync("///LoginPage");
    }
}
