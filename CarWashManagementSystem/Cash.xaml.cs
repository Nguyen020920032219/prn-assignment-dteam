using Repository.DTO;
using Repository.Entities;
using Service;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Cash.xaml
    /// </summary>
    public partial class Cash : UserControl
    {
        private Service.OrderService _orderService;

        private OrderServiceService _orderServiceService;

        private UserControl? _activeWindown = null;

        //private bool _isCashButtonEnable;

        //Track which is Vehicle of customer selected
        private CustomerVehicleDTO _customerVehicleSelected;

        //Track what are services of vehicle selected to display 
        private ObservableCollection<dynamic> _cashRecords;

        //Track what are services of which is vehicle selected 
        private Dictionary<int, List<Repository.Entities.Service>> _vehicleServices;

        private Order _currentOrder;

        public Cash()
        {
            InitializeComponent();
            DataContext = this;

            _orderService = new Service.OrderService();
            _orderServiceService = new OrderServiceService();

            _cashRecords = new ObservableCollection<dynamic>();
            dgvCash.ItemsSource = _cashRecords;

            _vehicleServices = new Dictionary<int, List<Repository.Entities.Service>>();


            GetTransactionNo();
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
        //Show services again if choice anther customer
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

        private void btnCash_Click(object sender, RoutedEventArgs e)
        {
            // If there are no cash records, do nothing
            if (_cashRecords.Count == 0)
            {
                MessageBox.Show("No services selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string transactionNo = _cashRecords[0].TransactionNo;
            var existingOrder = _orderService.GetOrderByTransactionNo(transactionNo);

            string totalPrice = lblTotalPrice.Content.ToString().Replace(",", "");
            decimal.TryParse(totalPrice, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal totalPriceIsConverted);

            if (existingOrder == null)
            {
                // Create and save a new Order
                _currentOrder = new Order()
                {
                    TransactionNo = _cashRecords[0].TransactionNo,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Status = false,
                    TotalPrice = totalPriceIsConverted,
                    VehicleId = _cashRecords[0].VehicleId,
                    CustomerId = _cashRecords[0].CustomerId,
                    EmployeeId = 1
                };
                _orderService.AddOrder(_currentOrder);
                Order orderIsSaved = _orderService.GetOrderByTransactionNo(_currentOrder.TransactionNo);
                _currentOrder.OrderId = orderIsSaved.OrderId;

                // Create and save new OrderServiceList
                List<Repository.Entities.OrderService> orderServices = new List<Repository.Entities.OrderService>();
                foreach (var serviceRecord in _cashRecords)
                {
                    Repository.Entities.OrderService orderService = new Repository.Entities.OrderService()
                    {
                        OrderId = orderIsSaved.OrderId,
                        ServiceId = serviceRecord.ServiceId,
                        UnitPrice = serviceRecord.ServicePrice
                    };
                    orderServices.Add(orderService);
                }
                _orderServiceService.AddOrderServiceList(orderServices);
            } else
            {
                _currentOrder = existingOrder;

                // Check if services have changed
                bool servicesChanged = false;
                var currentOrderServices = _orderServiceService.GetOrderServicesByOrderId(_currentOrder.OrderId);

                // Create a list of service IDs from current cash records
                var currentServiceIds = new HashSet<int>(_cashRecords.Select(cr => (int)cr.ServiceId));

                // Check if there's any service that is not in the current cash records
                foreach (var orderService in currentOrderServices)
                {
                    if (!currentServiceIds.Contains((int)orderService.ServiceId))
                    {
                        servicesChanged = true;
                        break;
                    }
                }

                // Check if there's any new service added in the current cash records
                foreach (var serviceId in currentServiceIds)
                {
                    if (!currentOrderServices.Any(os => os.ServiceId == serviceId))
                    {
                        servicesChanged = true;
                        break;
                    }
                }

                // Update order and order services if services have changed
                if (servicesChanged || existingOrder.TotalPrice != totalPriceIsConverted)
                {
                    _currentOrder.TotalPrice = totalPriceIsConverted;
                    _orderService.UpdateOrder(_currentOrder);

                    _orderServiceService.DeleteOrderServicesByOrderId(_currentOrder.OrderId);

                    List<Repository.Entities.OrderService> updatedOrderServices = new List<Repository.Entities.OrderService>();
                    foreach (var serviceRecord in _cashRecords)
                    {
                        Repository.Entities.OrderService orderService = new Repository.Entities.OrderService()
                        {
                            OrderId = _currentOrder.OrderId,
                            ServiceId = serviceRecord.ServiceId,
                            UnitPrice = serviceRecord.ServicePrice
                        };
                        updatedOrderServices.Add(orderService);
                    }
                    _orderServiceService.AddOrderServiceList(updatedOrderServices);
                }
            }


            SettlePayment settlePayment = new SettlePayment();
            settlePayment.order = _currentOrder;
            settlePayment.InitializePayment();
            settlePayment.PaymentCompleted += OnPaymentCompleted;
            settlePayment.ShowDialog();

            if (settlePayment.PaymentSuccessful)
            {
                ResetCash();
            }

        }

        private void OnPaymentCompleted(object sender, EventArgs e)
        {
            _currentOrder.Status = true;
            _orderService.CompleteOrder(_currentOrder);
            ResetCash();
        }

        private void ResetCash()
        {
            _cashRecords.Clear(); // Auto disable the cash button when reset
            _vehicleServices.Clear();
            GetTransactionNo();
            lblTotalPrice.Content = "0.00";
            btnAddService.IsEnabled = false; // Disable the service button when reset
        }
    }
}
