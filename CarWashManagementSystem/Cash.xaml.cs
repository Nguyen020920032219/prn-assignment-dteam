using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Identity.Client;
using Repository.DTO;
using Repository.Entities;
using Service;
using Service.Impl;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Cash.xaml
    /// </summary>
    public partial class Cash : UserControl
    {
        //private decimal? totalPrice = 0;

        private IOrderService _orderService;

        private UserControl? _activeWindown = null;

        //Track which is Vehicle of customer selected
        private CustomerVehicleDTO _customerVehicleSelected;

        //private List<Repository.Entities.Service> servicesSelected;

        //Track what are services of vehicle selected to display 
        private ObservableCollection<dynamic> _cashRecords = new ObservableCollection<dynamic>();

        //Track what are services of which is vehicle selected 
        private Dictionary<int, List<Repository.Entities.Service>> _vehicleServices = new Dictionary<int, List<Repository.Entities.Service>>();

        public Cash()
        {
            InitializeComponent();
            _orderService = new OrderServiceImpl();

            GetTransactionNo();
            dgvCash.ItemsSource = _cashRecords;
            btnAddService.IsEnabled = false;
        }

        public void OpenChildWindown(UserControl childWindown)
        {
            if (_activeWindown != null)
            {
                panelCash.Children.Remove(_activeWindown);
            }

            _activeWindown = childWindown;
            dgvCash.Margin = new Thickness(0, 271, 0, 80);
            panelCash.Height = 200;
            panelCash.Children.Add(childWindown);
        }

        public void CloseChildWindown()
        {
            if (_activeWindown != null)
            {
                panelCash.Children.Remove(_activeWindown);
                _activeWindown = null;
                dgvCash.Margin = new Thickness(0, 71, 0, 80); // Reset margin to original if needed
            }
        }

        public void GetTransactionNo()
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            string? MaxTransactionNoStartingWithDate = _orderService.GetMaxTransactionNoStartingWithDate(date);

            if (MaxTransactionNoStartingWithDate != null)
            {
                int count = int.Parse(MaxTransactionNoStartingWithDate.Substring(8, 4));
                lblTransactionNo.Content = date + (count + 1).ToString("D4");
            }
            else
            {
                lblTransactionNo.Content = date + "0001";
            }

        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CashCustomer cashCustomer = new CashCustomer();
            cashCustomer.OnCustomerSelected = OnCustomerSelected;
            OpenChildWindown(cashCustomer);
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            List<Repository.Entities.Service> servicesSelected;
            _vehicleServices.TryGetValue(_customerVehicleSelected.VehicleId, out servicesSelected);

            CashService cashService = new CashService(_customerVehicleSelected, servicesSelected);
            cashService.OnServiceSelectionCompleted = OnServiceSelectionCompleted;
            OpenChildWindown(cashService);
        }

        private void OnCustomerSelected(CustomerVehicleDTO customerVehicleSelected)
        {
            this._customerVehicleSelected = customerVehicleSelected;
            btnAddService.IsEnabled = true; // Enable service button when customer is selected
            btnAddService_Click(null, null); // Automatically open CashService
        }

        //Hidden serives are selected by a customer
        //Show services again if choice anther cust
        private void OnServiceSelectionCompleted(List<Repository.Entities.Service> servicesSelected)
        {
            if (_customerVehicleSelected != null)
            {
                if (_vehicleServices.ContainsKey(_customerVehicleSelected.VehicleId))
                {
                    _vehicleServices[_customerVehicleSelected.VehicleId].AddRange(servicesSelected);
                }
                else if (_cashRecords.Count > 0)
                {
                    _vehicleServices[_customerVehicleSelected.VehicleId] = new List<Repository.Entities.Service>(servicesSelected);
                    _vehicleServices.Remove(_cashRecords[0].VehicleId);
                } else
                {
                    _vehicleServices[_customerVehicleSelected.VehicleId] = new List<Repository.Entities.Service>(servicesSelected);
                }
            }

            //Clear previous services if a different vehicle is selcted
            if (_cashRecords.Count > 0 && _cashRecords[0].VehicleId != _customerVehicleSelected.VehicleId) 
            {
                _cashRecords.Clear();
            }

            DisplaySelectedInfo(_customerVehicleSelected, servicesSelected);
            UpdateTotalPrice();
            CloseChildWindown();
        }

        private void UpdateTotalPrice()
        {
            decimal totalPrice = 0;

            foreach(var record in _cashRecords)
            {
                totalPrice += record.ServicePrice;
            }

            lblTotalPrice.Content = $"{totalPrice:N2}";
        }

        public void DisplaySelectedInfo(CustomerVehicleDTO customerVehicleSelected, List<Repository.Entities.Service> servicesSelected)
        {
            // Display the selected info in the DataGrid
            foreach (var service in servicesSelected)
            {
                _cashRecords.Add(new
                {
                    CustomerId = customerVehicleSelected.CustomerId,
                    TransactionNo = lblTransactionNo.Content,
                    Name = customerVehicleSelected.Name,
                    Phone = customerVehicleSelected.Phone,
                    Address = customerVehicleSelected.Address,

                    VehicleId = customerVehicleSelected.VehicleId,
                    VehicleNo = customerVehicleSelected.VehicleNo,
                    VehicleMake = customerVehicleSelected.VehicleMake,
                    VehicleModel = customerVehicleSelected.VehicleModel,
                    VehicleYear = customerVehicleSelected.VehicleYear,

                    ServiceId = service.ServiceId,
                    ServiceName = service.Name,
                    ServicePrice = service.Price
                });
            }
        }

        public void DeleteServieButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var serviceRecord = button.DataContext;

            var serviceId = serviceRecord.GetType().GetProperty("ServiceId").GetValue(serviceRecord, null);
            var servicePrice = serviceRecord.GetType().GetProperty("ServicePrice").GetValue(serviceRecord, null);

            var customerId = serviceRecord.GetType().GetProperty("CustomerId").GetValue(serviceRecord, null);

            _cashRecords.Remove(serviceRecord);
            UpdateTotalPrice();

            if (_vehicleServices.ContainsKey((int)customerId))
            {
                var services = _vehicleServices[(int)customerId];
                var serviceToRemove = services.FirstOrDefault(s => s.ServiceId == (int)serviceId);

                if (serviceToRemove != null)
                {
                    services.Remove(serviceToRemove);
                }
            }
        }
    }
}
