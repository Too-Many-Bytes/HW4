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
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        const int userAuthId = 0;

        const int adminAuthId = 1;

        public MainWindow MainWindow { get; set; }

        public int ActionId { get; set; }

        public AuthorizationWindow(int actionId, MainWindow mainWindow)
        {
            InitializeComponent();
            ActionId = actionId;
            MainWindow = mainWindow;
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            Storage storage = new Storage();

            string login = log.Text;
            string password = psw.Password;

            bool isLog = false;

            if (ActionId == userAuthId)
            {
                foreach (User user in storage.Users)
                {
                    if (user.Login == login)
                    {
                        if (user.Password == password)
                        {
                            isLog = true;
                            MainWindow.Close();
                            UserWindow userWindow = new UserWindow(user.Login);
                            userWindow.Show();
                            Close();
                            break;
                        }

                        else
                        {
                            MessageBox.Show("Неверный пароль! Ты не молодец :(");
                            isLog = true;
                            break;
                        }
                    }
                }

                if (!isLog)
                {
                    MessageBox.Show("Пользователь с таким логином не найден :(");
                }
            }

            else {
                foreach (Admin admin in storage.Admins)
                {
                    if (admin.Login == login)
                    {
                        if (admin.Password == password)
                        {
                            isLog = true;
                            MainWindow.Close();
                            AdminWindow adminWindow = new AdminWindow(admin.Login);
                            adminWindow.Show();
                            Close();
                            break;
                        }

                        else
                        {
                            MessageBox.Show("Неверный пароль!");
                            isLog = true;
                            break;
                        }
                    }
                }

                if (!isLog)
                {
                    MessageBox.Show("Пользователь с таким логином не найден :(");
                }
            }
            

           
        }
    }
}
