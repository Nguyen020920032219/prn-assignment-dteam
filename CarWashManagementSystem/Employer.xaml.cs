using Repository.Entities;
using Service;
using Service.Impl;
using System.Windows;
using System.Windows.Controls;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Employer.xaml
    /// </summary>
    public partial class Employer : Window
    {
        private IEmployeeService employeeService = new EmployeeServiceImpl();

        public Employer()
        {
            InitializeComponent();
            //Employer_Load();
        }

        //public void Employer_Load()
        //{
        //    dgvEmployer.ItemsSource = null;
        //    dgvEmployer.ItemsSource = employeeService.GetTbEmployeesList();
        //}

        public void SearchTbEmployer(object sender, TextChangedEventArgs e)
        {
        //    dgvEmployer.ItemsSource = null;
        //    dgvEmployer.ItemsSource = employeeService.GetTbEmployeeContainNameList(txtSearch.Text);
        }
    }
}
