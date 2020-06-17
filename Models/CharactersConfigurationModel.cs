using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CharactersConfigurationModel
    {
        public int Id { get; set; }
        public ClasseModel Classe1 { get; set; }
        public ClasseModel Classe2 { get; set; }
        public ClasseModel Classe3 { get; set; }
        public ClasseModel Classe4 { get; set; }
        public int Active { get; set; }
    }
}
