using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17004111_HW04.Models
{
    public class Material
    {
        public int uniqueID { get; set; }
        public string materialType { get; set; }
        public DateTime dateAdded { get; set; }
        public decimal Size { get; set; }
        public bool Approved { get; set; }
    }
}