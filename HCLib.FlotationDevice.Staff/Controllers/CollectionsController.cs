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

namespace HCLib.FlotationDevice.Staff.Controllers
{
  public class CollectionsController : Controller
  {
    private Default.Container db = FlotationDeviceServiceContext.Context();

    //// GET: Collections
    //public ActionResult Index()
    //{
    //  return View(db.Collections.ToList());
    //}

    //// GET: Collections/Details/5
    //public ActionResult Details(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  Collection collection = db.Collections.Find(id);
    //  if (collection == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(collection);
    //}

    //// GET: Collections/Create
    //public ActionResult Create()
    //{
    //  return View();
    //}

    //// POST: Collections/Create
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Create([Bind(Include = "CollectionID,CollectionName,CollectionCode,Active,AverageWidth")] Collection collection)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.Collections.Add(collection);
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }

    //  return View(collection);
    //}

    //// GET: Collections/Edit/5
    //public ActionResult Edit(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  Collection collection = db.Collections.Find(id);
    //  if (collection == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(collection);
    //}

    //// POST: Collections/Edit/5
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit([Bind(Include = "CollectionID,CollectionName,CollectionCode,Active,AverageWidth")] Collection collection)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.Entry(collection).State = EntityState.Modified;
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }
    //  return View(collection);
    //}

    //// GET: Collections/Delete/5
    //public ActionResult Delete(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  Collection collection = db.Collections.Find(id);
    //  if (collection == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(collection);
    //}

    //// POST: Collections/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public ActionResult DeleteConfirmed(int id)
    //{
    //  Collection collection = db.Collections.Find(id);
    //  db.Collections.Remove(collection);
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
