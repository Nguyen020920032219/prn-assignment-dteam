using Repository.Entities;
using Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        AccountService _accountService;
        ValidationService _validationService;
        public Account()
        {
            InitializeComponent();
            _accountService = new AccountService();
            _validationService = new ValidationService();
            ShowData();
        }

        private void ShowData()
        {
            dgvAccount.ItemsSource = null;
            dgvAccount.ItemsSource = _accountService.getListAccount();
        }

        private void dgvAccount_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Repository.Entities.Account account = e.Row.Item as Repository.Entities.Account;
            if (!_validationService.IsStringValid(account.Password))
            {
                MessageBox.Show("Password can not be empty!");
                e.Cancel = true;
                return;
            }
            _accountService.UpdateAccount(account);
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvAccount.ItemsSource  = _accountService.GetAccountsByUsername(txtSearch.Text);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AccountModule am = new AccountModule();
            am.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            am.Closed += Module_Closed;
            am.ShowDialog();
        }

        private void Module_Closed(object? sender, EventArgs e)
        {
            ShowData();
        }
    }
}
