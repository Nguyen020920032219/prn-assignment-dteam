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
        CostOfGoodService _costOfGoodService;
        private DateOnly fromDate;
        private DateOnly toDate;

        public Report()
        {
            InitializeComponent();
            _reportService = new ReportService();
            _costOfGoodService = new CostOfGoodService();
            fromDate = DateOnly.MinValue;
            toDate = DateOnly.MaxValue;
            ShowData();
            ShowDataCostOfGood();
           
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
        private void ShowDataCostOfGood()
        {
            dgvCostOfGoodSold.ItemsSource = null;
            var (cost, total) = _costOfGoodService.GetOrdersCostOfGood(fromDate, toDate);
            dgvCostOfGoodSold.ItemsSource = cost;
            lblTotalPriceCostOfGoodSold.Content = total.ToString("0.00");
        }
        
        

        private void dpFromCostOfGoodSold_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dpFromCostOfGoodSold.SelectedDate;
            if (selectedDate.HasValue)
            {
                fromDate = DateOnly.FromDateTime(selectedDate.Value.Date);
                ShowDataCostOfGood();
            }
        }

        private void dpToCostOfGoodSold_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dpToCostOfGoodSold.SelectedDate;
            if (selectedDate.HasValue)
            {
                toDate = DateOnly.FromDateTime(selectedDate.Value);
                ShowDataCostOfGood();
            }
        }

        private void ShowDataGrossFit()
        {
            var (orders, total) = _reportService.GetOrders(fromDate, toDate);
            txtRevenus.Text = total.ToString("0.00");
            var (cost, totals) = _costOfGoodService.GetOrdersCostOfGood(fromDate, toDate);
            txtCostOfGoodSold.Text = totals.ToString("0.00");
            decimal grossprofit = decimal.Parse(txtRevenus.Text) - decimal.Parse(txtCostOfGoodSold.Text);
            txtGrossProfit.Text = grossprofit.ToString("0.00");

        }

        private void dpFromGrossProfit_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dpFromGrossProfit.SelectedDate;
            if (selectedDate.HasValue)
            {
                fromDate = DateOnly.FromDateTime(selectedDate.Value.Date);
                ShowDataGrossFit();
            }
        }

        private void dpToGrossProfit_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dpToGrossProfit.SelectedDate;
            if (selectedDate.HasValue)
            {
                toDate = DateOnly.FromDateTime(selectedDate.Value);
                ShowDataGrossFit() ;    
            }
        }
    }
}
