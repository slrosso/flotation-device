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
  public class ShelvingsController : ODataController
  {
    private IUnitOfWork data = new UnitOfWork();
    public ShelvingsController() { }
    public ShelvingsController(IUnitOfWork data)
    {
      this.data = data;
    }

    // GET: odata/Shelvings
    [EnableQuery]
    public IQueryable<Shelving> GetShelvings()
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      if (Properties.Settings.Default.DebugMode)
      {
        NLogWriter.LogMessage(LogType.Debug, LogMsg + "Get All Locations");
      }
      return data.ShelvingRepository.GetAll();
    }

    // GET: odata/Shelvings(5)
    [EnableQuery]
    public SingleResult<Shelving> GetShelving([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      List<Shelving> shelving = new List<Shelving>();
      try
      {
        shelving.Add(data.ShelvingRepository.GetByID(key));
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception retrieving shelving by ID = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception retrieving shelving by ID:: " + ex.ToString()),
          ReasonPhrase = "Shelving not found"
        };
        throw new HttpResponseException(resp);
      }
      return SingleResult.Create(shelving.AsQueryable());
    }

    // PUT: odata/Shelvings(5)
    public IHttpActionResult Put([FromODataUri] int key, Delta<Shelving> patch)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      Shelving shelving = null;

      try
      {
        Validate(patch.GetChangedPropertyNames());

        if (!ModelState.IsValid)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Invalid ModelState");
          throw new Exception("Invalid modelstate");
        }

        shelving = data.ShelvingRepository.GetByID(key);
        if (shelving == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Unable to find shelving by Key = '" + Convert.ToString(key) + "'");
          throw new Exception("Unable to find shelving by key");
        }

        patch.Put(shelving);
        try
        {
          data.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          if (!ShelvingExists(key))
          {
            NLogWriter.LogMessage(LogType.Error, LogMsg + "DbUpdateConcurrencyException putting shelving by ID '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
            throw new Exception("DbUpdateConcurrencyException putting shelving by ID = '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
          }
          else
          {
            throw;
          }
        }
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception updating shelving :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception updating shelving :: " + ex.ToString()),
          ReasonPhrase = "Unable to udpate shelving"
        };
        throw new HttpResponseException(resp);
      }
      return Updated(shelving);
    }

    // POST: odata/Shelvings
    public IHttpActionResult Post(Shelving shelving)
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

        data.ShelvingRepository.Insert(shelving);
        data.Save();
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception creating shelving :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception creating shelving :: " + ex.ToString()),
          ReasonPhrase = "Unable to create shelving"
        };
        throw new HttpResponseException(resp);
      }

      return Created(shelving);
    }

    // PATCH: odata/Shelvings(5)
    [AcceptVerbs("PATCH", "MERGE")]
    public IHttpActionResult Patch([FromODataUri] int key, Delta<Shelving> patch)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      Shelving shelving = null;

      try
      {
        Validate(patch.GetChangedPropertyNames());

        if (!ModelState.IsValid)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Invalid ModelState");
          throw new Exception("Invalid modelstate");
        }

        shelving = data.ShelvingRepository.GetByID(key);
        if (shelving == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Unable to find shelving by Key = '" + Convert.ToString(key) + "'");
          throw new Exception("Unable to find shelving by key");
        }

        patch.Put(shelving);
        try
        {
          data.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          if (!ShelvingExists(key))
          {
            NLogWriter.LogMessage(LogType.Error, LogMsg + "DbUpdateConcurrencyException putting shelving by ID '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
            throw new Exception("DbUpdateConcurrencyException putting shelving by ID = '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
          }
          else
          {
            throw;
          }
        }
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception updating shelving :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception updating shelving :: " + ex.ToString()),
          ReasonPhrase = "Unable to udpate shelving"
        };
        throw new HttpResponseException(resp);
      }
      return Updated(shelving);
    }

    // DELETE: odata/Shelvings(5)
    public IHttpActionResult Delete([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      Shelving shelving = null;
      try
      {
        shelving = data.ShelvingRepository.GetByID(key);
        if (shelving == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Shelving cannot be found");
          throw new Exception("Shelving cannot be found");
        }
        data.ShelvingRepository.Delete(key);
        data.Save();
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception deleting shelving by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception deleting shelving by key = '" + Convert.ToString(key) + "' :: " + ex.ToString()),
          ReasonPhrase = "Unable to delete shelving"
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

    private bool ShelvingExists(int key)
    {
      string LogMsg = MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().ToString() + " :: ";
      try
      {
        return data.ShelvingRepository.Exists(key);
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception checking shelving existence by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        return false;
      }
    }
  }
}
