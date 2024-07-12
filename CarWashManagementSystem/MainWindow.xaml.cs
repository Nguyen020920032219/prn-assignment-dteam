using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl? _activeWindow = null;

        public MainWindow(string role)
        {
            InitializeComponent();
           
            if (role.Equals("Staff", StringComparison.OrdinalIgnoreCase)) 
            {
                btnEmployer.IsEnabled = false;
                btnServices.IsEnabled = false;
            }
        }

        public void OpenChildWindow (UserControl childWindow)
        {
            if (_activeWindow != null)
            {
                panelChild.Children.Remove(_activeWindow);
            }

            _activeWindow = childWindow;
            panelChild.Children.Add(childWindow);
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            panelSlide.Height = btnDashboard.ActualHeight;
            Thickness margin = panelSlide.Margin;
            margin.Top = btnDashboard.TranslatePoint(new Point(0, 0), panelSlide.Parent as UIElement).Y;
            panelSlide.Margin = margin;
        }

        private void btnEmployer_Click(object sender, RoutedEventArgs e)
        {
            panelSlide.Height = btnEmployer.ActualHeight;
            Thickness margin = panelSlide.Margin;
            margin.Top = btnEmployer.TranslatePoint(new Point(0, 0), panelSlide.Parent as UIElement).Y;
            panelSlide.Margin = margin;

        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            panelSlide.Height = btnCustomer.ActualHeight;
            Thickness margin = panelSlide.Margin;
            margin.Top = btnCustomer.TranslatePoint(new Point(0, 0), panelSlide.Parent as UIElement).Y;
            panelSlide.Margin = margin;
        }

        private void btnServices_Click(object sender, RoutedEventArgs e)
        {
            panelSlide.Height = btnServices.ActualHeight;
            Thickness margin = panelSlide.Margin;
            margin.Top = btnServices.TranslatePoint(new Point(0, 0), panelSlide.Parent as UIElement).Y;
            panelSlide.Margin = margin;

            OpenChildWindow(new ServiceWindow());
        }

        private void btnCash_Click(object sender, RoutedEventArgs e)
        {
            panelSlide.Height = btnCash.ActualHeight;
            Thickness margin = panelSlide.Margin;
            margin.Top = btnCash.TranslatePoint(new Point(0, 0), panelSlide.Parent as UIElement).Y;
            panelSlide.Margin = margin;

            OpenChildWindow(new Cash());
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            panelSlide.Height = btnReport.ActualHeight;
            Thickness margin = panelSlide.Margin;
            margin.Top = btnReport.TranslatePoint(new Point(0, 0), panelSlide.Parent as UIElement).Y;
            panelSlide.Margin = margin;
        }

        private void btnWareHouse_Click(object sender, RoutedEventArgs e)
        {
            panelSlide.Height = btnWareHouse.ActualHeight;
            Thickness margin = panelSlide.Margin;
            margin.Top = btnWareHouse.TranslatePoint(new Point(0, 0), panelSlide.Parent as UIElement).Y;
            panelSlide.Margin = margin;

            OpenChildWindow(new Warehouse());
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            panelSlide.Height = btnLogout.ActualHeight;
            Thickness margin = panelSlide.Margin;
            margin.Top = btnLogout.TranslatePoint(new Point(0, 0), panelSlide.Parent as UIElement).Y;
            panelSlide.Margin = margin;

            Login login = new Login();
            login.Show();
            this.Close();
        }

        
    }
}