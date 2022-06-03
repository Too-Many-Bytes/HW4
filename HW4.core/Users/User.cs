using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.core.Users
{
    public class User
    {
        public string Login { get; set; }

        public string Password { get; set; }

        [JsonProperty]
        public string FIO { get; private set; }

        [JsonProperty]
        public int Money { get; set; }

        [JsonProperty]
        public int MoneySpent { get; set; }

        [JsonProperty]
        public int NumOfTickets { get; set; }

        [JsonProperty]
        public List<Session> BoughtSessions { get; private set; }

        [JsonProperty]
        public List<Ticket> Tickets { get; private set; }


        [JsonConstructor]
        public User(string log, string psw, string fio, int money, int moneySpent, int numOfTickets, List<Session> boughtSessions, List<Ticket> tickets)
        {
            Login = log;
            Money = money;
            Password = psw;
            FIO = fio;
            Tickets = tickets;
            BoughtSessions = boughtSessions;
            MoneySpent = moneySpent;
            NumOfTickets = numOfTickets;
        }


        public User(string fio, string log, string psw)
        {
            Login = log;
            Money = 0;
            Password = psw;
            FIO = fio;
            Tickets = new List<Ticket>();
            BoughtSessions = new List<Session>();
            MoneySpent = 0;
            NumOfTickets = 0;
        }
    }
}
