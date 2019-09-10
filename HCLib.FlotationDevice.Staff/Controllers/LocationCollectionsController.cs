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
  public class LocationCollectionsController : Controller
  {
    private Default.Container db = FlotationDeviceServiceContext.Context();


    // GET: LocationCollections
    public ActionResult Index()
    {
      //var locationCollections = db.LocationCollections.Include(l => l.Collection).Include(l => l.Location);
      //return View(locationCollections.ToList());
      string LogMsg = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller." +
                      this.ControllerContext.RouteData.Values["action"].ToString() + " :: ";

      LocationCollectionsViewModel lcvm = new LocationCollectionsViewModel();

      try
      {
        lcvm.fclist = db.LocationCollections.Include(r => r.Collection).Include(r => r.Location).Include(r => r.Collection.CollectionName).Include(r => r.Location.LocationName).ToList();
      }
      catch (Exception ex)
      {
        NLogWriter.LogMessage(LogType.Error, LogMsg + "Exception getting floating collections :: " + ex.ToString());
        ModelState.AddModelError("", "Unhandled Exception occurred");
        ModelState.AddModelError("", ex.ToString());
      }
      return View(lcvm);
    }

    //// GET: LocationCollections/Details/5
    //public ActionResult Details(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  LocationCollection locationCollection = db.LocationCollections.Find(id);
    //  if (locationCollection == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(locationCollection);
    //}

    //// GET: LocationCollections/Create
    //public ActionResult Create()
    //{
    //  ViewBag.CollectionID = new SelectList(db.Collections, "CollectionID", "CollectionName");
    //  ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
    //  return View();
    //}

    //// POST: LocationCollections/Create
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Create([Bind(Include = "LocationCollectionID,LocationID,CollectionID,CheckedInItems")] LocationCollection locationCollection)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.LocationCollections.Add(locationCollection);
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }

    //  ViewBag.CollectionID = new SelectList(db.Collections, "CollectionID", "CollectionName", locationCollection.CollectionID);
    //  ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", locationCollection.LocationID);
    //  return View(locationCollection);
    //}

    //// GET: LocationCollections/Edit/5
    //public ActionResult Edit(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  LocationCollection locationCollection = db.LocationCollections.Find(id);
    //  if (locationCollection == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  ViewBag.CollectionID = new SelectList(db.Collections, "CollectionID", "CollectionName", locationCollection.CollectionID);
    //  ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", locationCollection.LocationID);
    //  return View(locationCollection);
    //}

    //// POST: LocationCollections/Edit/5
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit([Bind(Include = "LocationCollectionID,LocationID,CollectionID,CheckedInItems")] LocationCollection locationCollection)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.Entry(locationCollection).State = EntityState.Modified;
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }
    //  ViewBag.CollectionID = new SelectList(db.Collections, "CollectionID", "CollectionName", locationCollection.CollectionID);
    //  ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", locationCollection.LocationID);
    //  return View(locationCollection);
    //}

    //// GET: LocationCollections/Delete/5
    //public ActionResult Delete(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  LocationCollection locationCollection = db.LocationCollections.Find(id);
    //  if (locationCollection == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(locationCollection);
    //}

    //// POST: LocationCollections/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public ActionResult DeleteConfirmed(int id)
    //{
    //  LocationCollection locationCollection = db.LocationCollections.Find(id);
    //  db.LocationCollections.Remove(locationCollection);
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
