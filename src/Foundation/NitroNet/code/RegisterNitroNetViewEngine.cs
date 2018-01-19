using System.Web.Mvc;
using Sitecore.Pipelines;
using SitecoreNitroNetViewEngine = NitroNet.Sitecore.SitecoreNitroNetViewEngine;

namespace Sitecore.Foundation.NitroNet
{
    public class RegisterNitroNetViewEngine
    {
        public virtual void Process(PipelineArgs args)
        {
            ViewEngines.Engines.Add(DependencyResolver.Current.GetService<SitecoreNitroNetViewEngine>());
        }
    }
}