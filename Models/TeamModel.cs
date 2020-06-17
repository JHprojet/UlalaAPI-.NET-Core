using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public CharactersConfigurationModel CharactersConfiguration { get; set; }
        public ZoneModel Zone { get; set; }
        public string TeamName { get; set; }
        public int Active { get; set; }
    }
}
