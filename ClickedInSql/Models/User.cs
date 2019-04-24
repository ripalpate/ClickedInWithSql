using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate{ get; set; }
        public int Age { get; set; }
        public bool IsPrisioner { get; set; }

        public User(string name, DateTime releaseDate, int age, bool isPrisioner)
        {
            Name = name;
            ReleaseDate = releaseDate;
            Age = age;
            IsPrisioner = isPrisioner;
            ReleaseDate = releaseDate;
        }
    }
}
