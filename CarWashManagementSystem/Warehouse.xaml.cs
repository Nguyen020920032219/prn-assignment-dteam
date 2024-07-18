using Repository.Entities;
using Service;
using System.Windows;
using System.Windows.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Xml.Linq;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Warehouse.xaml
    /// </summary>
    public partial class Warehouse : UserControl
    {
        ProductService _productService;
        ValidationService _validation;
        bool _isDiscontinued = false;
        public Warehouse()
        {
            InitializeComponent();
            _productService = new ProductService();
            _validation = new ValidationService();
            ShowData();
        }

        private void ShowData()
        {
            dgvProduct.ItemsSource = null;
            dgvProduct.ItemsSource = _productService.GetProductsByStatus(_isDiscontinued);
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvProduct.ItemsSource = _productService.GetProductsContainString(txtSearch.Text, _isDiscontinued);
        }

        private void dgvProduct_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (_isDiscontinued)
            {
                MessageBox.Show("Reactive this product to do this.");
                e.Cancel = true;    
                return;
            }

            Product product = e.Row.Item as Product;

            if (!_validation.IsStringValid(product.Name))
            {
                MessageBox.Show("Product name can not be empty.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsStringValid(product.Description))
            {
                MessageBox.Show("Product description can not be empty.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsStringValid(product.Price.ToString()))
            {
                MessageBox.Show("Product price can not be empty.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsNumber(product.Price.ToString()))
            {
                MessageBox.Show("Product price must be a number.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsWithinRange((decimal)product.Price, 0, decimal.MaxValue))
            {
                MessageBox.Show("Product price must be greater than 0 and smaller than " + decimal.MaxValue + ".");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsStringValid(product.StockQuantity.ToString()))
            {
                MessageBox.Show("Product quantity can not be empty.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsNumber(product.StockQuantity.ToString()))
            {
                MessageBox.Show("Product quantity must be a number.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsWithinRange((int)product.StockQuantity, 0, int.MaxValue))
            {
                MessageBox.Show("Product price must be greater than 0 and smaller than " + int.MaxValue + ".");
                e.Cancel = true;
                return;
            }

            _productService.UpdateProduct(product);
            MessageBox.Show("Product update successfully.");
            ShowData();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            WarehouseModule wm = new WarehouseModule();
            wm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            wm.Closed += Module_Closed;
            wm.ShowDialog();
        }
        private void Module_Closed(object sender, EventArgs e)
        {
            ShowData();
        }

        private void Discontinued_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Product product = (Product)btn.DataContext;

            if (product.IsDiscontinued)
            {
                MessageBoxResult result = MessageBox.Show(
                $"Are you sure to move '{product.Name}' to actively product?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        product.IsDiscontinued = false;
                        _productService.UpdateProduct(product);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error while processing: {ex.Message}");
                    }
                }
                ShowData();
            }
            else
            {

                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure to move '{product.Name}' to discontinued product?",
                    "Confirm",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _productService.DeleteProduct(product);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error while processing: {ex.Message}");
                    }
                }
                ShowData();
            }
        }

        private void StockIn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Product product = (Product)btn.DataContext;

            if (product.IsDiscontinued)
            {
                MessageBox.Show("Reactive this product to do this.");
                return;
            }

            StockIn s = new StockIn(product);
            s.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            s.Closed += Module_Closed;
            s.ShowDialog();
        }

        private void ChangeView_Click(object sender, RoutedEventArgs e)
        {
            if (_isDiscontinued == false)
            {
                _isDiscontinued = true;
                btnChangeView.Content = "Actively Product";
                Add.IsEnabled = false;
            }
            else
            {
                _isDiscontinued = false;
                btnChangeView.Content = "Discontinued product";
                Add.IsEnabled = true;
            }

            ShowData();
        }
    }
}
