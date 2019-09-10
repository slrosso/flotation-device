using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using HCLib.ApiKey;
using HCLib.FlotationDevice.Service.Models;
using HCLib.FlotationDevice.Service.Models.DAL;
using Microsoft.AspNet.OData;

namespace HCLib.FlotationDevice.Service.Controllers
{
  [ApiKeyAuthorize(AllowApplicationType = ApplicationType.All)]
  public class LocationsController : ODataController
  {
    private IUnitOfWork data = new UnitOfWork();
    public LocationsController() { }
    public LocationsController(IUnitOfWork data)
    {
      this.data = data;
    }

    // GET: odata/Locations
    [EnableQuery]
    public IQueryable<Location> GetLocations()
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      if (Properties.Settings.Default.DebugMode)
      {
        NLogWriter.LogMessage(LogType.Debug, LogMsg + "Get All Locations");
      }
      return data.LocationRepository.GetAll();
    }

    // GET: odata/Locations(5)
    [EnableQuery]
    public SingleResult<Location> GetLocation([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      List<Location> locations = new List<Location>();
      try
      {
        locations.Add(data.LocationRepository.GetByID(key));
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception retrieving location by ID = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception retrieving location by ID:: " + ex.ToString()),
          ReasonPhrase = "Location not found"
        };
        throw new HttpResponseException(resp);
      }
      return SingleResult.Create(locations.AsQueryable());
    }

    // PUT: odata/Locations(5)
    public IHttpActionResult Put([FromODataUri] int key, Delta<Location> patch)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      Location location = null;

      try
      {
        Validate(patch.GetChangedPropertyNames());

        if (!ModelState.IsValid)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Invalid ModelState");
          throw new Exception("Invalid modelstate");
        }

        location = data.LocationRepository.GetByID(key);
        if (location == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Unable to find Location by Key = '" + Convert.ToString(key) + "'");
          throw new Exception("Unable to find location by key");
        }

        patch.Put(location);
        try
        {
          data.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          if (!LocationExists(key))
          {
            NLogWriter.LogMessage(LogType.Error, LogMsg + "DbUpdateConcurrencyException putting location by ID '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
            throw new Exception("DbUpdateConcurrencyException putting location by ID = '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
          }
          else
          {
            throw;
          }
        }
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception updating location :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception updating location :: " + ex.ToString()),
          ReasonPhrase = "Unable to udpate location"
        };
        throw new HttpResponseException(resp);
      }
      return Updated(location);
    }

    // POST: odata/Locations
    public IHttpActionResult Post(Location location)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";
      try
      {
        if (!ModelState.IsValid)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Invalid ModelState");
          throw new Exception("Invalid modelstate");
        }

        data.LocationRepository.Insert(location);
        data.Save();
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception creating location :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception creating location :: " + ex.ToString()),
          ReasonPhrase = "Unable to create location"
        };
        throw new HttpResponseException(resp);
      }

      return Created(location);
    }

    // PATCH: odata/Locations(5)
    [AcceptVerbs("PATCH", "MERGE")]
    public IHttpActionResult Patch([FromODataUri] int key, Delta<Location> patch)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      Location location = null;

      try
      {
        Validate(patch.GetChangedPropertyNames());

        if (!ModelState.IsValid)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Invalid ModelState");
          throw new Exception("Invalid modelstate");
        }

        location = data.LocationRepository.GetByID(key);
        if (location == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Unable to find Location by Key = '" + Convert.ToString(key) + "'");
          throw new Exception("Unable to find location by key");
        }

        patch.Put(location);
        try
        {
          data.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          if (!LocationExists(key))
          {
            NLogWriter.LogMessage(LogType.Error, LogMsg + "DbUpdateConcurrencyException putting location by ID '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
            throw new Exception("DbUpdateConcurrencyException putting location by ID = '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
          }
          else
          {
            throw;
          }
        }
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception updating location :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception updating location :: " + ex.ToString()),
          ReasonPhrase = "Unable to udpate location"
        };
        throw new HttpResponseException(resp);
      }
      return Updated(location);
    }

    // DELETE: odata/Locations(5)
    public IHttpActionResult Delete([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      Location location = null;
      try
      {
        location = data.LocationRepository.GetByID(key);
        if (location == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Location cannot be found");
          throw new Exception("Location cannot be found");
        }
        data.LocationRepository.Delete(key);
        data.Save();
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception deleting location by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception deleting location by key = '" + Convert.ToString(key) + "' :: " + ex.ToString()),
          ReasonPhrase = "Unable to delete location"
        };
        throw new HttpResponseException(resp);
      }
      return StatusCode(HttpStatusCode.NoContent);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        data.Dispose();
      }
      base.Dispose(disposing);
    }

    private bool LocationExists(int key)
    {
      string LogMsg = MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().ToString() + " :: ";
      try
      {
        return data.LocationRepository.Exists(key);
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception checking location existence by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        return false;
      }
    }
  }
}
