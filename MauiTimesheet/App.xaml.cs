using MauiTimesheet.Services;

namespace MauiTimesheet
{
    public partial class App : Application
    {
        public App(AuthService authService)
        {
            InitializeComponent();
            authService.Initalize();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "MauiTimesheet" };
        }
    }
}
