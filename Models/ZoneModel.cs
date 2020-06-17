using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ZoneModel
    {
        public int Id { get; set; }
        public string ContinentFR { get; set; }
        public string ContinentEN { get; set; }
        public string ZoneFR { get; set; }
        public string ZoneEN { get; set; }
        public int ZoneQty { get; set; }
        public int Active { get; set; }
    }
}
