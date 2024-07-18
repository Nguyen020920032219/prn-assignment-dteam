using Repository.Entities;
using Service;
using System.Windows;
using System.Windows.Controls;

namespace CarWashManagementSystem
{
    public partial class Employer : UserControl
    {
        private readonly EmployeeService _employeeService;
        private readonly ValidationService _validationService;
        private readonly AccountService _accountService;
        public Employer()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _validationService = new ValidationService();
            _accountService = new AccountService();
            ShowData();
        }

        private void ShowData()
        {
            dgvEmployer.ItemsSource = null;
            dgvEmployer.ItemsSource = _employeeService.GetAllEmployees();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgvEmployer.ItemsSource = _employeeService.GetEmployeeByName(txtSearch.Text);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Employee employee = (Employee)btn.DataContext;

            MessageBoxResult result = MessageBox.Show(
                $"Are you sure delete to this employee?",
                "Confirm delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _employeeService.DeleteEmployee(employee);                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error delete employee: {ex.Message}");
                }
            }
            ShowData();
        }

        private void dgvEmployer_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Employee employee = e.Row.Item as Employee;
            if (!_validationService.IsStringValid(employee.Name))
            {
                MessageBox.Show("Employer name can not be empty.");
                e.Cancel = true;
                return;
            }
            if (!_validationService.IsPhoneNumberValid(employee.Phone))
            {
                MessageBox.Show("Employer phone must have 10 number");
                e.Cancel = true;
                return;
            }
            if (!_validationService.IsStringValid(employee.Address))
            {
                MessageBox.Show("Employer address can not be empty");
                e.Cancel = true; return;
            }

            if (!_validationService.IsStringValid(employee.Gender))
            {
                MessageBox.Show("Employer gender can not be empty");
                e.Cancel = true; return;
            }
            _employeeService.UpdateEmployee(employee);
            ShowData();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployerModule em = new EmployerModule();
            em.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            em.Closed += EmployerModule_Closed;
            em.ShowDialog();
        }

        private void EmployerModule_Closed(object? sender, EventArgs e)
        {
            ShowData();
        }
    }
}
