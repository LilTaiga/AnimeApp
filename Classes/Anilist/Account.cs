using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeApp.Classes.Anilist
{
    public class Account
    {
        public int Id { get; set; }
        public string AuthToken { get; set; }

        //The JsonSerializer requires a parameterless constructor to be able to desserialize an object.
        //Sadly, it must be public :(
        public Account()
        {
        }

        public Account(int _id, string _authToken = null)
        {
            Id = _id;

            AuthToken = _authToken;
        }

        public void AddToken(string _token)
        {
            AuthToken = _token;
        }

        public void DeleteToken()
        {
            AuthToken = null;
        }
    }
}
