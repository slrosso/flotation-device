using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HCLib.FlotationDevice.Service.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;

namespace HCLib.FlotationDevice.Service
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );

      ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
      config.Count().Filter().OrderBy().Expand().Select().MaxTop(null); 
      builder.EntitySet<LocationCollection>("LocationCollections");
      builder.EntitySet<Collection>("Collections");
      builder.EntitySet<Location>("Locations");
      builder.EntitySet<Shelving>("Shelvings");
      builder.EntitySet<ShelfType>("ShelfTypes");
      config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    }
  }
}
