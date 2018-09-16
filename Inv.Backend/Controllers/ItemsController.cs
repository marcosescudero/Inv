namespace Inv.Backend.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Backend.Helpers;
    using Backend.Models;
    using Common.Models;

    [Authorize]
    public class ItemsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Items
        public async Task<ActionResult> Index()
        {
            var items = db.Items.Include(i => i.MeasureUnit);
            return View(await items.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.MeasureUnitId = new SelectList(db.MeasureUnits, "MeasureUnitId", "Description");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ItemView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Products";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var item = this.ToItem(view, pic);

                this.db.Items.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MeasureUnitId = new SelectList(db.MeasureUnits, "MeasureUnitId", "Description", view.MeasureUnitId);
            return View(view);
        }

        private Item ToItem(ItemView view, string pic)
        {
            return new Item
            {
                Description = view.Description,
                ImagePath = pic,
                IsAvailable = view.IsAvailable,
                Barcode = view.Barcode,
                ItemId = view.ItemId,
                MeasureUnit = view.MeasureUnit,
                ImageArray = view.ImageArray,
                MeasureUnitId = view.MeasureUnitId,
            };
        }

        // GET: Items/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await this.db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }


            ViewBag.MeasureUnitId = new SelectList(db.MeasureUnits, "MeasureUnitId", "Description", item.MeasureUnitId);

            var view = this.ToView(item);
            return View(view);
        }

        private ItemView ToView(Item item)
        {
            return new ItemView
            {
                Description = item.Description,
                ImagePath = item.ImagePath,
                IsAvailable = item.IsAvailable,
                Barcode = item.Barcode,
                ItemId = item.ItemId,
                MeasureUnit = item.MeasureUnit,
                ImageArray = item.ImageArray,
                MeasureUnitId = item.MeasureUnitId,
            };
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ItemView view)
        {
            if (ModelState.IsValid)
            {

                var pic = view.ImagePath;
                var folder = "~/Content/Products";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var item = this.ToItem(view, pic);

                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MeasureUnitId = new SelectList(db.MeasureUnits, "MeasureUnitId", "Description", view.MeasureUnitId);
            return View(view);
        }

        // GET: Items/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Item item = await db.Items.FindAsync(id);
            db.Items.Remove(item);
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