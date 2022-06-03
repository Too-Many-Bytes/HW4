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
    /// Логика взаимодействия для AddFilmWindow.xaml
    /// </summary>
    public partial class AddFilmWindow : Window
    {

        public Storage Storage { get; set; }
        public AddFilmWindow(Storage storage)
        {
            InitializeComponent();

            Storage = storage;
        }

        private void AddFilmButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilmName.Text == "" | FilmName.Text == null)
            {
                MessageBox.Show("Некорректный ввод названия фильма!");
            } 
            else if (!CheckAgeRate(FilmAgeRate.Text))
            {
                MessageBox.Show("Некорректный возрастной рейтинг!");
            }
            else if (!CheckDuration(FilmDuration.Text))
            {
                MessageBox.Show("Некорректная продолжительность фильма!");
            }
            else if (!CheckRoomName(RoomName.Text))
            {
                MessageBox.Show("Некорректное название зала!");
            }
            else if (!CheckNumber(RoomNumOfRows.Text))
            {
                MessageBox.Show("Некорректное количество рядов!");
            } 
            else if (!CheckNumber(RoomNumOfSeats.Text))
            {
                MessageBox.Show("Некорректное количество мест!");
            }
            else if (!CheckNumber(RoomTicketPrice.Text))
            {
                MessageBox.Show("Некорректная цена билета!");
            }
            else if (!CheckFilmTime(SessoinFilmTime.Text))
            {
                MessageBox.Show("Некорректное время сеанса!");
            }
            else
            {
                TimeSpan.TryParse(FilmDuration.Text, out TimeSpan duration);
                DateTime.TryParse(SessoinFilmTime.Text, out DateTime sessionDateTime);
                Film film = new Film(FilmName.Text, FilmAgeRate.Text, duration);
                Room room = new Room(RoomName.Text, Int32.Parse(RoomNumOfRows.Text), Int32.Parse(RoomNumOfSeats.Text),  Int32.Parse(RoomTicketPrice.Text));
                film.Rooms.Add(room);
                room.Film = film;
                Session session = new Session(sessionDateTime, room);
                room.Sessions.Add(session);
                
                Storage.AllFilms.Add(film);
                Storage.SaveData();
                MessageBox.Show("Фильм успешно создан!");
                Close();
            }
                
        }


        private bool CheckAgeRate(string s)
        {
            bool isValid = false;
            for (int i = 0; i < Film.AgeRates.Length; i++)
            {
                if (s == Film.AgeRates[i])
                {
                    isValid = true;
                    break;
                }
            }

            if (isValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool CheckDuration(string s)
        {
            if (TimeSpan.TryParse(s, out TimeSpan duration))
            {
                if (duration < TimeSpan.Zero | duration > new TimeSpan(10, 0, 0))
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }

            else
            {
                return false;
            }
        }


        private bool CheckRoomName(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            bool isCreated = false;

            foreach (Room createdRoom in Storage.AllRooms)
            {
                if (createdRoom.Name == s)
                {
                    return false;
                }
            }

            if (!isCreated)
            {
                return true;
            }

            return false;
        }


        private bool CheckNumber(string s)
        {
            if (int.TryParse(s, out int num))
            {
                if (num <= 0) { return false; }

                else { return true; }
            }

            else { return false; }
        }


        private bool CheckFilmTime(string s)
        {
            if (DateTime.TryParse(s, out DateTime sessionDateTime))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
