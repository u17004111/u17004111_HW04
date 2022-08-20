using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17004111_HW04.Models
{
    [Serializable]
    public class FileModel
    {
        public string FileName { get; set; }
        public string selectedFilePath { get; set; }
        public bool Approved { get; set; }
        public string Author { get; set; }

        public List<FileModel> fileList = new List<FileModel>();

        public override bool Equals(object obj)
        {
            return obj is FileModel model &&
                   FileName == model.FileName &&
                   selectedFilePath == model.selectedFilePath &&
                   Approved == model.Approved &&
                   Author == model.Author;
        }
    }
}