using Repository;
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
    /// Interaction logic for VehicleModule.xaml
    /// </summary>
    public partial class VehicleModule : Window
    {
        Repository.Entities.Customer customer;
        VehicleService _vehicleService;
        ValidationService _validationService;
        public VehicleModule(Repository.Entities.Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
            _vehicleService = new VehicleService();
            _validationService = new ValidationService();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var LicensePlate = txtLicensePlate.Text;
            var Make = txtMake.Text;
            var Model = txtModel.Text;
            var YearText = txtYear.Text;

            if (!_validationService.IsStringValid(LicensePlate))
            {
                MessageBox.Show("License plate can not be empty.");
                return;
            }

            if (!_validationService.ValidateLicensePlate(LicensePlate))
            {
                MessageBox.Show("License plate is invalid, it must have 3 charater follow by 3 digit, ex: XXX000");
                return;
            }

            if (!_validationService.IsStringValid(Make))
            {
                MessageBox.Show("Make can not be empty.");
                return;
            }

            if (!_validationService.IsStringValid(Model))
            {
                MessageBox.Show("Model can not be empty.");
                return;
            }

            if (!_validationService.IsStringValid(YearText))
            {
                MessageBox.Show("License plate can not be empty.");
                return;
            }

            if (!_validationService.IsNumber(YearText))
            {
                MessageBox.Show("Year must be a number.");
                return;
            }

            if (!_validationService.IsYear(int.Parse(YearText)))
            {
                MessageBox.Show("Year must be from 1 to 9999.");
                return;
            }

            Repository.Entities.Vehicle vehicle = new Repository.Entities.Vehicle();
            vehicle.Make = Make;
            vehicle.Model = Model;
            vehicle.LicensePlate = LicensePlate;
            vehicle.Year = int.Parse(YearText);
            vehicle.CustomerId = customer.CustomerId;

            _vehicleService.AddVehicle(vehicle);
            MessageBox.Show("Vehicle add successfully.");
            Cancel_Click(sender, e);
        }
    }
}
