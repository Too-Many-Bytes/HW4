using HW4.core;
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
    /// Логика взаимодействия для EditFilmWindow.xaml
    /// </summary>
    public partial class EditFilmWindow : Window
    {
        public Storage Storage { get; set; }

        public Film Film { get; set; }

        public EditFilmWindow(Film film, Storage storage)
        {
            InitializeComponent();

            Storage = storage;
            Film = film;

            FilmNameText.Content = Film.FilmName;
            FilmAgeRateText.Content = Film.AgeRate;
            FilmDurationText.Content = Film.Duration;

        }

        private void ChangeNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewName.Text == "" | NewName.Text == null)
            {
                MessageBox.Show("Название фильма не может быть пустым!");
                return;
            }
            else
            {
                Film.FilmName = NewName.Text;
                FilmNameText.Content = Film.FilmName;
                Storage.SaveData();
            }
        }

        private void ChangeAgeRateButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = false; 
            for (int i = 0; i < Film.AgeRates.Length; i++)
            {
                if (NewAgeRate.Text == Film.AgeRates[i])
                {
                    isValid = true;
                    break;
                }
            }

            if (isValid)
            {
                Film.AgeRate = NewAgeRate.Text;
                FilmAgeRateText.Content = NewAgeRate.Text;
                Storage.SaveData();
            }
            else
            {
                MessageBox.Show("Некорректный ввод!");
            }
           
        }

        private void ChangeDurationButton_Click(object sender, RoutedEventArgs e)
        {
            if (TimeSpan.TryParse(NewDuration.Text, out TimeSpan duration))
            {
                if (duration < TimeSpan.Zero | duration > new TimeSpan(10, 0, 0))
                {
                    MessageBox.Show("Некорректный ввод!");
                }

                else
                {
                    Film.Duration = duration;
                    FilmDurationText.Content = Film.Duration;
                    Storage.SaveData();
                }
            }

            else
            {
                MessageBox.Show("Некорректный ввод!");
            }
        }

        private void DeleteFilmButton_Click(object sender, RoutedEventArgs e)
        {
            Film.Sessions.Clear();
            Film.Rooms.Clear();

            Storage.DeleteAllRoomsOfFilm(Film.FilmName);

            Storage.RemoveFilm(Film);

            MessageBox.Show("Фильм успешно удален!");

            Storage.SaveData();

            Close();
        }
    }
}
