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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    /// 


    public partial class UserWindow : Window
    {

        public Storage Storage { get; set; }

        public User CurrentUser { get; set; }

        public UserWindow(string userLogin)
        {
            InitializeComponent();
            
            Storage = new Storage();
            CurrentUser = Storage.GetUserByLogin(userLogin);
            Storage.CurrentUser = CurrentUser;
            TitleText.Content = "Добро пожаловать, " + CurrentUser.FIO + "!";
          

        }

        private void BalanceButton_Click(object sender, RoutedEventArgs e)
        {
            BalanceWindow balanceWindow = new BalanceWindow(CurrentUser, Storage);
            balanceWindow.Show();
        }

        private void BuyTicketButton_Click(object sender, RoutedEventArgs e)
        {
            FilmsWindow filmsWindow = new FilmsWindow(Storage);
            filmsWindow.Show();    
        }

        private void BoughtTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            BoughtTicketsWindow boughtTicketsWindow = new BoughtTicketsWindow(Storage);
            boughtTicketsWindow.Show();
        }

        private void ChangeAccountButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            foreach (Window window in Application.Current.Windows)
            {
                if (!window.Equals(mainWindow)) { 
                    window.Close(); 
                }
                
            }
         
            
        }
    }
}
