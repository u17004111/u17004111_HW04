using System;
using System.Collections.Generic;
using u17004111_HW04.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace u17004111_HW04.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //This is the actionresult for the the page that will display the SDG cards.
        //Passing the list of SDG's called cardList from the SDG_List to the model for the view.
        public ActionResult About()
        {
            var infoPaths = Directory.GetFileSystemEntries(Server.MapPath("~/Resources/SDG/Infographics/"));
            var NarrPaths = Directory.GetFileSystemEntries(Server.MapPath("~/Resources/Other_Images/Narratives"));

            List<FileModel> thumbnails = new List<FileModel>();
            foreach (var imgPath in infoPaths)
            {
                thumbnails.Add(new FileModel { FileName = Path.GetFileName(imgPath) });
            }

            List<FileModel> UC_Narratives = new List<FileModel>();
            foreach (var imgPath in NarrPaths)
            {
                UC_Narratives.Add(new FileModel { FileName = Path.GetFileName(imgPath) });
            }
            
            //These images are unfortunately wuite a lot of data to have to pass to the client.
            //Perhaps consider a better way to pass the infographics to the view as they are needed, instead of all at once.
            ViewData["Thumbnails"] = thumbnails;
            ViewData["Narratives"] = UC_Narratives;

            return View(SDG_List.cardList);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}