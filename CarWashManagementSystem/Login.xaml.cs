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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            IEmployeeService employeeService = new EmployeeService();

            TbEmployee? tbEmployee = employeeService.GetTbEmployee(txtUsername.Text.Trim(), txtPassword.Password.Trim());

            if (tbEmployee == null)
            {
                MessageBox.Show("Wrong Username or Password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
