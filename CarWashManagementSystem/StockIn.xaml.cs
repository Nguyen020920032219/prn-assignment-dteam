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

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for StockIn.xaml
    /// </summary>
    public partial class StockIn : Window
    {
        Product product;
        public StockIn(Product product)
        {
            InitializeComponent();
            this.product = product;
            txtName.Text = product.Name;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CostOfGood costOfGood = new CostOfGood();
            costOfGood.ProductId=product.ProductId;
            costOfGood.
        }
    }
}
