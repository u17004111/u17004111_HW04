using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17004111_HW04.Models
{
    public static class SDG_List
    {
        public static readonly List<SDG_card> cardList = new List<SDG_card>
        {
            new SDG_card() { Number = 1, Title = "No Poverty", Description = "End poverty in all its forms everywhere.", Img_title = "E-Web-Goal-01.png"},
            new SDG_card() { Number = 2, Title = "Zero Hunger", Description = "End hunger, achieve food security and improved nutrition and promote sustainable agriculture.", Img_title = "E-Web-Goal-02.png"},
            new SDG_card() { Number = 3, Title = "Good Health and Well-being", Description = "Ensure healthy lives and promote well-being for all at all ages.", Img_title = "E-Web-Goal-03.png"},
            new SDG_card() { Number = 4, Title = "Quality Education", Description = "Ensure inlusive and equitable quality education and promote lifelong learning opportunities for all.", Img_title = "E-Web-Goal-04.png"},
            new SDG_card() { Number = 5, Title = "Gender Equality", Description = "Achieve gender equality and empower all women and girls.", Img_title = "E-Web-Goal-05.png"},
            new SDG_card() { Number = 6, Title = "Clean Water and Sanitation", Description = "Ensure availability and sustainable management of water and sanitation for all.", Img_title = "E-Web-Goal-06.png" },
            new SDG_card() { Number = 7, Title = "Affordable and Clean Energy", Description = "Ensure access to affordable, reliable, sustainable and modern energy for all.", Img_title = "E-Web-Goal-07.png" },
            new SDG_card() { Number = 8, Title = "Decent Work and Economic Growth", Description = "Promote sustained, inclusive and sustainable economic growth, full and productive employment and decent work for all.", Img_title = "E-Web-Goal-08.png" },
            new SDG_card() { Number = 9, Title = "Industry, Innovation and Infrastructure", Description = "Build resilient infrastructure, promote inclusive and sustainable industrialization and foster innovation.", Img_title = "E-Web-Goal-09.png" },
            new SDG_card() { Number = 10, Title = "Reduced Inequalities", Description = "Reduce inequalities within and among countries.", Img_title = "E-Web-Goal-10.png" },
            new SDG_card() { Number = 11, Title = "Sustainable Cities and Communities", Description = "Make cities and human settlements inclusive, safe, resilient and sustainable.", Img_title = "E-Web-Goal-11.png" },
            new SDG_card() { Number = 12, Title = "Responsible Consumption and Production", Description = "Ensure sustainable consumption an production patterns.", Img_title = "E-Web-Goal-12.png" },
            new SDG_card() { Number = 13, Title = "Climate Action", Description = "Take urgent action to combat climate change and its impacts.", Img_title = "E-Web-Goal-13.png" },
            new SDG_card() { Number = 14, Title = "Life Below Water", Description = "conserve and sustainably use the oceans, sea and marine resources for sustainable development.", Img_title = "E-Web-Goal-14.png" },
            new SDG_card() { Number = 15, Title = "Life On Land", Description = "Protect, restore and promote sustainable use of terrestrial ecosystems, sustainably manage forests, combat desertification, and halt and reverse land degradation and halt biodiversity loss.", Img_title = "E-Web-Goal-15.png" },
            new SDG_card() { Number = 16, Title = "Peace, Justice and Strong Institutions", Description = "Promote peaceful and inclusive societies for sustainable development, provie access to justice for all and build effective, accountable and inclusive institutions at all levels.", Img_title = "E-Web-Goal-16.png" },
            new SDG_card() { Number = 17, Title = "Partnerships for the Goals", Description = "Strengthen the means of implementation and revitalize the global partnership for sustainable development.", Img_title = "E-Web-Goal-17.png" }
        };
    }
}