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
        public StockIn(Product product)
        {
            InitializeComponent();
            this.product = product;
            this._costOfGoodService = new CostOfGoodService();
            this._productService = new ProductService();
            txtName.Text = product.Name;
            txtPrice.Text=product.Price.ToString();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            product.StockQuantity += int.Parse(txtQuantity.Text);
            _productService.UpdateProduct(product);

            CostOfGood costOfGood = new CostOfGood();
            costOfGood.ProductId=product.ProductId;
            costOfGood.Date = DateOnly.FromDateTime(DateTime.Now);
            costOfGood.Price = decimal.Parse(txtPrice.Text);
            costOfGood.Quantity=int.Parse(txtQuantity.Text);

            _costOfGoodService.AddCostOfGood(costOfGood);

            btnCancel_Click(sender, e);
        }
    }
}
