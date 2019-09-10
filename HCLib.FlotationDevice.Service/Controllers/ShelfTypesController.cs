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
  public class ShelfTypesController : ODataController
  {
    private IUnitOfWork data = new UnitOfWork();
    public ShelfTypesController() { }
    public ShelfTypesController(IUnitOfWork data)
    {
      this.data = data;
    }

    // GET: odata/ShelfTypes
    [EnableQuery]
    public IQueryable<ShelfType> GetShelfTypes()
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      if (Properties.Settings.Default.DebugMode)
      {
        NLogWriter.LogMessage(LogType.Debug, LogMsg + "Get All ShelfTypes");
      }
      return data.ShelfTypeRepository.GetAll();
    }

    // GET: odata/ShelfTypes(5)
    [EnableQuery]
    public SingleResult<ShelfType> GetShelfType([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      List<ShelfType> shelfTypes = new List<ShelfType>();
      try
      {
        shelfTypes.Add(data.ShelfTypeRepository.GetByID(key));
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception retrieving shelftype by ID = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception retrieving shelftype by ID:: " + ex.ToString()),
          ReasonPhrase = "ShelfType not found"
        };
        throw new HttpResponseException(resp);
      }
      return SingleResult.Create(shelfTypes.AsQueryable());
    }

    // PUT: odata/ShelfTypes(5)
    public IHttpActionResult Put([FromODataUri] int key, Delta<ShelfType> patch)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      ShelfType shelftype = null;

      try
      {
        Validate(patch.GetChangedPropertyNames());

        if (!ModelState.IsValid)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Invalid ModelState");
          throw new Exception("Invalid modelstate");
        }

        shelftype = data.ShelfTypeRepository.GetByID(key);
        if (shelftype == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Unable to find ShelfType by Key = '" + Convert.ToString(key) + "'");
          throw new Exception("Unable to find shelftype by key");
        }

        patch.Put(shelftype);
        try
        {
          data.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          if (!ShelfTypeExists(key))
          {
            NLogWriter.LogMessage(LogType.Error, LogMsg + "DbUpdateConcurrencyException putting shelftype by ID '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
            throw new Exception("DbUpdateConcurrencyException putting shelftype by ID = '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
          }
          else
          {
            throw;
          }
        }
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception updating shelftype :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception updating shelftype :: " + ex.ToString()),
          ReasonPhrase = "Unable to udpate shelftype"
        };
        throw new HttpResponseException(resp);
      }
      return Updated(shelftype);
    }

    // POST: odata/ShelfTypes
    public IHttpActionResult Post(ShelfType shelfType)
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

        data.ShelfTypeRepository.Insert(shelfType);
        data.Save();
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception creating shelftype :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception creating shelftype :: " + ex.ToString()),
          ReasonPhrase = "Unable to create shelftype"
        };
        throw new HttpResponseException(resp);
      }

      return Created(shelfType);
    }

    // PATCH: odata/ShelfTypes(5)
    [AcceptVerbs("PATCH", "MERGE")]
    public IHttpActionResult Patch([FromODataUri] int key, Delta<ShelfType> patch)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      ShelfType shelftype = null;

      try
      {
        Validate(patch.GetChangedPropertyNames());

        if (!ModelState.IsValid)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Invalid ModelState");
          throw new Exception("Invalid modelstate");
        }

        shelftype = data.ShelfTypeRepository.GetByID(key);
        if (shelftype == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Unable to find ShelfType by Key = '" + Convert.ToString(key) + "'");
          throw new Exception("Unable to find shelftype by key");
        }

        patch.Put(shelftype);
        try
        {
          data.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          if (!ShelfTypeExists(key))
          {
            NLogWriter.LogMessage(LogType.Error, LogMsg + "DbUpdateConcurrencyException putting shelftype by ID '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
            throw new Exception("DbUpdateConcurrencyException putting shelftype by ID = '" + Convert.ToString(key) + "' - Not Found :: " + ex.ToString());
          }
          else
          {
            throw;
          }
        }
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception updating shelftype :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception updating shelftype :: " + ex.ToString()),
          ReasonPhrase = "Unable to udpate shelftype"
        };
        throw new HttpResponseException(resp);
      }
      return Updated(shelftype);
    }

    // DELETE: odata/ShelfTypes(5)
    public IHttpActionResult Delete([FromODataUri] int key)
    {
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      ShelfType shelftype = null;
      try
      {
        shelftype = data.ShelfTypeRepository.GetByID(key);
        if (shelftype == null)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "ShelfType cannot be found");
          throw new Exception("ShelfType cannot be found");
        }
        data.ShelfTypeRepository.Delete(key);
        data.Save();
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception deleting shelftype by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("Exception deleting shelftype by key = '" + Convert.ToString(key) + "' :: " + ex.ToString()),
          ReasonPhrase = "Unable to delete shelftype"
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

    private bool ShelfTypeExists(int key)
    {
      string LogMsg = MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().ToString() + " :: ";
      try
      {
        return data.ShelfTypeRepository.Exists(key);
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception checking shelftype existence by key = '" + Convert.ToString(key) + "' :: " + ex.ToString());
        return false;
      }
    }
  }
}
