using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Models
{
    public class UsersServices
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }

        public UsersServices(int userId, int serviceId)
        {
            UserId = userId;
            ServiceId = serviceId;
        }
    }
}
