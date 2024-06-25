using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace CarWashManagementSystem
{
    public partial class EmployerModule : Window

    {
        SqlCommand cm = new SqlCommand();
        String title = "Car Wash Managent System";
        private void btClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFullName.Text))
                {
                    MessageBox.Show("Please enter the full name.");
                    return;
                }

                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    MessageBox.Show("Please enter the phone number.");
                    return;
                }

                if (string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("Please enter the address.");
                    return;
                }

                if (!dpDob.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select a date of birth.");
                    return;
                }

                if (rdMale.IsChecked == false && rdFemale.IsChecked == false)
                {
                    MessageBox.Show("Please select a gender.");
                    return;
                }

                if (string.IsNullOrEmpty(cbRole.Text))
                {
                    MessageBox.Show("Please select a role.");
                    return;
                }

                if (string.IsNullOrEmpty(txtSalary.Text))
                {
                    MessageBox.Show("Please enter the salary.");
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Please enter the password.");
                    return;
                }
                if (MessageBox.Show("Are you sure you want register this employer ?", "Employer Registration", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbEmployee(name,phone,address,dob,gender,role,salary,password)VALUES(@name,@phone,@address,@dob,@gender,@role,@salary,@password)");
                    cm.Parameters.AddWithValue("@name", txtFullName.Text);
                    cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@dob", dpDob.SelectedDate.Value);
                    cm.Parameters.AddWithValue("@gender", rdFemale.IsChecked == true ?"Male":"Female");
                    cm.Parameters.AddWithValue("@role", cbRole.Text);
                    cm.Parameters.AddWithValue("@salary", txtSalary.Text);
                    cm.Parameters.AddWithValue("@password", txtPassword.Text);

                   
                    cm.ExecuteNonQuery();
                    
                    MessageBox.Show("Employer has been successfully registered!", title);
                }
            }catch(Exception ex) {
                MessageBox.Show(ex.Message, title);
            }
            catch (Exception ex)
        {
                MessageBox.Show(ex.Message, title);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        public void clear()
        {
            txtFullName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtPassword.Clear();
            txtSalary.Clear();

            dpDob.SelectedDate = DateTime.Now;
        }
    }
}
