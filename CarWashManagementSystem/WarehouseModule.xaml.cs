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
                MessageBox.Show("Product name can not be empty.");
                return;
            }

            if (!_validation.IsStringValid(description))
            {
                MessageBox.Show("Product description can not be empty.");
                return;
            }

            if (!_validation.IsStringValid(priceText))
            {
                MessageBox.Show("Product price can not be empty.");
                return;
            }

            if (!_validation.IsNumber(priceText))
            {
                MessageBox.Show("Product price must be a number.");
                return;
            }

            if (!_validation.IsWithinRange(decimal.Parse(priceText),0,decimal.MaxValue))
            {
                MessageBox.Show("Product price must be greater than 0 and smaller than " + decimal.MaxValue + ".");
                return;
            }

            if (!_validation.IsStringValid(quantityText))
            {
                MessageBox.Show("Product quantity can not be empty.");
                return;
            }

            if (!_validation.IsNumber(quantityText))
            {
                MessageBox.Show("Product quantity must be a number.");
                return;
            }

            if (!_validation.IsWithinRange(int.Parse(quantityText), 0, int.MaxValue))
            {
                MessageBox.Show("Product price must be greater than 0 and smaller than " + int.MaxValue + ".");
                return;
            }

            var price = decimal.Parse(priceText);
            var quantity = int.Parse(quantityText);

            Product product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = quantity,
                IsDiscontinued = false
            };

            try
            {
                _productService.CreateProduct(product);
                MessageBox.Show("Product add successfully.");
                Close_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while adding product: {ex.Message}");
            }
        }
    }
}
