
using FitCore.Mobile.Services;
using FitCore.Mobile.Views;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

#if ANDROID
using Android.App;
using Android.Content;
using Android.OS;
#endif

namespace FitCore.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

#if ANDROID
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(
                    "booking_reminder",
                    "Booking Reminders",
                    NotificationImportance.High)
                {
                    Description = "Notificări pentru rezervările tale"
                };

                var manager = (NotificationManager)
                    Android.App.Application.Context
                        .GetSystemService(Context.NotificationService);

                manager.CreateNotificationChannel(channel);
            }
#endif

            builder.Services.AddSingleton(sp =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                return new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://10.0.2.2:5251/")
                };
            });

            builder.Services.AddSingleton<ApiService>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<MyBookingsPage>();
            builder.Services.AddTransient<ProfilePage>();

            return builder.Build();
        }
    }
}