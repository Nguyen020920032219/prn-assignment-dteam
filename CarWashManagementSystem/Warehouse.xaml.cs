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
    public partial class Warehouse : Window
    {
        ProductService _productService;
        ValidationService _validation;
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
            dgvProduct.ItemsSource = _productService.GetProducts();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvProduct.ItemsSource = _productService.GetProductsContainString(txtSearch.Text);
        }

        private void dgvProduct_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
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

            //if (!_validation.IsStringValid(product.Price))
            //{
            //    MessageBox.Show("Giá sản phẩm không được để trống.");
            //    e.Cancel = true;
            //    return;
            //}

            if (!_validation.IsNumber(product.Price+""))
            {
                MessageBox.Show("Product price must be a number.");
                e.Cancel = true;
                return;
            }

            //if (!_validation.IsStringValid(product.StockQuantity))
            //{
            //    MessageBox.Show("Số lượng sản phẩm không được để trống.");
            //    e.Cancel = true;
            //    return;
            //}

            if (!_validation.IsNumber(product.StockQuantity + ""))
            {
                MessageBox.Show("Product quantity must be a number.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsPositiveInteger(Int32.Parse(product.StockQuantity + "")))
            {
                MessageBox.Show("Product quantity must be positve number.");
                e.Cancel = true;
                return;
            }

            _productService.UpdateProduct(product);
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

            MessageBoxResult result = MessageBox.Show(
                $"Are you sure to delete '{product.Name}'?",
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
                    MessageBox.Show($"Error while deleting product: {ex.Message}");
                }
            }
            ShowData();
        }

        private void StockIn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Product product= (Product)btn.DataContext;

            StockIn s = new StockIn(product);
            s.WindowStartupLocation= WindowStartupLocation.CenterScreen;
            s.Closed += Module_Closed;
            s.ShowDialog();
        }
    }
}
