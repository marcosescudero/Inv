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
    public class CountsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Counts
        public IQueryable<Count> GetCounts()
        {
            return 
                 db.Counts
                .Include(i => i.Item)
                .Include(i => i.Location)
                .Include(i => i.MeasureUnit);
                
        }

        // GET: api/Counts/5
        [ResponseType(typeof(Count))]
        public async Task<IHttpActionResult> GetCount(int id)
        {
            //Count count = await db.Counts.FindAsync(id);
            Count count = await db.Counts
                .Include(i => i.Item)
                .Include(i => i.Location)
                .Include(i => i.MeasureUnit)
                .FirstOrDefaultAsync(i => i.CountId == id);


            if (count == null)
            {
                return NotFound();
            }

            return Ok(count);
        }

        // GET: api/Counts/5
        [ResponseType(typeof(Count))]
        public async Task<IHttpActionResult> GetCount(int id, int ItemId)
        {
            //Count count = await db.Counts.FindAsync(id);
            Count count = await db.Counts
                .Include(i => i.Item)
                .Include(i => i.Location)
                .Include(i => i.MeasureUnit)
                .FirstOrDefaultAsync(i => i.ItemId == ItemId);

            if (count == null)
            {
                return NotFound();
            }

            return Ok(count);
        }

        // PUT: api/Counts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCount(int id, Count count)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != count.CountId)
            {
                return BadRequest();
            }

            db.Entry(count).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountExists(id))
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

        // POST: api/Counts
        [ResponseType(typeof(Count))]
        public async Task<IHttpActionResult> PostCount(Count count)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Counts.Add(count);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = count.CountId }, count);
        }

        // DELETE: api/Counts/5
        [ResponseType(typeof(Count))]
        public async Task<IHttpActionResult> DeleteCount(int id)
        {
            Count count = await db.Counts.FindAsync(id);
            if (count == null)
            {
                return NotFound();
            }

            db.Counts.Remove(count);
            await db.SaveChangesAsync();

            return Ok(count);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountExists(int id)
        {
            return db.Counts.Count(e => e.CountId == id) > 0;
        }
    }
}