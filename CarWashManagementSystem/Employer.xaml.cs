using Service;
using Service.Impl;
using System.Windows;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Employer.xaml
    /// </summary>
    public partial class Employer : Window
    {
        private IEmployeeService employeeService = new EmployeeService();

        public Employer()
        {
            InitializeComponent();
        }

        public void Employer_Load(object sender, EventArgs e)
        {
            dgvEmployer.ItemsSource = null;
            dgvEmployer.ItemsSource = employeeService.GetTbEmployeesList();
        }

    }
}
