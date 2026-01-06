namespace FitCore.Mobile;

public partial class App : Application
{
    public static FitCore.Data.Models.Member? CurrentUser { get; set; }

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}
