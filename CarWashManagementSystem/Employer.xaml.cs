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

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Employer.xaml
    /// </summary>
    public partial class Employer : Window
    {
        private readonly EmployeeService _employeeService;
        public Employer()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            ShowData();
        }

        private void ShowData()
        {
            dgvEmployer.ItemsSource = null;
            dgvEmployer.ItemsSource = _employeeService.GetEmployees();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvEmployer.ItemsSource = _employeeService.GetEmployeeByName(txtSearch.Text);
        }
    }
}
