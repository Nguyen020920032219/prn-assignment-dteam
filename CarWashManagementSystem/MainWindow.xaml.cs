using Service;
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
        ReportService _reportService;
        CostOfGoodService _costOfGoodService;
        private DateOnly fromDate;
        private DateOnly toDate;
        public MainWindow()
        private UserControl? _activeWindow = null;

        public MainWindow(string role)
        {
            InitializeComponent();
            _reportService = new ReportService();
            _costOfGoodService = new CostOfGoodService();
            toDate = DateOnly.FromDateTime(DateTime.Now);
            fromDate = toDate.AddDays(-7);
            ShowData();
           
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

            OpenChildWindow(new Employer());

        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            panelSlide.Height = btnCustomer.ActualHeight;
            Thickness margin = panelSlide.Margin;
            margin.Top = btnCustomer.TranslatePoint(new Point(0, 0), panelSlide.Parent as UIElement).Y;
            panelSlide.Margin = margin;

            OpenChildWindow(new Customer());
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
        private void ShowData()
        {
            var (orders, total) = _reportService.GetOrders(fromDate, toDate);
            txtRevenus.Text = total.ToString("0.00");
            var (cost, totals) = _costOfGoodService.GetOrdersCostOfGood(fromDate, toDate);
            txtCostOfGoodSold.Text = totals.ToString("0.00");
            decimal grossprofit = decimal.Parse(txtRevenus.Text) - decimal.Parse(txtCostOfGoodSold.Text);
            txtGrossProfit.Text = grossprofit.ToString("0.00");           
        }
        

        private void btnHistoryOrder_Click(object sender, RoutedEventArgs e)
        {
            panelSlide.Height = btnHistoryOrder.ActualHeight;
            Thickness margin = panelSlide.Margin;
            margin.Top = btnHistoryOrder.TranslatePoint(new Point(0, 0), panelSlide.Parent as UIElement).Y;
            panelSlide.Margin = margin;

            OpenChildWindow(new OrderHistory());
        }
    }
}