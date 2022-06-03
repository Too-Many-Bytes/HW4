using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.core
{
    public class Ticket
    {
        [JsonProperty]
        public int Row { get; }

        [JsonProperty]
        public int Seat { get; }

        [JsonProperty]
        public DateTime ShowTime { get; }

        [JsonProperty]
        public string FIO { get; }

        [JsonProperty]
        public Session Session { get; set; }

        [JsonProperty]
        internal string AdditionalOption { get; }


        [JsonConstructor]
        public Ticket(int row, int seat, DateTime showTime, string fio, Session session, string additionalOption)
        {
            Row = row;
            Seat = seat;
            ShowTime = showTime;
            FIO = fio;
            Session = session;
            AdditionalOption = additionalOption;
        }


        public Ticket(int row, int seat, DateTime showTime, string fio, Session session)
        {
            Row = row;
            Seat = seat;
            ShowTime = showTime;
            FIO = fio;
            Session = session;
        }

    }
}
