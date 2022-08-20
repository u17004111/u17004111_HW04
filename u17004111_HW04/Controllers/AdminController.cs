using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using u17004111_HW04.Models;


namespace u17004111_HW04.Controllers
{
    public class AdminController : Controller
    {
        User currentUser;
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
        // Make this page accesible only if an administrator has signed in. So, within this actionresult, have an if statement that checks a bool
        // to see whether or not an admin is signed in. 
        //Problem: How to get that bool from the account controller to the admin controller.
        public ActionResult Admin()
        {
            currentUser = getCurrent();
            //This exists to ensure that going to another screen and returning to the admin screen doesn't log you out.
            if (currentUser != null && currentUser.GetType().Name == "Administrator")
            {
                //User this currentuser variable
                return View();
            }
            else if(currentUser != null && currentUser.GetType().Name != "Administrator")
            {
                return RedirectToAction("Profile", "Account");
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult EditMaterials()
        {
            //Get lists of directories of all files uplaoded.
            var filePaths = Directory.GetFileSystemEntries(Server.MapPath("~/User_Data/Uploads/Files/"));
            var imgPaths = Directory.GetFileSystemEntries(Server.MapPath("~/User_Data/Uploads/Images/"));
            var vidPaths = Directory.GetFileSystemEntries(Server.MapPath("~/User_Data/Uploads/Videos/"));

            List<FileModel> files = new List<FileModel>();

            //Add directories to single list.
            foreach (var docPath in filePaths)
            {
                files.Add(new FileModel { FileName = Path.GetFileName(docPath) });
            }
            foreach (var docPath in imgPaths)
            {
                files.Add(new FileModel { FileName = Path.GetFileName(docPath) });
            }
            foreach (var docPath in vidPaths)
            {
                files.Add(new FileModel { FileName = Path.GetFileName(docPath) });
            }

            //Return list to view
            return View(files);
        }

        [HttpPost]
        public ActionResult EditMaterials(string filname, bool approved)
        {
            //Get lists of directories of all files uplaoded.
            var filePaths = Directory.GetFileSystemEntries(Server.MapPath("~/User_Data/Uploads/Files/"));
            var imgPaths = Directory.GetFileSystemEntries(Server.MapPath("~/User_Data/Uploads/Images/"));
            var vidPaths = Directory.GetFileSystemEntries(Server.MapPath("~/User_Data/Uploads/Videos/"));

            List<FileModel> files = new List<FileModel>();

            //Add directories to single list.
            foreach (var docPath in filePaths)
            {
                files.Add(new FileModel { FileName = Path.GetFileName(docPath) });
            }
            foreach (var docPath in imgPaths)
            {
                files.Add(new FileModel { FileName = Path.GetFileName(docPath) });
            }
            foreach (var docPath in vidPaths)
            {
                files.Add(new FileModel { FileName = Path.GetFileName(docPath) });
            }

            //Return list to view
            return View(files);
        }

        public ActionResult ReviewMembers()
        {
            //Would have to pass all the files to the view via the model here.

            return View();
        }
        public ActionResult ReviewUploads()
        {
            //Would have to pass all the files to the view via the model here.

            return View();
        }
    }
}