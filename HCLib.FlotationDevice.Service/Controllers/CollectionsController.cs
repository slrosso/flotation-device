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
  public class CollectionsController : ODataController
  {
    private IUnitOfWork data = new UnitOfWork();
    public CollectionsController() { }
    public CollectionsController(IUnitOfWork data)
    {
      this.data = data;
    }

    // GET: odata/Collections
    [EnableQuery]
    public IQueryable<Collection> GetCollections()
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      if (Properties.Settings.Default.DebugMode)
      {
        NLogWriter.LogMessage(LogType.Debug, LogMsg + "Get All Collections");
      }
      return data.CollectionRepository.GetAll();
    }

    // GET: odata/Collections(5)
    [EnableQuery]
    public SingleResult<Collection> GetCollection([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      List<Collection> collections = new List<Collection>();
      try
      {
        collections.Add(data.CollectionRepository.GetByID(key));
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception retrieving collection by ID = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception retrieving collection by ID:: " + ex.ToString()),
          ReasonPhrase = "Collection not found"
        };
        throw new HttpResponseException(resp);
      }
      return SingleResult.Create(collections.AsQueryable());
    }

    // PUT: odata/Collections(5)
    public IHttpActionResult Put([FromODataUri] int key, Delta<Collection> patch)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      Collection collection = null;

      try
      {
        Validate(patch.GetChangedPropertyNames());

        if (!ModelState.IsValid)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Invalid ModelState");
          throw new Exception("Invalid modelstate");
        }

        collection = data.CollectionRepository.GetByID(key);
        if (collection == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Unable to find Collection by Key = '" + Convert.ToString(key) + "'");
          throw new Exception("Unable to find collection by key");
        }

        patch.Put(collection);
        try
        {
          data.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          if (!CollectionExists(key))
          {
            NLogWriter.LogMessage(LogType.Error, LogMsg + "DbUpdateConcurrencyException putting collection by ID '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
            throw new Exception("DbUpdateConcurrencyException putting collection by ID = '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
          }
          else
          {
            throw;
          }
        }
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception updating collection :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception updating collection :: " + ex.ToString()),
          ReasonPhrase = "Unable to udpate collection"
        };
        throw new HttpResponseException(resp);
      }
      return Updated(collection);
    }

    // POST: odata/Collections
    public IHttpActionResult Post(Collection collection)
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

        data.CollectionRepository.Insert(collection);
        data.Save();
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception creating collection :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception creating collection :: " + ex.ToString()),
          ReasonPhrase = "Unable to create collection"
        };
        throw new HttpResponseException(resp);
      }

      return Created(collection);
    }

    // PATCH: odata/Collections(5)
    [AcceptVerbs("PATCH", "MERGE")]
    public IHttpActionResult Patch([FromODataUri] int key, Delta<Collection> patch)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      Collection collection = null;

      try
      {
        Validate(patch.GetChangedPropertyNames());

        if (!ModelState.IsValid)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Invalid ModelState");
          throw new Exception("Invalid modelstate");
        }

        collection = data.CollectionRepository.GetByID(key);
        if (collection == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Unable to find collection by Key = '" + Convert.ToString(key) + "'");
          throw new Exception("Unable to find collection by key");
        }

        patch.Put(collection);
        try
        {
          data.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          if (!CollectionExists(key))
          {
            NLogWriter.LogMessage(LogType.Error, LogMsg + "DbUpdateConcurrencyException putting collection by ID '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
            throw new Exception("DbUpdateConcurrencyException putting collection by ID = '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
          }
          else
          {
            throw;
          }
        }
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception updating collection :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception updating collection :: " + ex.ToString()),
          ReasonPhrase = "Unable to udpate collection"
        };
        throw new HttpResponseException(resp);
      }
      return Updated(collection);
    }

    // DELETE: odata/Collections(5)
    public IHttpActionResult Delete([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      Collection collection = null;
      try
      {
        collection = data.CollectionRepository.GetByID(key);
        if (collection == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Collection cannot be found");
          throw new Exception("Collection cannot be found");
        }
        data.CollectionRepository.Delete(key);
        data.Save();
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception deleting collection by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception deleting collection by key = '" + Convert.ToString(key) + "' :: " + ex.ToString()),
          ReasonPhrase = "Unable to delete collection"
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

    private bool CollectionExists(int key)
    {
      string LogMsg = MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().ToString() + " :: ";
      try
      {
        return data.CollectionRepository.Exists(key);
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception checking collection existence by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        return false;
      }
    }
  }
}
