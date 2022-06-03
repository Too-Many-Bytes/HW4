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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    /// 

   

    public partial class RegistrationWindow : Window
    {
        public MainWindow MainWindow { get; set; }

        public RegistrationWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            MainWindow = mainWindow;
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            string userFIO = fioReg.Text.Trim();
            string userLogin = logReg.Text.Trim();
            string userPassword = pswReg.Password;

            if (userFIO == "")
            {
                MessageBox.Show("Введите ФИО!");
            } 
            else if (userLogin == "")
            {
                MessageBox.Show("Введите логин!");
            }
            else if (userPassword.Trim() == "")
            {
                if (userPassword.Length != 0)
                {
                    MessageBox.Show("Пароль должен содержать непустые символы!");
                }
                else
                {
                    MessageBox.Show("Заполните поле пароля!");
                }
            }
            else
            {
                Storage storage = new Storage();
                if (storage.CheckLoginAvailability(userLogin))
                {
                    storage.Users.Add(new User(userFIO, userLogin, userPassword));
                    storage.SaveUsers();
                    MessageBox.Show("Вы успешно зарегистрированы!");
                    MainWindow.Close();
                    UserWindow userWindow = new UserWindow(userLogin);
                    userWindow.Show();
                    Close();
                    
                    

                }
                else
                {
                    MessageBox.Show("Данный логин уже занят :(");
                }
                
              
            }
        }
    }
}
