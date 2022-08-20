using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17004111_HW04.Models
{
    [Serializable]
    public class Educator : User
    {
        public string Institution { get; set; }
        public int contributionCount { get; set; }

        public List<string> Contributions = new List<string>();
        public List<int> ratings = new List<int>();

        //Base constructor
        public Educator() : base() { }

        //Parameterised constructor
        public Educator(int userID, int userRating, string name, string surname, string email, DateTime joinDate, string username, string password, bool loggedIn, string institution, int contributioncount) : base(userID, userRating, name, surname, email, joinDate, username, password, loggedIn)
        {
            Institution = institution;
            contributionCount = contributioncount;
        }

        public override int getRating()
        {
            int avgRating = 0;

            foreach (int rating in ratings)
            {
                avgRating += rating;
            }

            avgRating = avgRating / ratings.Count();
            userRating = avgRating;

            return userRating;
        }

        public override int worksContributed()
        {
            int worksCount = Contributions.Count(); ;
            contributionCount = worksCount;
            return worksCount;
        }
    }
}