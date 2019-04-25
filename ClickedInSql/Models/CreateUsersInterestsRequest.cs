using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Models
{
    public class CreateUsersInterestsRequest
    {
        public int UserId { get; set; }
        public int InterestId { get; set; }

    }
}
