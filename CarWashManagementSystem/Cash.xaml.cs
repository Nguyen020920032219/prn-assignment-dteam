using Repository.DTO;
using Repository.Entities;
using Service;
using Service.Impl;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Cash.xaml
    /// </summary>
    public partial class Cash : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IOrderService _orderService;

        private IOrderServiceService _orderServiceService;

        private UserControl? _activeWindown = null;

        private bool _isCashButtonEnable;

        //Track which is Vehicle of customer selected
        private CustomerVehicleDTO _customerVehicleSelected;

        //Track what are services of vehicle selected to display 
        private ObservableCollection<dynamic> _cashRecords;

        //Track what are services of which is vehicle selected 
        private Dictionary<int, List<Repository.Entities.Service>> _vehicleServices = new Dictionary<int, List<Repository.Entities.Service>>();

        public Cash()
        {
            InitializeComponent();
            DataContext = this;

            _orderService = new OrderServiceImpl();
            _orderServiceService = new OrderServiceServiceImpl();

            _cashRecords = new ObservableCollection<dynamic>();
            _cashRecords.CollectionChanged += CashRecords_CollectionChanged;
            dgvCash.ItemsSource = _cashRecords;

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

        public bool IsCashButtonEnabled
        {
            get { return _isCashButtonEnable; }

            set { 
                if (_isCashButtonEnable != value)
                {
                    _isCashButtonEnable = value;
                    OnPropertyChanged(nameof(IsCashButtonEnabled));
                }
             }
        }

        private void CashRecords_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsCashButtonEnabled = _cashRecords.Count > 0;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
            string totalPrice = lblTotalPrice.Content.ToString().Replace(",", "");
            decimal.TryParse(totalPrice, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal totalPriceIsConverted);

            // Create and save a new Order
            Order order = new Order()
            {
                TransactionNo = _cashRecords[0].TransactionNo,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Status = "Pending",
                TotalPrice = totalPriceIsConverted,
                VehicleId = _cashRecords[0].VehicleId,
                CustomerId = _cashRecords[0].CustomerId,
                EmployeeId = 1
            };
            _orderService.AddOrder(order);
            Order orderIsSaved = _orderService.GetOrderByTransactionNo(order.TransactionNo);

            // Create and save new OrderServiceList
            List<OrderService> orderServices = new List<OrderService>();
            foreach (var serviceRecord in _cashRecords)
            {
                OrderService orderService = new OrderService()
                {
                    OrderId = orderIsSaved.OrderId,
                    ServiceId = serviceRecord.ServiceId,
                    UnitPrice = serviceRecord.ServicePrice
                };
                orderServices.Add(orderService);
            }
            _orderServiceService.addOrderServiceList(orderServices);

            SettlePayment settlePayment = new SettlePayment();
            settlePayment.order = orderIsSaved;
            settlePayment.InitializePayment();
            settlePayment.Closed += (s, args) =>
            {
                if (settlePayment.DialogResult == true)
                {
                    ResetCash();
                }
            };
            settlePayment.ShowDialog();
        }

        private void ResetCash()
        {
            _cashRecords.Clear(); // Auto disable the cash button when reset
            GetTransactionNo();
            btnAddService.IsEnabled = false; // Disable the service button when reset
        }
    }
}
