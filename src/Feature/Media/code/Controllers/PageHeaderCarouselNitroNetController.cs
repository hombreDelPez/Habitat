namespace Sitecore.Feature.Media.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Sitecore.Data.Fields;
    using Sitecore.Feature.Media.Models;
    using Sitecore.Feature.Media.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Foundation.Theming.Extensions;
    using Sitecore.Mvc.Presentation;
    using Sitecore.Resources.Media;

    public class PageHeaderCarouselNitroNetController : Controller
    {
        public ActionResult Index()
        {
            var model = new PageHeaderCarouselViewModel
            {
                Id = "carousel" + Guid.NewGuid().ToString("N"),
                DataIntervalAttribute = Sitecore.Context.PageMode.IsExperienceEditor ? "data-interval" : "",
                Carousels = this.GetCarousels(),
                PreviousLabel = "", //TODO
                NextLabel = "" //TODO
            };

            //TODO Return PageEditorError if invalid Datasource Template (see cshtml)

            return this.View("frontend/patterns/molecules/page-header/page-header-carousel", model);
        }

        private IList<CarouselViewModel> GetCarousels()
        {
            var renderingItem = RenderingContext.Current.Rendering.Item;

            var carouselItems = MediaSelectorElementsRepository.Get(renderingItem).ToArray();

            var carouselList = new List<CarouselViewModel>();
            for (int i = 0; i < carouselItems.Length; i++)
            {
                var currentElement = carouselItems[i];

                carouselList.Add(new CarouselViewModel
                {
                    Index = i,
                    Active = currentElement.Active,
                    Style = this.GetStyle(currentElement),
                    IsVideo = currentElement.Item.IsDerived(Templates.HasMediaVideo.ID),
                    VideoUrl = currentElement.Item.MediaUrl(Templates.HasMediaVideo.Fields.VideoLink),
                    Title = "", //TODO
                    Description = "" //TODO
                });
            }

            return carouselList;
        }

        private string GetStyle(MediaSelectorElement element)
        {
            var carouselHeight = this.GetHeight(element);

            var style = "background-image: ";
            var field = element.Item.IsDerived(Templates.HasMediaVideo.ID) ? Templates.HasMedia.Fields.Thumbnail : Templates.HasMediaImage.Fields.Image;
            style += element.Item.ImageUrl(field, new MediaUrlOptions()).ToCssUrlValue();
            var fixedHeight = RenderingContext.Current.Rendering.IsFixedHeight();
            if (fixedHeight && carouselHeight.HasValue)
            {
                style += "; height:" + carouselHeight + "px;";
            }

            return style;
        }

        private int? GetHeight(MediaSelectorElement element)
        {
            int? carouselHeight = null;

            int height;
            var field = element.Item.IsDerived(Templates.HasMediaVideo.ID) ? Templates.HasMedia.Fields.Thumbnail : Templates.HasMediaImage.Fields.Image;
            if (int.TryParse(((ImageField)element.Item.Fields[field]).Height, out height) && height > (carouselHeight ?? 0))
            {
                carouselHeight = height;
            }

            return carouselHeight;
        }
    }

    public class PageHeaderCarouselViewModel
    {
        public string Id { get; set; }
        public string DataIntervalAttribute { get; set; }
        public IList<CarouselViewModel> Carousels { get; set; }
        public string PreviousLabel { get; set; }
        public string NextLabel { get; set; }
    }

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