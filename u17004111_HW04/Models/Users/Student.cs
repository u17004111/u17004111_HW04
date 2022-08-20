using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17004111_HW04.Models
{
    [Serializable]
    public class Student : User 
    {
        public string Book { get; set; }
        public string School { get; set; }
        public int Grade { get; set; }

        public List<int> ratings = new List<int>();
        public List<string> materials = new List<string>();
        public List<string> interestedTopics = new List<string>();

        //Base constructor
        public Student() : base() { }

        //Paramtererized constructor
        public Student(int userID, int userRating, string name, string surname, string email, DateTime joinDate, string username, string password, bool loggedIn, string book, string school, int grade) : base(userID, userRating, name, surname, email, joinDate, username, password, loggedIn) 
        {
            Book = book;
            School = school;
            Grade = grade;
        }

        public override int getRating()
        {
            int avgRating = 0;

            foreach(var rating in ratings)
            {
                avgRating += rating;
            }

            avgRating = avgRating / ratings.Count();

            userRating = avgRating;

            return userRating;
        }

        public override int worksContributed()
        {
            throw new NotImplementedException();
        }
    }
}