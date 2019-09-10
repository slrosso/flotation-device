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
  public class LocationCollectionsController : ODataController
  {
    private IUnitOfWork data = new UnitOfWork();
    public LocationCollectionsController() { }
    public LocationCollectionsController(IUnitOfWork data)
    {
      this.data = data;
    }

    // GET: odata/LocationCollections
    [EnableQuery]
    public IQueryable<LocationCollection> GetLocationCollections()
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      if (Properties.Settings.Default.DebugMode)
      {
        NLogWriter.LogMessage(LogType.Debug, LogMsg + "Get All LocationCollections");
      }
      return data.LocationCollectionRepository.GetAll();
    }

    // GET: odata/LocationCollections(5)
    [EnableQuery]
    public SingleResult<LocationCollection> GetLocationCollection([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      List<LocationCollection> locationcollections = new List<LocationCollection>();
      try
      {
        locationcollections.Add(data.LocationCollectionRepository.GetByID(key));
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception retrieving locationcollection by ID = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception retrieving locationcollection by ID:: " + ex.ToString()),
          ReasonPhrase = "Locationcollection not found"
        };
        throw new HttpResponseException(resp);
      }
      return SingleResult.Create(locationcollections.AsQueryable());
    }

    // PUT: odata/LocationCollections(5)
    public IHttpActionResult Put([FromODataUri] int key, Delta<LocationCollection> patch)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      LocationCollection locationcollection = null;

      try
      {
        Validate(patch.GetChangedPropertyNames());

        if (!ModelState.IsValid)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Invalid ModelState");
          throw new Exception("Invalid modelstate");
        }

        locationcollection = data.LocationCollectionRepository.GetByID(key);
        if (locationcollection == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Unable to find Locationcollection by Key = '" + Convert.ToString(key) + "'");
          throw new Exception("Unable to find locationcollection by key");
        }

        patch.Put(locationcollection);
        try
        {
          data.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          if (!LocationCollectionExists(key))
          {
            NLogWriter.LogMessage(LogType.Error, LogMsg + "DbUpdateConcurrencyException putting locationcollection by ID '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
            throw new Exception("DbUpdateConcurrencyException putting locationcollection by ID = '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
          }
          else
          {
            throw;
          }
        }
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception updating locationcollection :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception updating locationcollection :: " + ex.ToString()),
          ReasonPhrase = "Unable to udpate locationcollection"
        };
        throw new HttpResponseException(resp);
      }
      return Updated(locationcollection);
    }

    // POST: odata/LocationCollections
    public IHttpActionResult Post(LocationCollection locationCollection)
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

        data.LocationCollectionRepository.Insert(locationCollection);
        data.Save();
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception creating locationcollection :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception creating locationcollection :: " + ex.ToString()),
          ReasonPhrase = "Unable to create locationcollection"
        };
        throw new HttpResponseException(resp);
      }

      return Created(locationCollection);
    }

    // PATCH: odata/LocationCollections(5)
    [AcceptVerbs("PATCH", "MERGE")]
    public IHttpActionResult Patch([FromODataUri] int key, Delta<LocationCollection> patch)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      LocationCollection locationcollection = null;

      try
      {
        Validate(patch.GetChangedPropertyNames());

        if (!ModelState.IsValid)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Invalid ModelState");
          throw new Exception("Invalid modelstate");
        }

        locationcollection = data.LocationCollectionRepository.GetByID(key);
        if (locationcollection == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Unable to find Locationcollection by Key = '" + Convert.ToString(key) + "'");
          throw new Exception("Unable to find locationcollection by key");
        }

        patch.Put(locationcollection);
        try
        {
          data.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          if (!LocationCollectionExists(key))
          {
            NLogWriter.LogMessage(LogType.Error, LogMsg + "DbUpdateConcurrencyException putting locationcollection by ID '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
            throw new Exception("DbUpdateConcurrencyException putting locationcollection by ID = '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
          }
          else
          {
            throw;
          }
        }
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception updating locationcollection :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception updating locationcollection :: " + ex.ToString()),
          ReasonPhrase = "Unable to udpate locationcollection"
        };
        throw new HttpResponseException(resp);
      }
      return Updated(locationcollection);
    }

    // DELETE: odata/LocationCollections(5)
    public IHttpActionResult Delete([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      LocationCollection locationcollection = null;
      try
      {
        locationcollection = data.LocationCollectionRepository.GetByID(key);
        if (locationcollection == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Locationcollection cannot be found");
          throw new Exception("Locationcollection cannot be found");
        }
        data.LocationCollectionRepository.Delete(key);
        data.Save();
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception deleting locationcollection by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception deleting locationcollection by key = '" + Convert.ToString(key) + "' :: " + ex.ToString()),
          ReasonPhrase = "Unable to delete locationcollection"
        };
        throw new HttpResponseException(resp);
      }
      return StatusCode(HttpStatusCode.NoContent);
    }

    // GET: odata/LocationCollections(5)/Collection
    [EnableQuery]
    public SingleResult<Collection> GetCollection([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      List<Collection> collections = new List<Collection>();
      try
      {
        collections.Add(data.LocationCollectionRepository.GetByID(key).Collection);
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception finding LocationCollection's collection by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception finding locationcollection's collection :: " + ex.ToString()),
          ReasonPhrase = "Unable to find collection"
        };
        throw new HttpResponseException(resp);
      }

      return SingleResult.Create(collections.AsQueryable());
    }

    // GET: odata/LocationCollections(5)/Location
    [EnableQuery]
    public SingleResult<Location> GetLocation([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      List<Location> locations = new List<Location>();
      try
      {
        locations.Add(data.LocationCollectionRepository.GetByID(key).Location);
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception finding LocationCollection's location by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception finding locationcollection's location :: " + ex.ToString()),
          ReasonPhrase = "Unable to find location"
        };
        throw new HttpResponseException(resp);
      }

      return SingleResult.Create(locations.AsQueryable());
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        data.Dispose();
      }
      base.Dispose(disposing);
    }

    private bool LocationCollectionExists(int key)
    {
      string LogMsg = MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().ToString() + " :: ";
      try
      {
        return data.LocationCollectionRepository.Exists(key);
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception checking locationcollection existence by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        return false;
      }
    }
  }
}
