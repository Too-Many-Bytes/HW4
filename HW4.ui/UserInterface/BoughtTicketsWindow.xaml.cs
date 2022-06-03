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
    /// Логика взаимодействия для BoughtTicketsWindow.xaml
    /// </summary>
    public partial class BoughtTicketsWindow : Window
    {
        public Storage Storage { get; set; }

        public BoughtTicketsWindow(Storage storage)
        {
            InitializeComponent();
            Storage = storage;
            TicketsListBox.ItemsSource = Storage.CurrentUser.Tickets.OrderBy(c => c.ShowTime).Reverse().ToList();

            if (Storage.CurrentUser.Tickets.Count == 0)
            {
                BoughtTicketsText.Content = "У вас пока нет билетов!";
            }
        }

        private void FilmNameTextBlock_Initialized(object sender, EventArgs e)
        {
            var nameTextBlock = sender as TextBlock;

            var ticket = nameTextBlock.DataContext as Ticket;

            if (ticket.Session != null & ticket.Session.Room != null)
            {
                nameTextBlock.Text = ticket.Session.Room.Film.FilmName;
            }
            else
            {
                nameTextBlock.Text = "Unknown";
            }
        }

        private void SeatTextBlock_Initialized(object sender, EventArgs e)
        {
            var seatTextBlock = sender as TextBlock;

            var ticket = seatTextBlock.DataContext as Ticket;

            seatTextBlock.Text = "Ряд: " + (ticket.Row + 1) + " | Место: " + (ticket.Seat + 1);

        }

        private void FilmTimeTextBlock_Initialized(object sender, EventArgs e)
        {
            var filmTimeTextBlock = sender as TextBlock;

            var ticket = filmTimeTextBlock.DataContext as Ticket;

            filmTimeTextBlock.Text = ticket.ShowTime.ToString();
        }

        private void AvatarImage_Initialized(object sender, EventArgs e)
        {
            Image filmImage = sender as Image;

            Ticket ticket = filmImage.DataContext as Ticket;

            BitmapImage bitmapImage = new BitmapImage();
            if (ticket.Session != null & ticket.Session.Room != null)
            {
                try
                {
                    using (var fileStream = new FileStream("../../Images/" + ticket.Session.Room.Film.FilmName + ".jpg", FileMode.Open))
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
        }
    }
}
