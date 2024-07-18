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
    public partial class EmployerModule : Window
    {
        EmployeeService _employeeService;
        ValidationService _validationService;
        public EmployerModule()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _validationService = new ValidationService();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var fullname = txtFullName.Text;
            DateOnly? selectedDate = dpDob.SelectedDate.HasValue ? DateOnly.FromDateTime(dpDob.SelectedDate.Value.Date) : (DateOnly?)null;
            var phone = txtPhone.Text;
            var address = txtAddress.Text;
            var gender = "";
            if (rdFemale.IsChecked == true)
            {
                gender = "Female";
            }
            if (rdMale.IsChecked == true)
            {
                gender = "Male";
            }
            if (!_validationService.IsStringValid(fullname))
            {
                MessageBox.Show("Fullname can not be empty!");
                return;
            }
            if (!_validationService.IsStringValid(phone))
            {
                MessageBox.Show("Phone can not be empty!");
                return;
            }
            if (!_validationService.IsStringValid(address))
            {
                MessageBox.Show("Address can not be empty!");
                return;
            }
            Employee employer = new Employee
            {
                Name = fullname,
                Phone = phone,
                Address = address,
                DateOfBirth = selectedDate,
                Gender = gender,
                IsActive = true
            };
            try
            {
                _employeeService.CreateEmployee(employer);
                MessageBox.Show("Product added.");
                btnClose_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while adding product: {ex.Message}");
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
