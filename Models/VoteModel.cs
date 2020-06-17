using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class VoteModel
    {
        public int Id { get; set; }
        public StrategyModel Strategy { get; set; }
        public UserModel User { get; set; }
        public int Vote { get; set; }
        public int Active { get; set; }
    }
}
