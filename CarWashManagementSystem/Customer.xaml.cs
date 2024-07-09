using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {

        public Customer()
        {
            InitializeComponent();
            _productService = new ProductService();
            _validation = new ValidationService();
            ShowData();
        }

        private void dgvCustomer_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            CustomerModule wm = new CustomerModule();
            wm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            wm.Closed += CustomerModule_Closed;
            wm.ShowDialog();
        }

        private void CustomerModule_Closed(object? sender, EventArgs e)
        {
            
        }
    }
}
