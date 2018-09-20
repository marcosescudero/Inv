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
    public class CountsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        public JsonResult GetBins(int locationId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var bins = db.Bins.Where(m => m.LocationId == locationId);
            return Json(bins);
        }

        // GET: Counts
        public async Task<ActionResult> Index()
        {
            var counts = db.Counts.Include(c => c.Bin).Include(c => c.Item).Include(c => c.Location).Include(c => c.MeasureUnit);
            return View(await counts.ToListAsync());
        }

        // GET: Counts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Count count = await db.Counts.FindAsync(id);
            if (count == null)
            {
                return HttpNotFound();
            }
            return View(count);
        }

        // GET: Counts/Create
        public ActionResult Create()
        {
            /*
            ViewBag.BinId = new SelectList(db.Bins, "BinId", "Description");
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Barcode");
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Description");
            ViewBag.MeasureUnitId = new SelectList(db.MeasureUnits, "MeasureUnitId", "Description");
            return View();
            */

            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Barcode");
            ViewBag.LocationId = new SelectList(db.Locations,
                    "LocationId", "Description");
            ViewBag.BinId = new SelectList(db.Bins
                    .Where(m => m.LocationId == db.Locations.FirstOrDefault().LocationId),
                    "BinId", "Description");
            ViewBag.MeasureUnitId = new SelectList(db.MeasureUnits, "MeasureUnitId", "Description");
            return View();
        }

        // POST: Counts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CountId,ItemId,LocationId,BinId,MeasureUnitId,Quantity,CountDate")] Count count)
        {
            if (ModelState.IsValid)
            {
                db.Counts.Add(count);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BinId = new SelectList(db.Bins, "BinId", "Description", count.BinId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Barcode", count.ItemId);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Description", count.LocationId);
            ViewBag.MeasureUnitId = new SelectList(db.MeasureUnits, "MeasureUnitId", "Description", count.MeasureUnitId);
            return View(count);
        }

        // GET: Counts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Count count = await db.Counts.FindAsync(id);
            if (count == null)
            {
                return HttpNotFound();
            }
            //ViewBag.BinId = new SelectList(db.Bins, "BinId", "Description", count.BinId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Barcode", count.ItemId);
            //ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Description", count.LocationId);
            ViewBag.MeasureUnitId = new SelectList(db.MeasureUnits, "MeasureUnitId", "Description", count.MeasureUnitId);

            ViewBag.LocationId = new SelectList(db.Locations,
                "LocationId", "Description",
                count.LocationId);
            ViewBag.BinId = new SelectList(db.Bins
                .Where(m => m.BinId == count.BinId),
                "BinId", "Description",
                count.BinId);


            return View(count);
        }

        // POST: Counts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CountId,ItemId,LocationId,BinId,MeasureUnitId,Quantity,CountDate")] Count count)
        {
            if (ModelState.IsValid)
            {
                db.Entry(count).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BinId = new SelectList(db.Bins, "BinId", "Description", count.BinId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Barcode", count.ItemId);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Description", count.LocationId);
            ViewBag.MeasureUnitId = new SelectList(db.MeasureUnits, "MeasureUnitId", "Description", count.MeasureUnitId);
            return View(count);
        }

        // GET: Counts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Count count = await db.Counts.FindAsync(id);
            if (count == null)
            {
                return HttpNotFound();
            }
            return View(count);
        }

        // POST: Counts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Count count = await db.Counts.FindAsync(id);
            db.Counts.Remove(count);
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
