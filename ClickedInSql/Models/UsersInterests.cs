using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Models
{
    public class UsersInterests
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InterestId { get; set; }

        public UsersInterests(int userId, int interestId)
        {
            UserId = userId;
            InterestId = interestId;
        }
    }
}
