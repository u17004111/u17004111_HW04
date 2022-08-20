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

        //View ActionResults
        //------------------------------------------
        //This ActionResult returns the filepaths of every file in the 
        //'Documents' folder to the view that displays documents, via the model of the view.
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

        //Regarding images (I.e. PDF, Word, and PPTx)
        //This ActionResult returns the filepaths of every image in the 
        //'Images' folder to the view that displays images, via the model of the view.
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

            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        //Simple download fileresult for the uc diagrams. Downloads one pdf of all diagrams.
        public FileResult DownloadUC()
        {
            string filePath = filePath = Server.MapPath(Url.Content("~/Resources/Other_Images/UC_Diagrams.pdf"));
            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Octet, "UC_Diagrams.pdf");
        }

        //Another fileresults for the uc narratives. Downloads one pdf of all narratives.
        public FileResult DownloadUCN()
        {
            string filePath = filePath = Server.MapPath(Url.Content("~/Resources/Uploads/Files/uc_narratives"));
            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Octet, "UC_Narratives.pdf");
        }

        public ActionResult Delete(string filePath)
        {
            try
            {
                System.IO.File.Delete(filePath);

                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
            catch
            {
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
            var fileFolder = "";
            FileModel newfile = new FileModel();
            newfile.FileName = File.FileName;
            newfile.Author = getCurrent().Name + " " + getCurrent().Surname;
            newfile.Approved = false;

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

                while (System.IO.File.Exists(fullPath))
                {
                    var newName = Path.GetFileNameWithoutExtension(fileName) + "1";
                    fileName = Path.GetFileName(newName + Path.GetExtension(File.FileName));
                    newfile.selectedFilePath = fullPath;
                }
                
                File.SaveAs(fullPath);
            }

            if(Server.MapPath("~/App_Data/filelist.ser") == null)
            {
                saveRegistry();
            }
            updateRegistry(newfile);

            return RedirectToAction("Index");
        }


        //These methods deal with the file registry, which is a serialised file containing the info of all currently uploaded files.
        public void updateRegistry(FileModel file)
        {
            var files = getAllFiles();
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