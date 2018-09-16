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
    public class MeasureUnitsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: MeasureUnits
        public async Task<ActionResult> Index()
        {
            return View(await db.MeasureUnits.ToListAsync());
        }

        // GET: MeasureUnits/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureUnit measureUnit = await db.MeasureUnits.FindAsync(id);
            if (measureUnit == null)
            {
                return HttpNotFound();
            }
            return View(measureUnit);
        }

        // GET: MeasureUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeasureUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MeasureUnitId,Description")] MeasureUnit measureUnit)
        {
            if (ModelState.IsValid)
            {
                db.MeasureUnits.Add(measureUnit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(measureUnit);
        }

        // GET: MeasureUnits/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureUnit measureUnit = await db.MeasureUnits.FindAsync(id);
            if (measureUnit == null)
            {
                return HttpNotFound();
            }
            return View(measureUnit);
        }

        // POST: MeasureUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MeasureUnitId,Description")] MeasureUnit measureUnit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measureUnit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(measureUnit);
        }

        // GET: MeasureUnits/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureUnit measureUnit = await db.MeasureUnits.FindAsync(id);
            if (measureUnit == null)
            {
                return HttpNotFound();
            }
            return View(measureUnit);
        }

        // POST: MeasureUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MeasureUnit measureUnit = await db.MeasureUnits.FindAsync(id);
            db.MeasureUnits.Remove(measureUnit);
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
