using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Models
{
    public class CreateUsersServicesRequest
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
    }
}
