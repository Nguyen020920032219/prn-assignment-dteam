using Repository.Entities;
using Service;
using System.Windows;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private AccountService _accountService;

        public Login()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUsername.Text;
            string password = cbShowPassword.IsChecked == true ? txtPasswordVisible.Text : txtPassword.Password;

            Account? account = _accountService.GetEmployeeByUserNameAndPassword(userName, password);

            if (account != null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Wrong username or pasword", "Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            txtPasswordVisible.Text = txtPassword.Password;
            txtPassword.Visibility = Visibility.Hidden;
            txtPasswordVisible.Visibility = Visibility.Visible;
        }

        private void cbShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = txtPasswordVisible.Text;
            txtPasswordVisible.Visibility = Visibility.Hidden;
            txtPassword.Visibility = Visibility.Visible;
        }
    }
}
