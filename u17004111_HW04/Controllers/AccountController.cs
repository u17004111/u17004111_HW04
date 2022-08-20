using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using u17004111_HW04.Models;
using System.Dynamic;

//This controller handles the creation of accounts, loging into an existing account, and viewing one's profile overview paeg.
namespace u17004111_HW04.Controllers
{
    public class AccountController : Controller
    {
        //Some variables used in the controller to make login tracking easier.
        List<User> userlist = new List<User>();
        User currentUser;
        
        //Write a little method that writes the userlist to a textfile that is stored on the server.
        public void storeUsers()
        {
            //var listFile = Path.GetFullPath("~/User_Data/userlist.ser");
            var listFile = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/userlist.ser");

            //If the file exists already, do not overwrite it with the initialising userlist in userbase.
            //Note useing appdata folder now to store userlist txtfile
            if(System.IO.File.Exists(listFile)) { }
            else
            {
                userlist = Models.Users.Userbase.userlist;
                FileStream output = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/userlist.ser"), FileMode.Create, FileAccess.Write);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(output, userlist);
                output.Close();
            }
        }
        
        public void loadUsers()
        {
            try
            {
                FileStream input = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/userlist.ser"), FileMode.Open, FileAccess.Read);
                BinaryFormatter bFormatter = new BinaryFormatter();
                userlist.Clear();
                userlist = (List<User>)bFormatter.Deserialize(input);
                input.Close();
            }
            catch(FileNotFoundException) { }
        }

        //This method for adding a newly created user to the userlist.
        public void addUser(User user)
        {
            userlist = Models.Users.Userbase.userlist;
            userlist.Add(user);
            FileStream output = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/userlist.ser"), FileMode.Create, FileAccess.Write);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(output, userlist);
            output.Close();
        }

        //This method for storing the current user. Will check if they're admin or not in the admin action of the admin controller.
        public void storeCurrent(User user)
        {
            //Do not check if a current user exists, as they must be overwritten anyway when someone else logs in
            //userlist = Models.Users.Userbase.userlist;
            
            FileStream output = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/currentuser.ser"), FileMode.Create, FileAccess.Write);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(output, currentUser);
            output.Close();
        }

        public User getCurrent()
        {
            var userFile = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/currentuser.ser");
            try
            {
                FileStream input = new FileStream(userFile, FileMode.Open, FileAccess.Read);
                BinaryFormatter bFormatter = new BinaryFormatter();
                currentUser = null;
                currentUser = (User)bFormatter.Deserialize(input);
                input.Close();
            }
            catch (FileNotFoundException) { }

            return currentUser;
        }

        [HttpGet]
        public ActionResult Login()
        {
            storeUsers();
            loadUsers();
            return View(userlist);
        }

        //Re-doing the username and password checking using serialized texfiles as persistent storage.
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            loadUsers();
            foreach(var user in userlist)
            {
                //var userholder = user;
                if(username == user.getUsername())
                {
                    if(password == user.getPassword())
                    {
                        currentUser = null;
                        currentUser = user;
                        currentUser.loggedIn = true;
                        TempData["currentUser"] = currentUser.getUsername();
                        TempData["userType"] = currentUser.GetType().ToString();
                        storeCurrent(currentUser);
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        ViewData["loginMessage"] = "Incorrect password.";
                        return View(userlist);
                    }
                }
                else
                {
                    ViewData["loginMessage"] = "Username does not exist.";
                }
            }

            return View(userlist);
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        //Upon account creation, add user to userlist text file.
        [HttpPost]
        public ActionResult CreateAccount(string username, string password, string email, string name, string surname, string selectedRadio, string institution, string qualifications)
        {
            
            if(selectedRadio == "stud")
            {
                Student newUser = new Student();
                newUser.Username = username;
                newUser.Password = password;
                newUser.Name = name;
                newUser.Surname = surname;
                newUser.Email = email;

                addUser(newUser);
            }
            else if(selectedRadio == "edu")
            {
                Educator newEdu = new Educator();
                newEdu.Username = username;
                newEdu.Password = password;
                newEdu.Name = name;
                newEdu.Surname = surname;
                newEdu.Email = email;
                newEdu.Institution = institution;
                
                addUser(newEdu);
            }
            else
            {
                Administrator newAdmin = new Administrator();
                newAdmin.Username = username;
                newAdmin.Password = password;
                newAdmin.Name = name;
                newAdmin.Surname = surname;
                newAdmin.Email = email;

                addUser(newAdmin);
            }

            return View();
        }
        
        public ActionResult Profile()
        {
            //This dynamically updates the view model based on what type is assigned. very nifty.
            dynamic mymodel = new ExpandoObject();
            var current = getCurrent();
            mymodel.currentUser = current;
            //mymodel.usertype = current.GetType().ToString();
            ViewData["userType"] = TempData["userType"];

            //return View(current as User);
            return View(mymodel);
        }
    }
}