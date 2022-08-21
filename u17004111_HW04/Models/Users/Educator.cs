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

        //Base constructor
        public Educator() : base() { }

        //Parameterised constructor
        public Educator(int userID, int userRating, string name, string surname, string email, DateTime joinDate, string username, string password, bool loggedIn, string institution, int contributioncount) : base(userID, userRating, name, surname, email, joinDate, username, password, loggedIn)
        {
            Institution = institution;
            contributionCount = contributioncount;
        }

        //Educator rating based off of number of materials uploaded.
        public override int Rating
        {
            get
            {
                int rating = 0;

                foreach (string file in Contributions)
                {
                    rating++;
                }

                
                return rating;
            }
        }

        public override int worksContributed()
        {
            int worksCount = Contributions.Count(); ;
            contributionCount = worksCount;
            return worksCount;
        }
    }
}