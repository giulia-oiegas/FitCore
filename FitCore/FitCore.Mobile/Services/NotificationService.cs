using Plugin.LocalNotification;

namespace FitCore.Mobile.Services;

public class NotificationService
{
    public async Task ScheduleBookingNotification(
    int bookingId,
    string className,
    DateTime classTime)
{
    DateTime notifyTime;

    var oneHourBefore = classTime.AddHours(-1);

    if (oneHourBefore > DateTime.Now)
        notifyTime = oneHourBefore;
    else if (classTime > DateTime.Now)
        notifyTime = DateTime.Now.AddSeconds(5);
    else
        return;

    var request = new NotificationRequest
    {
        NotificationId = bookingId,
        Title = "⏰ Rezervare confirmată",
        Description = $"Clasa „{className}” începe la {classTime:HH:mm}",
        Schedule = new NotificationRequestSchedule
        {
            NotifyTime = notifyTime
        }
    };

    await LocalNotificationCenter.Current.Show(request);
}


    public void CancelBookingNotification(int bookingId)
    {
        LocalNotificationCenter.Current.Cancel(bookingId);
    }
}

