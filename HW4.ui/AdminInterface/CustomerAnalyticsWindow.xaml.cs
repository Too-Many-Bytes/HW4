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
    /// Логика взаимодействия для CustomerAnalyticsWindow.xaml
    /// </summary>
    public partial class CustomerAnalyticsWindow : Window
    {

        public Storage Storage { get; set; }

        public CustomerAnalyticsWindow(Storage storage)
        {
            InitializeComponent();

            Storage = storage;

            UsersMostSessionsListBox.ItemsSource = Storage.Users.OrderBy(c => c.BoughtSessions.Count).Reverse().ToList();
            UsersMostTicketsListBox.ItemsSource = Storage.Users.OrderBy(c => c.NumOfTickets).Reverse().ToList();
            UsersMostMoneyListBox.ItemsSource = Storage.Users.OrderBy(c => c.MoneySpent).Reverse().ToList();
        }

        private void FioTextBlock_Initialized(object sender, EventArgs e)
        {
            var fioTextBlock = sender as TextBlock;

            var user = fioTextBlock.DataContext as User;

            fioTextBlock.Text = user.FIO;
        }

        private void NumSessionsTextBlock_Initialized(object sender, EventArgs e)
        {
            var numSessionsTextBlock = sender as TextBlock;

            var user = numSessionsTextBlock.DataContext as User;

            numSessionsTextBlock.Text = "Всего сеансов: " + user.BoughtSessions.Count;
        }

        private void NumTicketsTextBlock_Initialized(object sender, EventArgs e)
        {
            var numTicketsTextBlock = sender as TextBlock;

            var user = numTicketsTextBlock.DataContext as User;

            numTicketsTextBlock.Text = "Всего билетов: " + user.NumOfTickets;
        }

        private void AmountMoneyTextBlock_Initialized(object sender, EventArgs e)
        {
            var moneyTextBlock = sender as TextBlock;

            var user = moneyTextBlock.DataContext as User;

            moneyTextBlock.Text = "Всего потрачено: " + user.MoneySpent + " руб";
        }
    }
}
