using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SkillModel
    {
        public int Id { get; set; }
        public string NameFR { get; set; }
        public string NameEN { get; set; }
        public string DescriptionFR { get; set; }
        public string DescriptionEN { get; set; }
        public int Cost { get; set; }
        public string Location { get; set; }
        public ClasseModel Classe { get; set; }
        public string ImagePath { get; set; }
        public int Active { get; set; }
    }
}
