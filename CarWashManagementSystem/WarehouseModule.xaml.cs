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
    /// Interaction logic for ServiceModule.xaml
    /// </summary>
    public partial class WarehouseModule : Window
    {
        ProductService _productService;
        public WarehouseModule()
        {
            InitializeComponent();
            _productService = new ProductService();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var Name = txtName.Text;
            var Description=txtDescription.Text;
            var Price= decimal.Parse(txtPrice.Text);
            var Quantity= Int32.Parse(txtQuantity.Text);

            Product product = new Product();
            product.Name = Name;
            product.Description = Description;
            product.Price = Price;
            product.StockQuantity = Quantity;
            product.IsDeleted = false;

            _productService.CreateProduct(product);

            Close_Click(sender, e);
        }
    }
}
