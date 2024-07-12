using Repository.Entities;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderHistory.xaml
    /// </summary>
    public partial class OrderHistory : UserControl
    {
        private Service.OrderService _orderService;

        private Service.OrderServiceService _orderServiceService;

        private ServiceService _serviceService;

        private CustomerService _customerService;

        private VehicleService _vehicleService;

        private ObservableCollection<dynamic> _cashRecords = new ObservableCollection<dynamic>();

        public OrderHistory()
        {
            InitializeComponent();

            _orderService = new Service.OrderService();
            _orderServiceService = new Service.OrderServiceService();
            _serviceService = new ServiceService();
            _customerService = new CustomerService();
            _vehicleService = new VehicleService();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Order? order = _orderService.GetOrderByTransactionNo(txtTransactionNo.Text);

            if (order == null) 
            {
                MessageBox.Show($"No order have transaction no: {txtTransactionNo.Text}");
                return;
            } 

            List<Repository.Entities.OrderService> orderServices = _orderServiceService.GetOrderServicesByOrderId(order.OrderId);

            Repository.Entities.Customer customer = _customerService.GetCustomerById(order.CustomerId.Value);

            Repository.Entities.Vehicle vehicle = _vehicleService.GetVehicleById(order.VehicleId.Value);

            // Clear previous records
            _cashRecords.Clear();

            // Prepare data for DataGrid using dynamic objects
            foreach (var serviceRecord in orderServices)
            {
                Repository.Entities.Service service = _serviceService.GetServicesById(serviceRecord.ServiceId.Value);

                _cashRecords.Add(new
                {
                    CustomerId = customer.CustomerId,
                    TransactionNo = order.TransactionNo,
                    Name = customer.Name,
                    Phone = customer.Phone ?? string.Empty,
                    Address = customer.Address ?? string.Empty,
                    VehicleId = vehicle.VehicleId,
                    VehicleNo = vehicle.LicensePlate ?? string.Empty,
                    ServiceId = serviceRecord.ServiceId.Value,
                    ServiceName = service.Name ?? string.Empty,
                    ServicePrice = serviceRecord.UnitPrice ?? 0,
                    VehicleMake = vehicle.Make ?? string.Empty,
                    VehicleModel = vehicle.Model ?? string.Empty,
                    VehicleYear = vehicle.Year ?? 0
                });
            }

            // Set the DataGrid's ItemSource
            dgvCash.ItemsSource = _cashRecords;

            // Update total price
            decimal totalPrice = 0;

            foreach (var record in _cashRecords)
            {
                totalPrice += record.ServicePrice;
            }

            lblTotalPrice.Content = $"{totalPrice:N2}";
        }
    }
}
