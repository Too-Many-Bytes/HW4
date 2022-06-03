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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public Storage Storage { get; set; }

        public Admin CurrentAdmin { get; set; }

        public AdminWindow(string login)
        {
            InitializeComponent();
            Storage = new Storage();

            CurrentAdmin = Storage.GetAdminByLogin(login);
        }

        private void EditFilmButton_Click(object sender, RoutedEventArgs e)
        {
            EditFilmChoiceWindow editFilmChoiceWindow = new EditFilmChoiceWindow(Storage);
            editFilmChoiceWindow.Show();
        }

        private void EditRoomButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditSessionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SalesAnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            SalesAnalyticsWindow salesAnalyticsWindow = new SalesAnalyticsWindow();
            salesAnalyticsWindow.Show();
        }

        private void CustomerAnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerAnalyticsWindow customerAnalyticsWindow = new CustomerAnalyticsWindow(Storage);
            customerAnalyticsWindow.Show();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            foreach (Window window in Application.Current.Windows)
            {
                if (!window.Equals(mainWindow))
                {
                    window.Close();
                }

            }
        }
    }
}
