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
    /// Interaction logic for ManageVehicleType.xaml
    /// </summary>
    public partial class Vehicle : Window
    {
        Repository.Entities.Customer customer;
        VehicleService _vehicleService;
        public Vehicle(Repository.Entities.Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
            this._vehicleService = new VehicleService();
            ShowData();
        }

        private void ShowData()
        {
            dgvVehicle.ItemsSource = null;
            dgvVehicle.ItemsSource = _vehicleService.GetVehiclesByCustomerId(customer.CustomerId);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            VehicleModule vm=new VehicleModule(customer);
            vm.WindowStartupLocation= WindowStartupLocation.CenterScreen;
            vm.Closed += VehicleModule_Closed;
            vm.ShowDialog();
        }

        private void VehicleModule_Closed(object sender, EventArgs e)
        {
            ShowData();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                dgvVehicle.ItemsSource = _vehicleService.GetVehiclesContainString(txtSearch.Text);
            }
            else
            {
                ShowData();
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Repository.Entities.Vehicle vehicle = (Repository.Entities.Vehicle)btn.DataContext;

            MessageBoxResult result = MessageBox.Show(
                $"Are you sure to delete?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _vehicleService.DeleteVehicle(vehicle);
                    MessageBox.Show("Delete successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while deleting vehicle: {ex.Message}");
                }
            }
            ShowData();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
