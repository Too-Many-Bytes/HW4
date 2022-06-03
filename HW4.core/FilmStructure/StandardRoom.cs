using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.core.FilmStructure
{
    public sealed class StandardRoom : Room
    {
        [JsonProperty]
        List<string> Drinks { get; set; }


        [JsonConstructor]
        public StandardRoom(List<string> drinks, string name, int numOfRows, int numOfSeats, int[,] prices, List<Session> sessions) : base(name, numOfRows, numOfSeats, prices, sessions)
        {
            Drinks = drinks;
        }


        public StandardRoom(string name, string rowsSeats, Film film) : base(name, rowsSeats, film)
        {
            Drinks = new List<string>();
        }


    }
}
