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
    /// Логика взаимодействия для BuyTicketsWindow.xaml
    /// </summary>
    public partial class BuyTicketsWindow : Window
    {

        public Storage Storage { get; set; }

        public Session Session { get; set; }

        public List<int[]> ChosenSeats { get; set; }
        public BuyTicketsWindow(Session session, Storage storage)
        {
            InitializeComponent();
            Storage = storage;
            Session = session;
            ChosenSeats = new List<int[]>();
            InitializeGrid(Session);
            SumOfOrderText.Content = "Сумма заказа: 0 руб";
            FilmNameText.Content = "Фильм: " + session.Room.Film.FilmName;
            FilmTimeText.Content = "Время сеанса: " + session.FilmTime;
        }



        private void InitializeGrid(Session session)
        {
            for (int i = 0; i < session.SeatsAvailable.GetLength(0); i++)
            {
                SeatsGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < session.SeatsAvailable.GetLength(1); i++)
            {
                SeatsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            int iRow = -1;
            foreach (RowDefinition rowDefinition in SeatsGrid.RowDefinitions)
            {
                iRow++;
                int iCol = -1;
                foreach (ColumnDefinition columnDefinition in SeatsGrid.ColumnDefinitions)
                {
                    iCol++;

                    Border panel = new Border();
                    Grid.SetColumn(panel, iCol);
                    Grid.SetRow(panel, iRow);

                    Label lbl = new Label();
                    lbl.Content = "Р" + (iCol + 1) + "М" + (iRow + 1);
                    lbl.HorizontalAlignment = HorizontalAlignment.Center;
                    lbl.VerticalAlignment = VerticalAlignment.Center;
                    panel.Child = lbl;

                    panel.Margin = new Thickness(5);

                    panel.PreviewMouseLeftButtonDown += Panel_PreviewMouseLeftButtonDown;

                    panel.HorizontalAlignment = HorizontalAlignment.Center;
                    panel.VerticalAlignment = VerticalAlignment.Center;

                    if (session.SeatsAvailable[iRow, iCol] == 0)
                    {
                        panel.Background = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        panel.Background = new SolidColorBrush(Colors.Red);
                    }

                    SeatsGrid.Children.Add(panel);

                }
            }
        }

        private void Panel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border panel = sender as Border;

            if (Session.SeatsAvailable[Grid.GetRow(panel), Grid.GetColumn(panel)] == 0)
            {
                bool isChosen = false;
                for (int i = 0; i < ChosenSeats.Count; i++)
                {
                    if (ChosenSeats[i][0] == Grid.GetRow(panel) & ChosenSeats[i][1] == Grid.GetColumn(panel))
                    {
                        isChosen = true; 
                        ChosenSeats.RemoveAt(i);
                        panel.Background = new SolidColorBrush(Colors.Green);
                        SumOfOrderText.Content = "Сумма заказа: " + CalcSumOfOrder(ChosenSeats) + " руб";
                        break;
                    }
                }

                if (!isChosen)
                {
                    panel.Background = new SolidColorBrush(Colors.Yellow);
                    int[] pair = new int[2];
                    pair[0] = Grid.GetRow(panel);
                    pair[1] = Grid.GetColumn(panel);
                    ChosenSeats.Add(pair);
                    SumOfOrderText.Content = "Сумма заказа: " + CalcSumOfOrder(ChosenSeats) + " руб";
                }
            }
            else
            {
                MessageBox.Show("Место занято!");
            }
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (Storage.CurrentUser.Money < CalcSumOfOrder(ChosenSeats))
            {
                MessageBox.Show("Недостаточно средств!");
            }
            else
            {
                if (!CheckSeatsAvailability(ChosenSeats))
                {
                    MessageBox.Show("Некоторые места были выкуплены!");
                    BuyTicketsWindow buyTicketsWindow = new BuyTicketsWindow(Session, Storage);
                    buyTicketsWindow.Show();
                    Close();
                    return;
                }
                Session.ChangeSeatCondition(ChosenSeats);
                Storage.CurrentUser.Money -= CalcSumOfOrder(ChosenSeats);
                Storage.CurrentUser.MoneySpent += CalcSumOfOrder(ChosenSeats);
                Storage.CurrentUser.NumOfTickets += ChosenSeats.Count;

                bool wasUserOnThisSession = false;

                foreach (Session session in Storage.CurrentUser.BoughtSessions)
                {
                    if (Session.Equals(session))
                    {
                        wasUserOnThisSession = true;
                        break;
                    }
                }

                if (!wasUserOnThisSession)
                {
                    Storage.CurrentUser.BoughtSessions.Add(Session);
                }

                SumOfOrderText.Content = "Сумма заказа: 0 руб";

                for (int i = 0; i < ChosenSeats.Count; i++)
                {
                    Storage.CurrentUser.Tickets.Add(new Ticket(ChosenSeats[i][0], ChosenSeats[i][1], Session.FilmTime, Storage.CurrentUser.FIO, Session));

                    foreach (Border panel in SeatsGrid.Children)
                    {
                        if (Grid.GetRow(panel) == ChosenSeats[i][0] & Grid.GetColumn(panel) == ChosenSeats[i][1])
                        {
                            panel.Background = new SolidColorBrush(Colors.Red);
                        }
                       
                    }
                }



            }
            ChosenSeats.Clear();
            Storage.SaveData();
        }


        private int CalcSumOfOrder(List<int[]> seats)
        {
            int sum = 0;

            for (int i = 0; i < seats.Count; i++)
            {
                sum += Session.Room.Prices[seats[i][0], seats[i][1]];
            }

            return sum;
        }

        private bool CheckSeatsAvailability(List<int[]> seats)
        {
            
            for (int i = 0; i < seats.Count; i++)
            {
                if (Session.SeatsAvailable[seats[i][0], seats[i][1]] != 0) {
                    return false;
                }
            }

            return true;
        }
    }

}
