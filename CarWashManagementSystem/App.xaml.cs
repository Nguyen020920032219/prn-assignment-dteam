using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Threading;



namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterExceptionHandler();
            
        }
        private void RegisterExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                ExceptionHandler(e.ExceptionObject as Exception);
            };

            DispatcherUnhandledException += (sender, e) =>
            {
                e.Handled = true;
                ExceptionHandler(e.Exception);

            };
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                ExceptionHandler(e.Exception);
            };
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionHandler(e.ExceptionObject as Exception);   
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            
        }

        private void ExceptionHandler(Exception exception)
        {
            if (exception is FileNotFoundException)
            {
                MessageBox.Show("File not found: " + exception.Message);
            }
            else if (exception is IOException)
            {
                MessageBox.Show("IO errors: " + exception.Message);
            }
            else if (exception is NotSupportedException)
            {
                MessageBox.Show("Errors : " + exception.Message);
            }
            else if (exception is ArgumentNullException)
            {
                MessageBox.Show("ArgumentNullException: " + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        }
    }

}
