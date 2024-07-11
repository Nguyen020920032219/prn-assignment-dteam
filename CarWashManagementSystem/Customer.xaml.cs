using Repository.Entities;
using Service;
using System.Windows;
using System.Windows.Controls;


namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        CustomerService _customerService;
        ValidationService _validation;
        public Customer()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _validation = new ValidationService();
            ShowData();
        }

        private void ShowData()
        {
            dgvCustomer.ItemsSource = null;
            dgvCustomer.ItemsSource = _customerService.GetCustomers();
        }

        private void dgvCustomer_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Repository.Entities.Customer customer = e.Row.Item as Repository.Entities.Customer;

            if (!_validation.IsStringValid(customer.Name))
            {
                MessageBox.Show("Customer name can not be empty.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsStringValid(customer.Phone))
            {
                MessageBox.Show("Customer phone number can not be empty.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsPhoneNumberValid(customer.Phone))
            {
                MessageBox.Show("Customer phone number is not valid.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsStringValid(customer.Address))
            {
                MessageBox.Show("Customer Address can not be empty.");
                e.Cancel = true;
                return;
            }

            _customerService.UpdateCustomer(customer);
            ShowData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Repository.Entities.Customer customer = (Repository.Entities.Customer)btn.DataContext;

            MessageBoxResult result = MessageBox.Show(
                $"Are you sure to delete '{customer.Name}'?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _customerService.DeleteCustomer(customer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while deleting customer: {ex.Message}");
                }
            }
            ShowData();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvCustomer.ItemsSource = _customerService.GetCustomersContainString(txtSearch.Text);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            CustomerModule wm = new CustomerModule();
            wm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            wm.Closed += Close_Module;
            wm.ShowDialog();
        }

        private void Close_Module(object? sender, EventArgs e)
        {
            ShowData();
        }

        private void ManageVehicle_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Repository.Entities.Customer customer = (Repository.Entities.Customer)btn.DataContext;

            Vehicle mv=new Vehicle(customer);
            mv.WindowStartupLocation= WindowStartupLocation.CenterScreen;
            mv.Closed += Close_Module;
            mv.ShowDialog();
        }
    }
}
