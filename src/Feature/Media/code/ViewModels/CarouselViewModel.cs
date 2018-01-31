using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Media.ViewModels
{
    public class CarouselViewModel
    {
        public int Index { get; set; }
        public string Active { get; set; }
        public string Style { get; set; }
        public bool IsVideo { get; set; }
        public string VideoUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}