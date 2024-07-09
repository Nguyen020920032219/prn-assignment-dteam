using Repository.Entities;
using Service;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CarWashManagementSystem
{
    
    public partial class Report : Window
    {
        ReportService _reportService;
        private DateOnly fromDate;
        private DateOnly toDate;

        public Report()
        {
            InitializeComponent();
            _reportService = new ReportService();
            fromDate = DateOnly.MinValue;
            toDate = DateOnly.MaxValue;
            ShowData();
           
        }

        private void ShowData()
        {
            dgvRevenus.ItemsSource = null;
            var (orders, total) = _reportService.GetOrders(fromDate, toDate);
            dgvRevenus.ItemsSource = orders;
            lblTotalPriceRevenus.Content = total.ToString("0.00");
        }

        private void dpFromRevenus_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
             DateTime? selectedDate = dpFromRevenus.SelectedDate;
            if (selectedDate.HasValue)
            {
                fromDate = DateOnly.FromDateTime(selectedDate.Value.Date); 
                ShowData(); 
            }
        }

        private void dpToRevenus_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dpToRevenus.SelectedDate;
            if (selectedDate.HasValue)
            {
                toDate = DateOnly.FromDateTime(selectedDate.Value);
                ShowData();
            }
        }
    }
}
