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
        ValidationService _validation; 
        public VehicleModule(Repository.Entities.Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
            _vehicleService = new VehicleService();
            _validation = new ValidationService();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var LicensePlate=txtLicensePlate.Text;
            var Make=txtMake.Text;
            var Model=txtModel.Text;
            var Year=txtYear.Text;
            if (!_validation.IsStringValid(LicensePlate)) {
                throw new ArgumentNullException("License plate cannot be empty or null!!!");
            }
            if (!_validation.IsStringValid(Make)) {
                throw new ArgumentNullException("Make cannot be empty or null!!!");
            }
            if (!_validation.IsStringValid(Model)) {
                throw new ArgumentNullException("Modelcannot be empty or null!!!");
            }
            if (!_validation.IsStringValid(Year)) {
                throw new ArgumentNullException("Year plate cannot be empty or null!!!");
            }

            if (!_validation.isFormat(@"^(19|20|21)\d{2}$", Year))
            {
                throw new ArgumentException("Year wrong: " + Year);
            }
           try
            {
                Repository.Entities.Vehicle vehicle = new Repository.Entities.Vehicle();
                vehicle.Make = Make;
                vehicle.Model = Model;
                vehicle.LicensePlate = LicensePlate;
                vehicle.Year = int.Parse(Year);
                vehicle.CustomerId = customer.CustomerId;

                _vehicleService.AddVehicle(vehicle);
            }
            catch
            {
                throw;
            }

            Cancel_Click(sender, e);
        }
    }
}
