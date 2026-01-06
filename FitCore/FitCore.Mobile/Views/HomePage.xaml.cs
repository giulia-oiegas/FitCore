using FitCore.Data.Models;
using FitCore.Mobile.Services;
using Plugin.LocalNotification;
using System.Globalization;
using Microsoft.Maui.Storage;

namespace FitCore.Mobile.Views;

public partial class HomePage : ContentPage
{
    ApiService _api;

    public HomePage(ApiService api)
    {
        InitializeComponent();
        _api = api;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 🔹 Asigurăm userul
        if (App.CurrentUser == null || string.IsNullOrEmpty(App.CurrentUser.FirstName))
        {
            var userId = Preferences.Get("UserId", 0);

            if (userId != 0)
            {
                App.CurrentUser = await _api.GetMember(userId);
            }
        }

        var user = App.CurrentUser;

        BindingContext = new
        {
            Greeting = user != null
                ? $"Salut, {user.FirstName}!"
                : "Salut!",

            TodayText = DateTime.Now.ToString(
                "dddd, dd MMMM",
                new CultureInfo("ro-RO"))
        };

        ClassesView.ItemsSource =
            await _api.GetFutureClasses();

        await LocalNotificationCenter
            .Current
            .RequestNotificationPermission();
    }

    async void OnBookClicked(object sender, EventArgs e)
    {
        if (App.CurrentUser == null)
            return;

        var gymClass =
            (GymClass)((Button)sender).CommandParameter;

        await _api.BookClass(
            gymClass.Id,
            App.CurrentUser.Id);

        var notificationService =
            new NotificationService();

        notificationService.ScheduleBookingNotification(
            bookingId: gymClass.Id,
            className: gymClass.Name,
            classTime: gymClass.Schedule);

        await DisplayAlert(
            "Succes",
            "Rezervare făcută!",
            "OK");
    }
}
