
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
        ValidationService _validationService;
        public ServiceModule()
        {
            InitializeComponent();
            _service = new ServiceService();
            _validationService = new ValidationService();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var Name = txtName.Text;
            var Description = txtDescription.Text;
                if (!_validationService.IsStringValid(Name))
                {
                    throw new ArgumentNullException("Name cannot be empty or null");
                }
            


            if (!_validationService.IsStringValid(txtPrice.Text))
            {
                throw new ArgumentNullException("Price cannot be null or empty");
            }

         
            if (!_validationService.IsStringValid(Description))
            {
                throw new ArgumentNullException("Description cannot be null or empty");
            }
           

            try
            {
                var Price = decimal.Parse(txtPrice.Text);
                if (!_validationService.IsPositiveDecimal(Price))
                {
                    throw new ArgumentException("Price cannot be negative number");
                }
                
                Repository.Entities.Service service = new Repository.Entities.Service();
                service.Name = Name;
                service.Description = Description;
                service.Price = Price;
                service.IsDiscontinued = false;
                _service.addService(service);
                MessageBox.Show("Add succesfully!");
                Close_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add service failed because: " + ex.Message);
            }

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
