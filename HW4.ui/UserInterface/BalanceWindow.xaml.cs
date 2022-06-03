using HW4.core;
using HW4.core.Users;
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

namespace HW4.ui
{
    /// <summary>
    /// Логика взаимодействия для BalanceWindow.xaml
    /// </summary>
    public partial class BalanceWindow : Window
    {

        public User CurrentUser { get; set; }

        public Storage Storage { get; set; }

        public BalanceWindow(User user, Storage storage)
        {
            InitializeComponent();
            Storage = storage;
            CurrentUser = user;
            CurrentBalance.Content = "Ваш баланс: " + CurrentUser.Money + " руб.";

        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            if (sumOfPay.Text == "")
            {
                MessageBox.Show("Введите сумму пополнения!");
            }
            else if (int.TryParse(sumOfPay.Text, out int sum))
            {
                if (sum > 0)
                {
                    CurrentUser.Money += sum;
                    MessageBox.Show("Баланс успешно пополнен!");
                    CurrentBalance.Content = "Ваш баланс: " + CurrentUser.Money + " руб.";
                    Storage.SaveUsers();
                    
                }

                else
                {
                    MessageBox.Show("Некорректная сумма!");
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод!");
            }
        }
    }
}
