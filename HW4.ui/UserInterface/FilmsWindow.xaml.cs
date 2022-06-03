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
    /// Логика взаимодействия для BuyTicketsWindow.xaml
    /// </summary>
    public partial class FilmsWindow : Window
    {
        public Storage Storage { get; set; }
        public FilmsWindow(Storage storage)
        {
            InitializeComponent();
            Storage = storage;
            FilmsListBox.ItemsSource = Storage.AllFilms;
        }

        private void AvatarImage_Initialized(object sender, EventArgs e)
        {
            Image filmImage = sender as Image;

            Film film = filmImage.DataContext as Film;

            BitmapImage bitmapImage = new BitmapImage();

            try
            {
                using (var fileStream = new FileStream("../../Images/" + film.FilmName + ".jpg", FileMode.Open))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = fileStream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    filmImage.Source = bitmapImage;
                }
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
                        filmImage.Source = bitmapImage;
                    }

                    filmImage.Source = bitmapImage;
                }
                catch
                {
                    return;
                }
            }

            
        }

        private void NameTextBlock_Initialized(object sender, EventArgs e)
        {
            var nameTextBlock = sender as TextBlock;

            var film = nameTextBlock.DataContext as Film;

            nameTextBlock.Text = film.FilmName;
        }

        private void AgeRateTextBlock_Initialized(object sender, EventArgs e)
        {
            var ageRateTextBlock = sender as TextBlock;

            var film = ageRateTextBlock.DataContext as Film;

            ageRateTextBlock.Text = film.AgeRate;
        }

        private void DurationTextBlock_Initialized(object sender, EventArgs e)
        {
            var durationTextBlock = sender as TextBlock;

            var film = durationTextBlock.DataContext as Film;

            durationTextBlock.Text = film.Duration.ToString();
        }

        //private void FilmsListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    Film listBoxItem = e.Source as Film;

        //    //Film film = listBoxItem.DataContext as Film;

        //    RoomsWindow roomsWindow = new RoomsWindow(listBoxItem, Storage);
        //    roomsWindow.Show();
        //}

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = sender as ListBoxItem;

            Film film = listBoxItem.DataContext as Film;

            SessionsWindow sessionsWindow = new SessionsWindow(film, Storage);
            sessionsWindow.Show();
        }
    }
}
