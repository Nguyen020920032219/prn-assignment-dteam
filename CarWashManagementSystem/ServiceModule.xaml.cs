
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
using System.Windows.Xps;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for ServiceModule.xaml
    /// </summary>
    public partial class ServiceModule : Window
    {
        ServiceService _service;
        public ServiceModule()
        {
            InitializeComponent();
            _service = new ServiceService();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Name = txtName.Text;
                var Description = txtDescription.Text;
                var Price = decimal.Parse(txtPrice.Text);

                Repository.Entities.Service service = new Repository.Entities.Service();
                service.Name = Name;
                service.Description = Description;
                service.Price = Price;
                service.IsDeleted = false;

                _service.addService(service);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add service failed because: " + ex.Message);
            }
            Close_Click(sender, e);

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close_Click(sender, e);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
