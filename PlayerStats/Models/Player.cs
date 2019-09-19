using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayerStats.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public string Sex { get; set; }
        public CountryModel Country { get; set; }
        public string Picture { get; set; }
        public DataModel Data { get; set; }

    }
    public class CountryModel
    {
        public string Picture { get; set; }
        public string Code { get; set; }
        

    }
    public class DataModel
    {
        public int Rank { get; set; }
        public int Points { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public List<int> Last { get; set; }
     }

    public class jsonPlayers
    {
        public List<Player> players { get; set; }
    }

}