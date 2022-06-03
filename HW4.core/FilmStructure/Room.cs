using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.core
{
    public class Room
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonIgnore]
        public Film Film { get; set; }

        [JsonProperty]
        internal int NumOfRows { get; private set; }

        [JsonProperty]
        internal int NumOfSeats { get; private set; }

        [JsonProperty]
        public int[,] Prices { get; private set; }

        [JsonProperty]
        public List<Session> Sessions { get; private set; }


        [JsonConstructor]
        public Room(string name, int numOfRows, int numOfSeats, int[,] prices, List<Session> sessions)
        {
            Name = name;
            NumOfRows = numOfRows;
            NumOfSeats = numOfSeats;
            Prices = prices;
            Sessions = sessions;

        }


        public Room(string name, string rowsSeats, Film film)
        {
            Name = name;
            Film = film;
            int[] parsed = rowsSeats.Split().Select(x => Int32.Parse(x)).ToArray();
            NumOfRows = parsed[0];
            NumOfSeats = parsed[1];
            Prices = new int[NumOfRows, NumOfSeats];
            Sessions = new List<Session>();
        }


        public Room(string name, int numOfRows, int numOfSeats, int price)
        {
            Name = name;
            NumOfRows = numOfRows;
            NumOfSeats = numOfSeats;
            Prices = new int[numOfRows, numOfSeats];
            for (int i = 0; i < numOfRows; i++)
            {
                for (int j = 0; j < numOfSeats; j++)
                {
                    Prices[i, j] = price;
                }
            }

            Sessions = new List<Session>();

        }
    }
}
