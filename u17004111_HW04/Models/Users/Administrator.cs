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
        public int reviewCount { get; set; }

        public List<string> uploadsReviewed = new List<string>();
        public List<int> ratings = new List<int>();

        //Base constructor
        public Administrator() : base() { }

        //Parameterised constructor
        public Administrator(int userID, int userRating, string name, string surname, string email, DateTime joinDate, string username, string password, bool loggedIn, decimal salary, int reviewcount) : base(userID, userRating, name, surname, email, joinDate, username, password, loggedIn)
        {
            Salary = salary;
            reviewCount = reviewcount;
        }

        public override int getRating()
        {
            int avgRating = 0;

            foreach (var rating in ratings)
            {
                avgRating += rating;
            }

            avgRating = avgRating / ratings.Count();

            userRating = avgRating;

            return userRating;
        }

        public int getReviews()
        {
            int count = uploadsReviewed.Count();
            reviewCount = count;
            return count;
        }

        public override int worksContributed()
        {
            throw new NotImplementedException();
        }
    }
}