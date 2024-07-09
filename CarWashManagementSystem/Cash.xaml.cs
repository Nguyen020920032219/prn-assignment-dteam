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
    public partial class Cash : Window
    {
        //private decimal? totalPrice = 0;

        private IOrderService orderService;

        private UserControl? activeWindown = null;

        //Track which is Vehicle of customer selected
        private CustomerVehicleDTO customerVehicleSelected;

        //private List<Repository.Entities.Service> servicesSelected;

        //Track what are services of vehicle selected to display 
        private ObservableCollection<dynamic> cashRecords = new ObservableCollection<dynamic>();

        //Track what are services of which is vehicle selected 
        private Dictionary<int, List<Repository.Entities.Service>> vehicleServices = new Dictionary<int, List<Repository.Entities.Service>>();

        public Cash()
        {
            InitializeComponent();
            orderService = new OrderServiceImpl();

            GetTransactionNo();
            dgvCash.ItemsSource = cashRecords;
            btnAddService.IsEnabled = false;
        }

        public void OpenChildWindown(UserControl childWindown)
        {
            if (activeWindown != null)
            {
                panelCash.Children.Remove(activeWindown);
            }

            activeWindown = childWindown;
            dgvCash.Margin = new Thickness(0, 271, 0, 80);
            panelCash.Height = 200;
            panelCash.Children.Add(childWindown);
        }

        public void CloseChildWindown()
        {
            if (activeWindown != null)
            {
                panelCash.Children.Remove(activeWindown);
                activeWindown = null;
                dgvCash.Margin = new Thickness(0, 71, 0, 80); // Reset margin to original if needed
            }
        }

        public void GetTransactionNo()
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            string? MaxTransactionNoStartingWithDate = orderService.GetMaxTransactionNoStartingWithDate(date);

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
            cashCustomer.onCustomerSelected = OnCustomerSelected;
            OpenChildWindown(cashCustomer);
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            List<Repository.Entities.Service> selectedServices;
            vehicleServices.TryGetValue(customerVehicleSelected.VehicleId, out selectedServices);

            CashService cashService = new CashService(customerVehicleSelected, selectedServices);
            cashService.OnServiceSelectionCompleted = OnServiceSelectionCompleted;
            OpenChildWindown(cashService);
        }

        private void OnCustomerSelected(CustomerVehicleDTO customerVehicleSelected)
        {
            this.customerVehicleSelected = customerVehicleSelected;
            btnAddService.IsEnabled = true; // Enable service button when customer is selected
            btnAddService_Click(null, null); // Automatically open CashService
        }

        //Hidden serives are selected by a customer
        //Show services again if choice anther cust
        private void OnServiceSelectionCompleted(List<Repository.Entities.Service> selectedServices)
        {
            if (customerVehicleSelected != null)
            {
                if (vehicleServices.ContainsKey(customerVehicleSelected.VehicleId))
                {
                    vehicleServices[customerVehicleSelected.VehicleId].AddRange(selectedServices);
                }
                else if (cashRecords.Count > 0)
                {
                    vehicleServices[customerVehicleSelected.VehicleId] = new List<Repository.Entities.Service>(selectedServices);
                    vehicleServices.Remove(cashRecords[0].VehicleId);
                } else
                {
                    vehicleServices[customerVehicleSelected.VehicleId] = new List<Repository.Entities.Service>(selectedServices);
                }
            }

            //Clear previous services if a different vehicle is selcted
            if (cashRecords.Count > 0 && cashRecords[0].VehicleId != customerVehicleSelected.VehicleId) 
            {
                cashRecords.Clear();
            }

            DisplaySelectedInfo(customerVehicleSelected, selectedServices);
            UpdateTotalPrice();
            CloseChildWindown();
        }

        private void UpdateTotalPrice()
        {
            decimal totalPrice = 0;

            foreach(var record in cashRecords)
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
                cashRecords.Add(new
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

            cashRecords.Remove(serviceRecord);
            UpdateTotalPrice();

            if (vehicleServices.ContainsKey((int)customerId))
            {
                var services = vehicleServices[(int)customerId];
                var serviceToRemove = services.FirstOrDefault(s => s.ServiceId == (int)serviceId);

                if (serviceToRemove != null)
                {
                    services.Remove(serviceToRemove);
                }
            }
        }
    }
}
