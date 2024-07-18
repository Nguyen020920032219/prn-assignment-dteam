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
using Repository.Entities;
using Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for StockIn.xaml
    /// </summary>
    public partial class StockIn : Window
    {
        Product product;
        CostOfGoodService _costOfGoodService;
        ProductService _productService;
        ValidationService _validation;
        public StockIn(Product product)
        {
            InitializeComponent();
            this.product = product;
            this._costOfGoodService = new CostOfGoodService();
            this._productService = new ProductService();
            this._validation = new ValidationService();
            txtName.Text = product.Name;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var priceText = txtPrice.Text;
            var quantityText = txtQuantity.Text;

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

            if (!_validation.IsWithinRange(decimal.Parse(priceText), 0, decimal.MaxValue))
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

            product.StockQuantity += int.Parse(txtQuantity.Text);
            _productService.UpdateProduct(product);

            CostOfGood costOfGood = new CostOfGood();
            costOfGood.ProductId = product.ProductId;
            costOfGood.Date = DateOnly.FromDateTime(DateTime.Now);
            costOfGood.Price = decimal.Parse(priceText);
            costOfGood.Quantity = int.Parse(quantityText);
            costOfGood.Total = decimal.Parse(priceText) * int.Parse(quantityText);

            _costOfGoodService.AddCostOfGood(costOfGood);
            MessageBox.Show("Add successfully.");
            btnCancel_Click(sender, e);
        }
    }
}
