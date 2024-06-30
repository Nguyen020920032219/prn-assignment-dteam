using Repository.Entities;
using Service;
using Service.Impl;
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
        private IEmployeeService employeeService;

        public Employer()
        {
            InitializeComponent();
        }

        public void Employer_Load(object sender, EventArgs e)
        {
            employeeService = new EmployeeService();

            List<TbEmployee> tbEmployees = employeeService.GetTbEmployeesList();

            dgvEmployer.ItemsSource = tbEmployees;

        }

        public void EmployeeSearch(object sender, EventArgs e)
        {
            
        }
    }
}
