# NitroNet.Sitecore Demo Integration into Habitat

## Description
This project is based on the [Sitecore Habitat solution](https://github.com/Sitecore/Habitat) and should help and show new NitroNet.Sitecore developers how they have to integrate NitroNet.Sitecore into their Sitecore solutions.

## How this Demo was created
**(1)** The Sitecore Habitat solution has been forked a new branch on basis of *master* has been created

**(2)** A new project `Sitecore.Foundation.NitroNet` has been created and the NuGet `NitroNet.Sitecore.Microsoft.DependencyInjection.Sitecore82` has been installed into this project.

See also: https://github.com/namics/NitroNetSitecore/blob/master/docs/installation.md

**(3)** Registered the NitroNet View Engine via a Sitecore config file (this step is obsolete in NitroNet.Sitecore >= v.2.0.0)

**(4)** The component **Page Header Carousel** has been rewritten to use handlebars and NitroNet instead of Razor (.cshtml). Therefore a new handlebars view `page-header-carousel.hbs` has been created in */frontend/patterns/molecules*. And in addition a new Controller `PageHeaderCarouselNitroNetController` has been created to build the needed ViewModels.