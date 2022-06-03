using HW4.core;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для RoomsWindow.xaml
    /// </summary>
    /// 
    public partial class SessionsWindow : Window
    {

        public Film Film { get; set; }

        public Storage Storage { get; set; }
        public SessionsWindow(Film film, Storage storage)
        {
            InitializeComponent();

            Storage = storage;
            Film = film;
            SessionsListBox.ItemsSource = film.GetRelevantSessions();
        }

        private void RoomsListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AvatarImage_Initialized(object sender, EventArgs e)
        {
            Image filmImage = sender as Image;

            Session session = filmImage.DataContext as Session;

            BitmapImage bitmapImage = new BitmapImage();
            try
            {
                using (var fileStream = new FileStream("../../Images/" + session.Room.Film.FilmName + ".jpg", FileMode.Open))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = fileStream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                }

                filmImage.Source = bitmapImage;
            }
            catch
            {
                try
                {
                    using (var fileStream = new FileStream("../../Images/default.jpg", FileMode.Open))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = fileStream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                    }

                    filmImage.Source = bitmapImage;
                }
                catch
                {
                    return;
                }
            }
        }

        private void RoomNameTextBlock_Initialized(object sender, EventArgs e)
        {
            var roomNameTextBlock = sender as TextBlock;

            var session = roomNameTextBlock.DataContext as Session;

            roomNameTextBlock.Text = session.Room.Name;
        }

        private void DurationTextBlock_Initialized(object sender, EventArgs e)
        {
            var durationTextBlock = sender as TextBlock;

            var session = durationTextBlock.DataContext as Session;

            durationTextBlock.Text = session.Duration.ToString();
        }

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ListBoxItem listBoxItem = sender as ListBoxItem;

            Session session = listBoxItem.DataContext as Session;

            BuyTicketsWindow buyTicketsWindow = new BuyTicketsWindow(session, Storage);
            buyTicketsWindow.Show();
        }

        private void FilmTimeTextBlock_Initialized(object sender, EventArgs e)
        {
            var filmTimeTextBlock = sender as TextBlock;

            var session = filmTimeTextBlock.DataContext as Session;

            filmTimeTextBlock.Text = session.FilmTime.ToString();
        }
    }
}
