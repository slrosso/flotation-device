using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HCLib.FlotationDevice.Service.Models;
using HCLib.FlotationDevice.Staff.Models;
using HCLib.FlotationDevice.Staff.ViewModels;

namespace HCLib.FlotationDevice.Staff.Controllers
{
  public class ShelvingsController : Controller
  {
    private Default.Container db = FlotationDeviceServiceContext.Context();

    // GET: Shelvings/Index/5
    public ActionResult Index(int? id)
    {
      //var shelvings = db.Shelvings.Include(s => s.LocationCollection).Include(s => s.ShelfType);
      //return View(shelvings.ToList());
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      ShelvingIndexViewModel sivm = new ShelvingIndexViewModel();

      try
      {
        sivm.slist = db.Shelvings.Where(l => l.LocationID == id).Include(r => r.ShelfType).Include(r => r.LocationCollection).Include(r => r.LocationID).ToList();
        try
        {
          sivm.stypes = db.ShelfTypes.ToList();
          try
          {
            sivm.location = db.Locations.Where(l => l.LocationID == id).FirstOrDefault().LocationName; 
            try
            {
              sivm.loccol = db.LocationCollections.Where(l => l.LocationID == id).ToList();
              try
              {
                sivm.col = db.Collections.ToList();

                //foreach (Shelving s in sivm.slist)
                //{
                //  var lcid = s.LocationCollectionID;
                //  var colid = sivm.loccol.
                //  s.LocationCollection = db.Collections.Where(l => l.CollectionID == s.LocationCollectionID).FirstOrDefault().Collection.CollectionName;
                //}
              }
              catch (Exception ex)
              {
                NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception getting collection :: " + ex.ToString());
                ModelState.AddModelError("", "Unhandled Exception occurred");
                ModelState.AddModelError("", ex.ToString());
              }
            }
            catch (Exception ex)
            {
              NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception getting location collection :: " + ex.ToString());
              ModelState.AddModelError("", "Unhandled Exception occurred");
              ModelState.AddModelError("", ex.ToString());
            }
          }
          catch (Exception ex)
          {
            NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception getting location :: " + ex.ToString());
            ModelState.AddModelError("", "Unhandled Exception occurred");
            ModelState.AddModelError("", ex.ToString());
          }
        }
        catch (Exception ex)
        {
          NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception getting shelf types :: " + ex.ToString());
          ModelState.AddModelError("", "Unhandled Exception occurred");
          ModelState.AddModelError("", ex.ToString());
        }
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception getting floating collections :: " + ex.ToString());
        ModelState.AddModelError("", "Unhandled Exception occurred");
        ModelState.AddModelError("", ex.ToString());
      }
      return View(sivm);
    }

    //// GET: Shelvings/Details/5
    //public ActionResult Details(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  Shelving shelving = db.Shelvings.Find(id);
    //  if (shelving == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(shelving);
    //}

    //// GET: Shelvings/Create
    //public ActionResult Create()
    //{
    //  ViewBag.LocationCollectionID = new SelectList(db.LocationCollections, "LocationCollectionID", "LocationCollectionID");
    //  ViewBag.ShelfTypeID = new SelectList(db.ShelfTypes, "ShelfTypeID", "ShelfType1");
    //  return View();
    //}

    //// POST: Shelvings/Create
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Create([Bind(Include = "ShelvingID,LocationCollectionID,ShelfTypeID,ShelfQty,Length,RowQty,Note,Updated")] Shelving shelving)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.Shelvings.Add(shelving);
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }

    //  ViewBag.LocationCollectionID = new SelectList(db.LocationCollections, "LocationCollectionID", "LocationCollectionID", shelving.LocationCollectionID);
    //  ViewBag.ShelfTypeID = new SelectList(db.ShelfTypes, "ShelfTypeID", "ShelfType1", shelving.ShelfTypeID);
    //  return View(shelving);
    //}

    //// GET: Shelvings/Edit/5
    //public ActionResult Edit(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  Shelving shelving = db.Shelvings.Find(id);
    //  if (shelving == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  ViewBag.LocationCollectionID = new SelectList(db.LocationCollections, "LocationCollectionID", "LocationCollectionID", shelving.LocationCollectionID);
    //  ViewBag.ShelfTypeID = new SelectList(db.ShelfTypes, "ShelfTypeID", "ShelfType1", shelving.ShelfTypeID);
    //  return View(shelving);
    //}

    //// POST: Shelvings/Edit/5
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit([Bind(Include = "ShelvingID,LocationCollectionID,ShelfTypeID,ShelfQty,Length,RowQty,Note,Updated")] Shelving shelving)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.Entry(shelving).State = EntityState.Modified;
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }
    //  ViewBag.LocationCollectionID = new SelectList(db.LocationCollections, "LocationCollectionID", "LocationCollectionID", shelving.LocationCollectionID);
    //  ViewBag.ShelfTypeID = new SelectList(db.ShelfTypes, "ShelfTypeID", "ShelfType1", shelving.ShelfTypeID);
    //  return View(shelving);
    //}

    //// GET: Shelvings/Delete/5
    //public ActionResult Delete(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  Shelving shelving = db.Shelvings.Find(id);
    //  if (shelving == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(shelving);
    //}

    //// POST: Shelvings/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public ActionResult DeleteConfirmed(int id)
    //{
    //  Shelving shelving = db.Shelvings.Find(id);
    //  db.Shelvings.Remove(shelving);
    //  db.SaveChanges();
    //  return RedirectToAction("Index");
    //}

    //protected override void Dispose(bool disposing)
    //{
    //  if (disposing)
    //  {
    //    db.Dispose();
    //  }
    //  base.Dispose(disposing);
    //}
  }
}
