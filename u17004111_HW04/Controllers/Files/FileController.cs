using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u17004111_HW04.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// This controller is intended to handle any and everything to w=dow tih files of any sort.
// That includes but isn'tlimited to displaying images on views, the uploading, editing and deleting of uploaded files, etc etc.

namespace u17004111_HW04.Controllers
{
    public class FileController : Controller
    {
        User currentUser;
        List<FileModel> fileList = new List<FileModel>();

        public ActionResult Files()
        {
            var docPaths = Directory.GetFileSystemEntries(Server.MapPath("~/User_Data/Uploads/Files/"));

            List<FileModel> documents = new List<FileModel>();
            foreach (var docPath in docPaths)
            {
                documents.Add(new FileModel { FileName = Path.GetFileName(docPath) });
            }

            return View(documents);
        }

        public ActionResult Images()
        {
            var imgPaths = Directory.GetFileSystemEntries(Server.MapPath("~/User_Data/Uploads/Images/"));

            List<FileModel> images = new List<FileModel>();
            foreach (var imgPath in imgPaths)
            {
                images.Add(new FileModel { FileName = Path.GetFileName(imgPath) });
            }

            return View(images);
        }

        public ActionResult Videos()
        {
            var vidPaths = Directory.GetFileSystemEntries(Server.MapPath("~/User_Data/Uploads/Videos/"));

            List<FileModel> videos = new List<FileModel>();
            foreach (var vidPath in vidPaths)
            {
                videos.Add(new FileModel { FileName = Path.GetFileName(vidPath) });
            }

            return View(videos);
        }

        //This one is for the admin controller actually
        public ActionResult ViewFiles()
        {
            var files = getAllFiles();
            currentUser = getCurrent();
            fileList.Clear();
            var authorName = currentUser.Name + " " + currentUser.Surname;

            //Only display files uploaded by the current user, who miust be an educator.
            foreach(var file in files)
            {
                if(authorName == file.Author && currentUser.GetType().Equals(typeof(Educator)))
                {
                    fileList.Add(file);
                }
            }

            ViewData["currentUser"] = authorName;
            //Return list to view
            return View(files);
        }

        public FileResult Download(string fileName)
        {
            string filePath = "";
            //Check firstly in Files folder
            filePath = Server.MapPath(Url.Content("~/User_Data/Uploads/Files/" + fileName));
            if (System.IO.File.Exists(filePath)) 
            {
                return File(filePath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                //Check secondly in Images folder
                filePath = Server.MapPath(Url.Content("~/User_Data/Uploads/Images/" + fileName));
                if (System.IO.File.Exists(filePath)) { }
                else
                {
                    //Lastly resort to Videos folder
                    filePath = Server.MapPath(Url.Content("~/User_Data/Uploads/Videos/" + fileName));
                }
            }

            AccountController tempcontroller = new AccountController();
            tempcontroller.increaseRating(fileName);

            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        //Simple download fileresult for the uc diagrams. Downloads one pdf of all diagrams.
        public FileResult DownloadUC()
        {
            string filePath = filePath = Server.MapPath(Url.Content("~/Resources/Other_Images/Diagrams/UCN_Combined_u17004111.pdf"));
            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Octet, "UCN_Diagrams.pdf");
        }

        public ActionResult Delete(string fileName)
        {
            string filePath = "";
            //Check firstly in Files folder
            
            if (System.IO.File.Exists(filePath)) 
            {
                filePath = Server.MapPath(Url.Content("~/User_Data/Uploads/Files/" + fileName)); 
            }
            else
            {
                //Check secondly in Images folder
                filePath = Server.MapPath(Url.Content("~/User_Data/Uploads/Images/" + fileName));
                if (System.IO.File.Exists(filePath)) { }
                else
                {
                    //Lastly resort to Videos folder
                    filePath = Server.MapPath(Url.Content("~/User_Data/Uploads/Videos/" + fileName));
                }
            }

            try
            {
                System.IO.File.Delete(filePath);
                getAllFiles();
                saveRegistry();
                getRegistry();
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
            catch
            {
                ViewData["Message"] = "Delete unsuccessful";
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
        }

        [HttpGet]
        public ActionResult Upload()
        {
            saveRegistry();
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase File, string selectedRadio)
        {
            //Only educators can upload files anyways, so cast won't cause problems.
            Educator currentEdu = getCurrent() as Educator;
            var fileFolder = "";
            FileModel newfile = new FileModel();

            switch (selectedRadio)
            {
                case "doc":
                    fileFolder = Server.MapPath("~/User_Data/Uploads/Files/");
                    break;

                case "img":
                    fileFolder = Server.MapPath("~/User_Data/Uploads/Images/");
                    break;

                case "vid":
                    fileFolder = Server.MapPath("~/User_Data/Uploads/Videos/");
                    break;

                default:
                    fileFolder = "";
                    break;
            }

            if (File != null && File.ContentLength != 0)
            {
                var fileName = Path.GetFileName(File.FileName);
                var fullPath = Path.Combine(fileFolder, fileName);
                newfile.FileName = File.FileName;
                newfile.Author = getCurrent().Name + " " + getCurrent().Surname;
                newfile.Approved = false;

                while (System.IO.File.Exists(fullPath))
                {
                    var newName = Path.GetFileNameWithoutExtension(fileName) + "1";
                    fileName = Path.GetFileName(newName + Path.GetExtension(File.FileName));
                    fullPath = Path.Combine(fileFolder, fileName);
                    newfile.selectedFilePath = fullPath;
                }

                //Linking educator to file uplaoded.
                currentEdu.contributionCount += 1;
                currentEdu.Contributions.Add(newfile.FileName);
                //RedirectToAction("updateUser", "Account", new { User = currentEdu });
                AccountController tempcontroller = new AccountController();
                tempcontroller.updateUser(currentEdu);
                tempcontroller.increaseRating(fileName);

                //Saving file, finally.
                File.SaveAs(fullPath);
            }
            else
            {
                ViewData["Message"] = "File must not be empty.";
                return View();
            }

            if(Server.MapPath("~/App_Data/filelist.ser") == null)
            {
                saveRegistry();
            }
            updateRegistry(newfile);

            ViewData["Message"] = "File uploaded succesfully";
            return View();
        }

        //These methods deal with the file registry, which is a serialised file containing the info of all currently uploaded files.
        public void updateRegistry(FileModel file)
        {
            List<FileModel> files = getAllFiles();
            files.Add(file);

            FileStream output = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/filelist.ser"), FileMode.Create, FileAccess.Write);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(output, files);
            output.Close();
        }

        public void saveRegistry()
        {
                var files = getAllFiles();

                FileStream output = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/filelist.ser"), FileMode.Create, FileAccess.Write);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(output, files);
                output.Close();
        }

        public List<FileModel> getRegistry()
        {
            var fileReg = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/filelist.ser");
            try
            {
                FileStream input = new FileStream(fileReg, FileMode.Open, FileAccess.Read);
                BinaryFormatter bFormatter = new BinaryFormatter();
                fileList = null;
                fileList = (List<FileModel>)bFormatter.Deserialize(input);
                input.Close();
            }
            catch (FileNotFoundException) { }

            return fileList;
        }

        public List<FileModel> getAllFiles()
        {
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

            return files;
        }

        //This handles getting the current user to assign them as an author when they upload a file.
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
    }
}