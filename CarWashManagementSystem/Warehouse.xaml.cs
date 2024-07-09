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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Product product = (Product)btn.DataContext;

            MessageBoxResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sản phẩm '{product.Name}' không?",
                "Xác nhận xóa",
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
                    MessageBox.Show($"Lỗi khi xóa sản phẩm: {ex.Message}");
                }
            }
            ShowData();
        }

        private void dgvProduct_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Product product = e.Row.Item as Product;

            if (!_validation.IsStringValid(product.Name))
            {
                MessageBox.Show("Tên sản phẩm không được để trống.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsStringValid(product.Description))
            {
                MessageBox.Show("Mô tả sản phẩm không được để trống.");
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
                MessageBox.Show("Giá sản phẩm phải là một con số.");
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
                MessageBox.Show("Số lượng sản phẩm phải là một con số.");
                e.Cancel = true;
                return;
            }

            if (!_validation.IsPositiveInteger(Int32.Parse(product.StockQuantity + "")))
            {
                MessageBox.Show("Số lượng sản phẩm phải là số nguyên dương.");
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
            wm.Closed += WarehouseModule_Closed;
            wm.ShowDialog();
        }
        private void WarehouseModule_Closed(object sender, EventArgs e)
        {
            ShowData();
        }
    }
}
