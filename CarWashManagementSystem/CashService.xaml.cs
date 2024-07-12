using Microsoft.IdentityModel.Tokens;
using Repository.DTO;
using Service;
using Service.Impl;
using System.Windows;
using System.Windows.Controls;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for CashService.xaml
    /// </summary>
    public partial class CashService : UserControl
    {
        private IServiceService _serviceService;

        public CustomerVehicleDTO? CustomerVehicleSelected { get; set; }

        public List<Repository.Entities.Service> ServicesSelected { get; private set; }

        public Action<List<Repository.Entities.Service>> OnServiceSelectionCompleted { get; set; }

        public CashService(CustomerVehicleDTO? customerVehicleSelected, List<Repository.Entities.Service> alreadySelectedServices)
        {
            InitializeComponent();
            _serviceService = new ServiceServiceImpl();

            CustomerVehicleSelected = customerVehicleSelected;
            ServicesSelected = new List<Repository.Entities.Service>();
            CashService_Load(alreadySelectedServices);
        }

        public void CashService_Load(List<Repository.Entities.Service> alreadySelectedServices)
        {
            var allServices = _serviceService.GetServicesIsNotDeleteList();
            
            if(alreadySelectedServices != null)
            {
                // Filter out already selected services
                dgvService.ItemsSource = allServices.Where(service => !alreadySelectedServices.Any(selected => selected.ServiceId == service.ServiceId)).ToList();
            } else
            {
                dgvService.ItemsSource = allServices;
            }
        }

        public void SearchServiceByName(object sender, TextChangedEventArgs e)
        {
            if (dgvService != null)
            {
                dgvService.Items.Filter = ServiceFilter;
                dgvService.Items.Refresh();
            }
        }

        //Filter Service is valid for SearchServiceByName
        public bool ServiceFilter(object item)
        {
            if (txtSearch.Text.Trim().IsNullOrEmpty())
            {
                return true;
            }

            var service = item as Repository.Entities.Service;
            return service != null && service.Name.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void btnSubmit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var item in dgvService.SelectedItems)
            {
                ServicesSelected.Add(item as Repository.Entities.Service);
            }

            OnServiceSelectionCompleted?.Invoke(ServicesSelected);
        }
    }
}
