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
    /// Interaction logic for CustomerModule.xaml
    /// </summary>
    public partial class CustomerModule : Window
    {
        CustomerService _customerService;
        ValidationService _validation;
        public CustomerModule()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _validation = new ValidationService();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var Name=txtName.Text;
            var Phone=txtPhone.Text;
            var Address=txtAddress.Text;

            if (!_validation.IsStringValid(Name))
            {
                MessageBox.Show("Customer name can not be empty.");
                return;
            }

            if (!_validation.IsStringValid(Phone))
            {
                MessageBox.Show("Customer phone number can not be empty.");
                return;
            }

            if (!_validation.IsPhoneNumberValid(Phone))
            {
                MessageBox.Show("Customer phone number is not valid.");
                return;
            }

            if (!_validation.IsStringValid(Address))
            {
                MessageBox.Show("Customer Address can not be empty.");
                return;
            }

            Repository.Entities.Customer customer = new Repository.Entities.Customer();
            customer.Name = Name;
            customer.Phone = Phone;
            customer.Address = Address;

            _customerService.CreateCustomer(customer);
            MessageBox.Show("Customer add successfully.");
            btnClose_Click(sender,e);
        }
    }
}
