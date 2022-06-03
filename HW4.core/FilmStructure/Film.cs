using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.core
{
    public class Film
    {
        public static string[] AgeRates { get; } = { "0+", "6+", "12+", "16+", "18+" };

        [JsonProperty]
        public string FilmName { get; set; }

        [JsonProperty]
        public string AgeRate { get; set; }

        [JsonProperty]
        public TimeSpan Duration { get; set; }

        [JsonProperty]
        public List<Room> Rooms { get; private set; }

        //public List<VIPRoom> VIPRooms { get; set; }

        //public List<StandardRoom> StandardRooms { get; set; }

        [JsonIgnore]
        public List<Session> Sessions { get; set; }


        [JsonConstructor]
        public Film(string filmName, string ageRate, TimeSpan duration, List<Room> rooms)
        {
            FilmName = filmName;
            AgeRate = ageRate;
            Duration = duration;
            Rooms = rooms;
            Sessions = new List<Session>();
            //VIPRooms = vipRooms;
            //StandardRooms = standardRooms;
        }


        public Film(string filmName, string ageRate, TimeSpan duration)
        {
            FilmName = filmName;
            AgeRate = ageRate;
            Duration = duration;
            Rooms = new List<Room>();
            Sessions = new List<Session>();
            //VIPRooms = new List<VIPRoom>();
            //StandardRooms = new List<StandardRoom>();
        }


        public List<Session> GetRelevantSessions()
        {
            List<Session> relevantSessions = new List<Session>();
            DateTime now = DateTime.Now;

            foreach (Session session in Sessions)
            {
                if (session.FilmTime >= now)
                {
                    relevantSessions.Add(session);
                }
            }
            relevantSessions = relevantSessions.OrderBy(c => c.FilmTime).ToList();
            return relevantSessions;
        }
    }
}
