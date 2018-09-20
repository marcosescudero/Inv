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
    public class MeasureUnitsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/MeasureUnits
        public IQueryable<MeasureUnit> GetMeasureUnits()
        {
            return db.MeasureUnits;
        }

        // GET: api/MeasureUnits/5
        [ResponseType(typeof(MeasureUnit))]
        public async Task<IHttpActionResult> GetMeasureUnit(int id)
        {
            MeasureUnit measureUnit = await db.MeasureUnits.FindAsync(id);
            if (measureUnit == null)
            {
                return NotFound();
            }

            return Ok(measureUnit);
        }

        // PUT: api/MeasureUnits/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMeasureUnit(int id, MeasureUnit measureUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != measureUnit.MeasureUnitId)
            {
                return BadRequest();
            }

            db.Entry(measureUnit).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeasureUnitExists(id))
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

        // POST: api/MeasureUnits
        [ResponseType(typeof(MeasureUnit))]
        public async Task<IHttpActionResult> PostMeasureUnit(MeasureUnit measureUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MeasureUnits.Add(measureUnit);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = measureUnit.MeasureUnitId }, measureUnit);
        }

        // DELETE: api/MeasureUnits/5
        [ResponseType(typeof(MeasureUnit))]
        public async Task<IHttpActionResult> DeleteMeasureUnit(int id)
        {
            MeasureUnit measureUnit = await db.MeasureUnits.FindAsync(id);
            if (measureUnit == null)
            {
                return NotFound();
            }

            db.MeasureUnits.Remove(measureUnit);
            await db.SaveChangesAsync();

            return Ok(measureUnit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeasureUnitExists(int id)
        {
            return db.MeasureUnits.Count(e => e.MeasureUnitId == id) > 0;
        }
    }
}