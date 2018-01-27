using SitecoreNitroNetViewEngine = NitroNet.Sitecore.SitecoreNitroNetViewEngine;

namespace Sitecore.Foundation.NitroNet.Processors
{
    using System.Web.Mvc;
    using Sitecore.Pipelines;

    public class RegisterNitroNetViewEngine
    {
        public virtual void Process(PipelineArgs args)
        {
            ViewEngines.Engines.Add(DependencyResolver.Current.GetService<SitecoreNitroNetViewEngine>());
        }
    }
}