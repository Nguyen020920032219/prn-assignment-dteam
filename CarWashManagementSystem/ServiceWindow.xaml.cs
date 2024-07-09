using Repository.Entities;
using service;
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
    public partial class ServiceWindow : Window
    {
        ServiceService _services;
        public ServiceWindow()
        {
            InitializeComponent();
            _services = new ServiceService();
            ViewData();
        }

        private void ViewData()
        {
            try
            {
                dgvService.ItemsSource = _services.GetList();
            } catch {
                MessageBox.Show("Cannot access with data");
            }

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ServiceModule serviceModule = new ServiceModule();  
            serviceModule.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            serviceModule.Closed += ServiceModule_Closed;
            serviceModule.Show();


        }

        private void ServiceModule_Closed(object? sender, EventArgs e)
        {
            ViewData();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            
         
            Repository.Entities.Service service = button.DataContext as Repository.Entities.Service;
            service.IsDeleted = true;
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
                );

            if ( result == MessageBoxResult.Yes )
            {
                try
                {       _services.DeleteService(service);
                        ViewData();
                        MessageBox.Show("Delete successfull");                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hide service failed because: " + ex.Message);
                }
            }
            
        }

        private void dgvService_Update(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                var Service = e.Row.Item as Repository.Entities.Service;
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
            catch (Exception ex) {
                MessageBox.Show("Update was failed beacause: " + ex.Message);
            }
        }
    }
}
