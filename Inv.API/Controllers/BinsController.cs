using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Inv.Common.Models;
using Inv.Domain.Models;

namespace Inv.API.Controllers
{
    public class BinsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Bins
        public IQueryable<Bin> GetBins()
        {
            return db.Bins
                .Include(b => b.Location);
        }

        // GET: api/Bins/5
        [ResponseType(typeof(Bin))]
        public async Task<IHttpActionResult> GetBin(int id)
        {
            Bin bin = await db.Bins.FindAsync(id);
            if (bin == null)
            {
                return NotFound();
            }

            return Ok(bin);
        }

        // PUT: api/Bins/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBin(int id, Bin bin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bin.BinId)
            {
                return BadRequest();
            }

            db.Entry(bin).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BinExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Bins
        [ResponseType(typeof(Bin))]
        public async Task<IHttpActionResult> PostBin(Bin bin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bins.Add(bin);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bin.BinId }, bin);
        }

        // DELETE: api/Bins/5
        [ResponseType(typeof(Bin))]
        public async Task<IHttpActionResult> DeleteBin(int id)
        {
            Bin bin = await db.Bins.FindAsync(id);
            if (bin == null)
            {
                return NotFound();
            }

            db.Bins.Remove(bin);
            await db.SaveChangesAsync();

            return Ok(bin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BinExists(int id)
        {
            return db.Bins.Count(e => e.BinId == id) > 0;
        }
    }
}