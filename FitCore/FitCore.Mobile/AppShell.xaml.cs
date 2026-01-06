using FitCore.Data.Models;
using Microsoft.Maui.Storage;
using Plugin.LocalNotification;

namespace FitCore.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Dispatcher.Dispatch(async () =>
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();

            if (Preferences.ContainsKey("UserId"))
            {
                await GoToAsync("//MainTabs/HomePage");
            }
            else
            {
                await GoToAsync("//LoginPage");
            }
        });
    }
}
