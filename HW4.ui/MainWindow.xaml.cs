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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW4.ui
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int userAuthId = 0;
        const int adminAuthId = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AuthorizationUserButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorisationWindow = new AuthorizationWindow(userAuthId, this);

            authorisationWindow.Show();
            
        }


        private void AuthorizationAdminButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorisationWindow = new AuthorizationWindow(adminAuthId, this);

            authorisationWindow.Show();
        }


        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(this);

            registrationWindow.Show();

        }
    }
}
