using Repository.Entities;
using Service;
using System.Windows;
using System.Windows.Controls;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Warehouse.xaml
    /// </summary>
    public partial class Warehouse : UserControl
    {
        ProductService _productService;
        public Warehouse()
        {
            InitializeComponent();
            _productService = new ProductService();
            ShowData();
        }

        private void ShowData()
        {
            dgvProduct.ItemsSource = null;
            dgvProduct.ItemsSource=_productService.GetProducts();
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
            Product product= e.Row.Item as Product;
            _productService.UpdateProduct(product);
            ShowData();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            WarehouseModule wm = new WarehouseModule();
            wm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            wm.Closed += WarehouseModule_Closed;
            wm.Show();
        }
        private void WarehouseModule_Closed(object sender, EventArgs e)
        {
            ShowData();
        }
    }
}
