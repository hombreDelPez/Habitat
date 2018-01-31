namespace Sitecore.Feature.Media.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Feature.Media.Models;
    using Sitecore.Feature.Media.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Foundation.Theming.Extensions;
    using Sitecore.Mvc.Presentation;
    using Sitecore.Resources.Media;
    using Sitecore.Feature.Media.ViewModels;
    using Sitecore.Foundation.Alerts;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Web.UI.XslControls;

    public class PageHeaderCarouselNitroNetController : Controller
    {
        public ActionResult Index()
        {
            //TODO Return PageEditorError if invalid Datasource Template (see cshtml) 

            var model = new PageHeaderCarouselViewModel
            {
                Id = "carousel" + Guid.NewGuid().ToString("N"),
                DataIntervalAttribute = Sitecore.Context.PageMode.IsExperienceEditor ? "data-interval" : "",
                Carousels = this.GetCarousels(),
                PreviousLabel = DictionaryPhraseRepository.Current.Get("/media/carousel/previous", "Previous"),
                NextLabel = DictionaryPhraseRepository.Current.Get("/media/carousel/next", "Next")
            };

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
                    Title = Sitecore.Web.UI.WebControls.FieldRenderer.Render(currentElement.Item, Templates.HasMedia.Fields.Title.ToString()), 
                    Description = Sitecore.Web.UI.WebControls.FieldRenderer.Render(currentElement.Item, Templates.HasMedia.Fields.Description.ToString()) 
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
}