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
  public class ShelfTypesController : Controller
  {
    private Default.Container db = FlotationDeviceServiceContext.Context();

    //// GET: ShelfTypes
    //public ActionResult Index()
    //{
    //  return View(db.ShelfTypes.ToList());
    //}

    //// GET: ShelfTypes/Details/5
    //public ActionResult Details(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  ShelfType shelfType = db.ShelfTypes.Find(id);
    //  if (shelfType == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(shelfType);
    //}

    //// GET: ShelfTypes/Create
    //public ActionResult Create()
    //{
    //  return View();
    //}

    //// POST: ShelfTypes/Create
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Create([Bind(Include = "ShelfTypeID,ShelfType1")] ShelfType shelfType)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.ShelfTypes.Add(shelfType);
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }

    //  return View(shelfType);
    //}

    //// GET: ShelfTypes/Edit/5
    //public ActionResult Edit(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  ShelfType shelfType = db.ShelfTypes.Find(id);
    //  if (shelfType == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(shelfType);
    //}

    //// POST: ShelfTypes/Edit/5
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit([Bind(Include = "ShelfTypeID,ShelfType1")] ShelfType shelfType)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.Entry(shelfType).State = EntityState.Modified;
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }
    //  return View(shelfType);
    //}

    //// GET: ShelfTypes/Delete/5
    //public ActionResult Delete(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  ShelfType shelfType = db.ShelfTypes.Find(id);
    //  if (shelfType == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(shelfType);
    //}

    //// POST: ShelfTypes/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public ActionResult DeleteConfirmed(int id)
    //{
    //  ShelfType shelfType = db.ShelfTypes.Find(id);
    //  db.ShelfTypes.Remove(shelfType);
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
