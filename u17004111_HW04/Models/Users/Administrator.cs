using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17004111_HW04.Models
{
    [Serializable]
    public class Administrator : User
    {
        public decimal Salary { get; set; }

        //Base constructor
        public Administrator() : base() { }

        //Parameterised constructor
        public Administrator(int userID, int userRating, string name, string surname, string email, DateTime joinDate, string username, string password, bool loggedIn, decimal salary) : base(userID, userRating, name, surname, email, joinDate, username, password, loggedIn)
        {
            Salary = salary;

        }

        public override int worksContributed()
        {
            throw new NotImplementedException();
        }
    }
}