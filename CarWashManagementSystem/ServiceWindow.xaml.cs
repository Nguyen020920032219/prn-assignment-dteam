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
    /// Interaction logic for Service.xaml
    /// </summary>
    public partial class ServiceWindow : UserControl
    {
        ServiceService _services;
        ValidationService _validationService;
        bool _isDiscontinued = false;

        public ServiceWindow()
        {
            InitializeComponent();
            _services = new ServiceService();
            _validationService = new ValidationService();
            ViewData();
        }

        private void ViewData()
        {
            try
            {
                dgvService.ItemsSource = _services.GetServicesByStatus(_isDiscontinued);
            }
            catch
            {
                MessageBox.Show("Cannot access with data");
            }

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvService.ItemsSource=_services.GetServicesContainString(txtSearch.Text, _isDiscontinued);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ServiceModule serviceModule = new ServiceModule();
            serviceModule.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            serviceModule.Closed += ServiceModule_Closed;
            serviceModule.ShowDialog();

        }

        private void ServiceModule_Closed(object? sender, EventArgs e)
        {
            ViewData();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Repository.Entities.Service service = (Repository.Entities.Service)btn.DataContext;

            if (service.IsDiscontinued)
            {
                MessageBoxResult result = MessageBox.Show(
                $"Are you sure to move '{service.Name}' to actively product?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        service.IsDiscontinued = false;
                        _services.UpdateService(service);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error while processing: {ex.Message}");
                    }
                }
                ViewData();
            }
            else
            {

                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure to move '{service.Name}' to discontinued product?",
                    "Confirm",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        service.IsDiscontinued = true;
                        _services.UpdateService(service);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error while processing: {ex.Message}");
                    }
                }
                ViewData();
            }
        }

        private void dgvService_Update(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (_isDiscontinued)
            {
                MessageBox.Show("Reactive service to do this process.");
                e.Cancel = true;
                return;
            }

            var Service = e.Row.Item as Repository.Entities.Service;
            if (!_validationService.IsStringValid(Service.Name))
            {
                MessageBox.Show("Name cannot be null or empty");
                e.Cancel = true;
                return;
            }
            if (!_validationService.IsStringValid(Service.Description))
            {
                MessageBox.Show("Description cannot be null or empty");
                e.Cancel = true;
                return;
            }

            if (!_validationService.IsPositiveDecimal((decimal)Service.Price))
            {
                MessageBox.Show("Price cannot be null or empty");
                e.Cancel = true;
                return;
            }
            try
            {

                if (Service == null)
                {
                    MessageBox.Show("Row is null!!");
                    return;
                }
                if (Service.Price.GetType() != typeof(decimal))
                {
                    MessageBox.Show("Update was failed because price must be number");
                    return;
                }
                _services.UpdateService(Service);
                ViewData();
                MessageBox.Show("Update successfully!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update was failed beacause: " + ex.Message);
            }
        }

        private void ChangeView_Click(object sender, RoutedEventArgs e)
        {
            if (_isDiscontinued == false)
            {
                _isDiscontinued = true;
                btnChangeView.Content = "Actively Product";
                btnAdd.IsEnabled = false;
            }
            else
            {
                _isDiscontinued = false;
                btnChangeView.Content = "Discontinued product";
                btnAdd.IsEnabled = true;
            }

            ViewData();
        }
    }
}
