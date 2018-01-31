using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Media.ViewModels
{
    public class PageHeaderCarouselViewModel
    {
        public string Id { get; set; }
        public string DataIntervalAttribute { get; set; }
        public IList<CarouselViewModel> Carousels { get; set; }
        public string PreviousLabel { get; set; }
        public string NextLabel { get; set; }
    }
}