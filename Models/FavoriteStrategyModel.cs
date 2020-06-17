using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FavoriteStrategyModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public StrategyModel Strategy { get; set; }
        public int Active { get; set; }
    }
}
