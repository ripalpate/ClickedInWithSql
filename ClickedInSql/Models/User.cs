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
        public bool IsPrisoner { get; set; }
        public string Interest { get; set; }
        public string Service { get; set; }
        

        public User(string name, DateTime releaseDate, int age, bool isPrisoner)
        {
            Name = name;
            ReleaseDate = releaseDate;
            Age = age;
            IsPrisoner = isPrisoner;
            ReleaseDate = releaseDate;
        }

        public User(string name, DateTime releaseDate, int age, bool isPrisoner, string interest)
        {
            Name = name;
            ReleaseDate = releaseDate;
            Age = age;
            IsPrisoner = isPrisoner;
            Interest = interest;
        }

        public User(string name, DateTime releaseDate, int age, bool isPrisoner, string interest, string service)
        {
            Name = name;
            ReleaseDate = releaseDate;
            Age = age;
            IsPrisoner = isPrisoner;
            Interest = interest;
            Service = service;
        }

        public User() {
            
        }
    }
}
