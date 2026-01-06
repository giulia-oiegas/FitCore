using FitCore.Data.Models;
using FitCore.Mobile.Services;

namespace FitCore.Mobile.Views;

public partial class MyBookingsPage : ContentPage
{
    ApiService _api;

    public MyBookingsPage(ApiService api)
    {
        InitializeComponent();
        _api = api;
    }

    protected override async void OnAppearing()
    {
        if (App.CurrentUser == null)
            return;

        BookingsView.ItemsSource =
            await _api.GetMyBookings(App.CurrentUser.Id);
    }

    private async void OnRemoveBookingClicked(object sender, EventArgs e)
    {
        var booking = (Booking)((Button)sender).CommandParameter;

        bool confirm = await DisplayAlert(
            "Confirmare",
            "Sigur vrei să ștergi această rezervare?",
            "Da",
            "Nu");

        if (!confirm)
            return;

        await _api.RemoveBooking(booking.Id);

        var notificationService = new NotificationService();
        notificationService.CancelBookingNotification(booking.Id);

        // refresh list
        BookingsView.ItemsSource =
            await _api.GetMyBookings(App.CurrentUser.Id);
    }

}
