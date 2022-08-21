using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace u17004111_HW04.Models
{
    [Serializable]
    //This is to be my abstract base class
    public abstract class User
    {
        public int userID { get; set; }
        private int userRating { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        private DateTime JoinDate { get; set; }
        internal string Username { get; set; }
        internal string Password { get; set; }

        //Testing usage of bool to track login status
        internal bool loggedIn { get; set; }

        //Constructor
        protected User(int userID, int userRating, string name, string surname, string email, DateTime joinDate, string username, string password, bool loggedIn)
        {
            this.userID = userID;
            this.userRating = userRating;
            Name = name;
            Surname = surname;
            Email = email;
            JoinDate = DateTime.Now;
            Username = username;
            Password = password;
            this.loggedIn = loggedIn;
        }

        protected User() { }

        //Abstract methods
        public abstract int worksContributed();

        //Virtual methods
        //User rating virtual method:
        //For contributors, ratings will be based off of how well other user's rate the contributor's contributions/how many
        //people make use of the contributions
        //For students, ratings will be based on how many contributions they visit/download, or how many quizes they do and how well they do in the quizes.
        public virtual int Rating => (userRating);
        //Normal methods
        public TimeSpan membershipLength()
        {
            DateTime current = new DateTime();
            current = DateTime.Now;

            System.TimeSpan diff = current.Subtract(JoinDate);

            return diff;
        }

        public string getUsername()
        {
            return Username;
        }
        public string getPassword()
        {
            return Password;
        }
    }
}