using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.core
{
    public class Session
    {
        [JsonIgnore]
        public Room Room { get; set; }

        [JsonProperty]
        public int[,] SeatsAvailable { get; private set; }

        [JsonProperty]
        internal int Revenue { get; private set; }

        [JsonProperty]
        public DateTime FilmTime { get; private set; }

        [JsonProperty]
        public TimeSpan Duration { get; private set; }

        [JsonProperty]
        internal string RoomName { get; private set; }


        [JsonConstructor]
        public Session(int[,] seatsAvailable, int revenue, DateTime sessionTime, TimeSpan duration, string roomName)
        {
            SeatsAvailable = seatsAvailable;
            Revenue = revenue;
            RoomName = roomName;
            FilmTime = sessionTime;
            Duration = duration;
        }


        public Session(DateTime sessionTime, Room room)
        {
            FilmTime = sessionTime;
            RoomName = room.Name;
            Room = room;
            SeatsAvailable = new int[room.NumOfRows, room.NumOfSeats];
            Duration = Room.Film.Duration;
        }

        public void ChangeSeatCondition(List<int[]> boughtSeats)
        {
            foreach (int[] seat in boughtSeats)
            {
                Revenue += Room.Prices[seat[0], seat[1]];
                SeatsAvailable[seat[0], seat[1]] = -1;
            }
        }
    }
}
