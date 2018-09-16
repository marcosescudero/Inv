using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inv.Backend.Models;
using Inv.Common.Models;

namespace Inv.Backend.Controllers
{
    public class BinsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Bins
        public async Task<ActionResult> Index()
        {
            var bins = db.Bins.Include(b => b.Location);
            return View(await bins.ToListAsync());
        }

        // GET: Bins/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bin bin = await db.Bins.FindAsync(id);
            if (bin == null)
            {
                return HttpNotFound();
            }
            return View(bin);
        }

        // GET: Bins/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Description");
            return View();
        }

        // POST: Bins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BinId,Description,LocationId")] Bin bin)
        {
            if (ModelState.IsValid)
            {
                db.Bins.Add(bin);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Description", bin.LocationId);
            return View(bin);
        }

        // GET: Bins/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bin bin = await db.Bins.FindAsync(id);
            if (bin == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Description", bin.LocationId);
            return View(bin);
        }

        // POST: Bins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BinId,Description,LocationId")] Bin bin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bin).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Description", bin.LocationId);
            return View(bin);
        }

        // GET: Bins/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bin bin = await db.Bins.FindAsync(id);
            if (bin == null)
            {
                return HttpNotFound();
            }
            return View(bin);
        }

        // POST: Bins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Bin bin = await db.Bins.FindAsync(id);
            db.Bins.Remove(bin);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
