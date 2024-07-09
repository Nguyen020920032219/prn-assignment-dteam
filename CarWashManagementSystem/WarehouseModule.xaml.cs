using Repository.Entities;
using Service;
using System;
using System.Windows;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for WarehouseModule.xaml
    /// </summary>
    public partial class WarehouseModule : Window
    {
        ProductService _productService;
        ValidationService _validation;

        public WarehouseModule()
        {
            InitializeComponent();
            _productService = new ProductService();
            _validation = new ValidationService();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var name = txtName.Text;
            var description = txtDescription.Text;
            var priceText = txtPrice.Text;
            var quantityText = txtQuantity.Text;

            if (!_validation.IsStringValid(name))
            {
                MessageBox.Show("Tên sản phẩm không được để trống.");
                return;
            }

            if (!_validation.IsStringLengthValid(name, 1, 50))
            {
                MessageBox.Show("Tên sản phẩm phải từ 1 đến 50 ký tự.");
                return;
            }

            if (!_validation.IsStringValid(description))
            {
                MessageBox.Show("Mô tả sản phẩm không được để trống.");
                return;
            }

            if (!_validation.IsStringValid(priceText))
            {
                MessageBox.Show("Giá sản phẩm không được để trống.");
                return;
            }

            if (!_validation.IsNumber(priceText))
            {
                MessageBox.Show("Giá sản phẩm phải là một con số.");
                return;
            }

            if (!_validation.IsStringValid(quantityText))
            {
                MessageBox.Show("Số lượng sản phẩm không được để trống.");
                return;
            }

            if (!_validation.IsNumber(quantityText))
            {
                MessageBox.Show("Số lượng sản phẩm phải là một con số.");
                return;
            }

            if (!_validation.IsPositiveInteger(Int32.Parse(quantityText)))
            {
                MessageBox.Show("Số lượng sản phẩm phải là số nguyên dương.");
                return;
            }

            var price = decimal.Parse(priceText);
            var quantity = Int32.Parse(quantityText);

            Product product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = quantity,
                IsDeleted = false
            };

            try
            {
                _productService.CreateProduct(product);
                MessageBox.Show("Sản phẩm đã được thêm thành công.");
                Close_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm sản phẩm: {ex.Message}");
            }
        }
    }
}
