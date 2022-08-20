using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17004111_HW04.Models.Users
{
    public class Userbase
    {
        public static List<User> userlist = new List<User>
        {
            new Student() {Name = "Mikail", Surname = "Kieser", Email = "u17004111@tuks.co.za", userID = 000004, Username = "Kieser", Password = "1", loggedIn = false},
            new Student() {Name = "Benjamin", Surname = "Button", Email = "mrbutton@gmail.com", userID = 000003, Username = "Benny", Password = "2", loggedIn = false},
            new Educator() {Name = "Archibald", Surname = "Weatherby", Email = "profweatherby45@protonmail.com", userID = 000002, Username = "Archy", Password = "3", loggedIn = false, Institution = "University of Pretoria"},
            new Administrator() {Name = "Marshal", Surname = "Johnson", Email = "neckbeardking@gmail.com", userID = 000001, Username = "Marsh", Password = "4", loggedIn = false},
            new Administrator() {Name = "Peter", Surname = "Fully", Email = "lolbru@gmail.com", userID = 000005, Username = "Piet", Password = "5", loggedIn = false},
            new Administrator() {Name = "Marshal", Surname = "Johnson", Email = "neckbeardking@gmail.com", userID = 000006, Username = "Bean", Password = "6", loggedIn = false},
            new Administrator() {Name = "Marshal", Surname = "Johnson", Email = "neckbeardking@gmail.com", userID = 000007, Username = "Doug", Password = "7", loggedIn = false}
        };
    }
}