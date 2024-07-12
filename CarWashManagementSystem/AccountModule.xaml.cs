using Repository.Entities;
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
    /// Interaction logic for AccountModule.xaml
    /// </summary>
    public partial class AccountModule : Window
    {
        AccountService _accountService;
        ValidationService _validationService;
        EmployeeService _employeeService;
        public AccountModule()
        {
            InitializeComponent();
            _accountService = new AccountService();
            _validationService = new ValidationService();
            _employeeService = new EmployeeService();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var employerID = txtEmployerID.Text;
            var username = txtUsername.Text;
            var password = txtPassword.Text;
            var role = txtRole.Text;
            if (!_validationService.IsNumber(employerID))
            {
                MessageBox.Show("Employer ID must be number");
                return;
            }
            if (!_validationService.IsStringValid(username)) 
            {
                MessageBox.Show("Username can not be empty!");
                return;
            }
            if (!_validationService.IsStringValid(password))
            {
                MessageBox.Show("Password can not be empty!");
                return;
            }
            if (!_validationService.IsStringValid(role))
            {
                MessageBox.Show("Role can not be empty!");
                return;
            }
            if (int.TryParse(txtEmployerID.Text.Trim(), out int employeeId))
            {
                bool employeeExists = _employeeService.GetAllEmployees().Any(emp => emp.EmployeeId == employeeId);
                if (!employeeExists)
                {
                    MessageBox.Show("EmployeeID does not exist");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Invalid EmployeeID format");
                return;
            }

            Repository.Entities.Account account = new Repository.Entities.Account
            {
                EmployeeId = employeeId,
                UserName = username,
                Password = password,
                Role = role,
                IsActive = true
                   
            };
            try
            {
                _accountService.CreateAccount(account);
                MessageBox.Show("Account add successfull!!");
                Close_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
