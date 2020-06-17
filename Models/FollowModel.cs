using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FollowModel
    {
        public int Id { get; set; }
        public UserModel Followed { get; set; }
        public UserModel Follower { get; set; }
    }
}
