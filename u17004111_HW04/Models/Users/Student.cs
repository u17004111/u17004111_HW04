using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17004111_HW04.Models
{
    [Serializable]
    public class Student : User 
    {
        public string School { get; set; }

        public virtual IList<string> downloads { get; set; }

        //Base constructor
        public Student() : base() { }

        //Paramtererized constructor
        public Student(int userID, int userRating, string name, string surname, string email, DateTime joinDate, string username, string password, bool loggedIn, string school) : base(userID, userRating, name, surname, email, joinDate, username, password, loggedIn) 
        {
            School = school;
        }

        //Rating based off amoount fo downloads.
        public override int Rating
        {
            get
            {
                int rating = 0;
                foreach (string file in downloads)
                {
                    rating++;
                }
                return rating;
            }
        }

        public override int worksContributed()
        {
            throw new NotImplementedException();
        }
    }
}