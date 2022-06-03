using HW4.core.FilmStructure;
using HW4.core.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.core
{
    public class Storage
    {

        const string filePathFilms = "../../../HW4.core/Data/Films.json";

        const string filePathUsers = "../../../HW4.core/Data/Users.json";

        const string filePathAdmins = "../../../HW4.core/Data/Admins.json";


        public User CurrentUser { get; set; }

        public List<User> Users { get; set; }

        public List<Admin> Admins { get; set; }

        public List<Film> AllFilms { get; set; }

        public List<Room> AllRooms { get; set; }


        public Storage()
        {
            AllRooms = new List<Room>();
            AllFilms = LoadFilms();
            FormBackReferencesFilms();
            Users = LoadUsers();
            FormBackReferencesUsers();
            Admins = LoadAdmins();
            
        }



        static List<User> LoadUsers()
        {
            using (var sr = new StreamReader(filePathUsers))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    return serializer.Deserialize<List<User>>(jsonReader);
                }
            }
        }


        static List<Admin> LoadAdmins()
        {
            using (var sr = new StreamReader(filePathAdmins))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    return serializer.Deserialize<List<Admin>>(jsonReader);

                }
            }
        }


        static List<Film> LoadFilms()
        {
            using (var sr = new StreamReader(filePathFilms))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };
                    
                    return serializer.Deserialize<List<Film>>(jsonReader);
                }
            }
        }


        public void SaveData()
        {
            using (var sw = new StreamWriter(filePathFilms))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                    };

                    serializer.Serialize(jsonWriter, AllFilms);
                }
            }

            using (var sw = new StreamWriter(filePathUsers))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                    };

                    serializer.Serialize(jsonWriter, Users);
                }
            }

            using (var sw = new StreamWriter(filePathAdmins))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                    };

                    serializer.Serialize(jsonWriter, Admins);
                }
            }
        }

        private void FormBackReferencesFilms()
        {
            foreach (Film film in AllFilms)
            {
                foreach (Room room in film.Rooms)
                {
                    room.Film = film;
                    AllRooms.Add(room);

                    foreach (Session session in room.Sessions)
                    {
                        session.Room = room;
                        film.Sessions.Add(session);
                    }
                }
            }
        }


        private void FormBackReferencesUsers()
        {
            foreach (User user in Users)
            {
                for (int i = 0; i < user.BoughtSessions.Count; i++)
                {
                    foreach (Room room in AllRooms)
                    {
                        if (user.BoughtSessions[i].RoomName == room.Name)
                        {
                            foreach (Session session2 in room.Sessions)
                            {
                                if (session2.FilmTime == user.BoughtSessions[i].FilmTime)
                                {
                                    user.BoughtSessions[i] = session2;
                                }
                            }
                        }
                    }
                }
            }

            foreach (User user in Users)
            {
                for (int i = 0; i < user.Tickets.Count; i++)
                {
                    foreach (Room room in AllRooms)
                    {
                        if (user.Tickets[i].Session.RoomName == room.Name)
                        {
                            foreach (Session session in room.Sessions)
                            {
                                if (session.FilmTime == user.Tickets[i].Session.FilmTime)
                                {
                                    user.Tickets[i].Session = session;
                                }
                            }
                        }
                    }
                }
            }
        }


        public void SaveUsers()
        {
            using (var sw = new StreamWriter(filePathUsers))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                    };

                    serializer.Serialize(jsonWriter, Users);
                }
            }
        }


        public bool CheckLoginAvailability(string loginToCheck)
        {
            foreach (User user in Users)
            {
                if (user.Login == loginToCheck)
                {
                    return false;
                }
            }

            return true;
        }


        public User GetUserByLogin(string login)
        {
            foreach (User user in Users)
            {
                if (user.Login == login)
                {
                    return user;
                }

            }

            return null;
        }

        public Admin GetAdminByLogin(string login)
        {
            foreach (Admin admin in Admins)
            {
                if (admin.Login == login)
                {
                    return admin;
                }
            }

            return null;
        }


        public void DeleteAllRoomsOfFilm(string filmName)
        {
            bool isSmthToRemove = true;
            int j = 0;

            while (isSmthToRemove)
            {
                isSmthToRemove = false;

                for (int i = 0; i < AllRooms.Count; i++)
                {
                    if (AllRooms[i].Film.FilmName == filmName)
                    {
                        isSmthToRemove = true;
                        j = i;
                        break;
                    }
                }

                if (isSmthToRemove)
                {
                    AllRooms.RemoveAt(j);
                }
            }
        }


        public void RemoveFilm(Film film)
        {
            for (int i = 0; i < AllFilms.Count; i++)
            {
                if (AllFilms[i].Equals(film))
                {
                    AllFilms.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
