﻿using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Service;
using Service.Impl;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarWashManagementSystem
{
    /// <summary>
    /// Interaction logic for SettlePayment.xaml
    /// </summary>
    public partial class SettlePayment : Window
    {
        private IOrderService _orderService;

        public Order order { get; set; }
         
        public SettlePayment()
        {
            InitializeComponent();
            _orderService = new OrderServiceImpl();
        }

        public void InitializePayment()
        {
            if (order != null)
            {
                txtSale.Text = order.TotalPrice.ToString();
                txtCash.Text = "0";
                txtChange.Text = "0";
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btn0.Content.ToString();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btn1.Content.ToString();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btn2.Content.ToString();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btn3.Content.ToString();
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btn4.Content.ToString();
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btn5.Content.ToString();
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btn6.Content.ToString();
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btn7.Content.ToString();
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btn8.Content.ToString();
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btn9.Content.ToString();
        }

        private void btn00_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btn00.Content.ToString();
        }

        private void btnPoint_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Text += btnPoint.Content.ToString();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        { 
            if (double.Parse(txtCash.Text) - double.Parse(txtSale.Text) >= 0) {
                _orderService.CompleteOrder(order);
                MessageBox.Show("Pay successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true;
                this.Close();
            } else
            {
                MessageBox.Show("Cash money can not less than sale money", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        public void txtCash_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double charge = double.Parse(txtCash.Text) - double.Parse(txtSale.Text);

                if (charge <= 0)
                {
                    txtChange.Text = "0.00";
                } else
                {
                    txtChange.Text = $"{charge:N2}";
                }
            } catch (Exception ex)
            {
                txtChange.Text = "0.00";
            }
        }
    }
}
