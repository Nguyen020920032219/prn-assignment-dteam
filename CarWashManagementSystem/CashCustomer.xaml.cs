using Microsoft.IdentityModel.Tokens;
using Repository.DTO;
using Service;
using System.Windows;
using System.Windows.Controls;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for CashCustomer.xaml
    /// </summary>
    public partial class CashCustomer : UserControl
    {
        private CustomerService _customerService;

        public Action<CustomerVehicleDTO> OnCustomerSelected { get; set; }

        public CashCustomer()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            CashCustomer_Load();
        }

        public void CashCustomer_Load()
        {
            dgvCustomer.ItemsSource = _customerService.GetAllCustomersWithVehicles();
        }

        public void SearchCustomerByName(object sender, TextChangedEventArgs e)
        {
            if (dgvCustomer != null)
            {
                dgvCustomer.Items.Filter = CustomerFilter;
                dgvCustomer.Items.Refresh();
            }
        }

        //Filter Customer is valid for SearchCustomerByName
        public bool CustomerFilter(object item)
        {
            if (txtSearch.Text.Trim().IsNullOrEmpty())
            {
                return true;
            }

            var customer = item as CustomerVehicleDTO;
            return customer != null && customer.Name.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public void SelectCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var customerVehicelSelected = dgvCustomer.SelectedItem as CustomerVehicleDTO;
            OnCustomerSelected?.Invoke(customerVehicelSelected);
        }
    }
}
