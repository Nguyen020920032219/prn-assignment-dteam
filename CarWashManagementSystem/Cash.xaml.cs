using Service;
using Service.Impl;
using System.Windows;
using System.Windows.Controls;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Cash.xaml
    /// </summary>
    public partial class Cash : Window
    {
        private ICashService cashService = new CashServiceImpl();

        private UserControl activeWindown = null;

        public void OpenChildWindown (UserControl childWindown)
        {
            if (activeWindown != null)
            {
                panelCash.Children.Remove(activeWindown);
            }

            activeWindown = childWindown;
            dgvEmployer.Margin = new Thickness(0, 271, 0, 0);
            panelCash.Height = 200;
            panelCash.Children.Add(childWindown);

            //activeWindown = childWindown;
            //childWindown.Topmost = false;
            //childWindown.WindowStyle = WindowStyle.None;
            //childWindown.HorizontalAlignment = HorizontalAlignment.Stretch;
            //childWindown.VerticalAlignment = VerticalAlignment.Stretch;
            //panelCash.Height = 200;
            //panelCash.Children.Add(childWindown);
            //panelCash.Tag = childWindown;
            //childWindown.BringIntoView();
            //childWindown.Show();
        }

        public Cash()
        {
            InitializeComponent();
            GetTransactionNo();
        }

        public void GetTransactionNo()
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            string? MaxTransactionNoStartingWithDate = cashService.GetMaxTransactionNoStartingWithDate(date);

            if (MaxTransactionNoStartingWithDate != null)
            {
                int count = int.Parse(MaxTransactionNoStartingWithDate.Substring(8, 4));
                lblTransactionNo.Content = date + (count + 1);
            } else
            {
                lblTransactionNo.Content = date + "1001";
            }
            
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            OpenChildWindown(new CashCustomer());
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            OpenChildWindown(new CashService());
        }
    }
}
