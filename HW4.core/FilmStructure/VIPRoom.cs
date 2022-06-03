using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.core.FilmStructure
{
    internal sealed class VIPRoom : Room
    {
        [JsonProperty]
        List<string> Menu { get; set; }


        [JsonConstructor]
        public VIPRoom(List<string> dishes, string name, int numOfRows, int numOfSeats, int[,] prices, List<Session> sessions) : base(name, numOfRows, numOfSeats, prices, sessions)
        {
            Menu = dishes;
        }


        public VIPRoom(string name, string rowsSeats, Film film) : base(name, rowsSeats, film)
        {
            Menu = new List<String>();
        }

    }
}
