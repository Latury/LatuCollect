using Microsoft.UI.Xaml;

namespace LatuCollect.UI.WinUI
{
    public partial class App : Application
    {
        private Window? _window;

        public App()
        {
            this.InitializeComponent();

            this.UnhandledException += App_UnhandledException;
        }

        private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            System.IO.File.WriteAllText("crash.txt", e.Exception.ToString());
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Activate();
        }
    }
}