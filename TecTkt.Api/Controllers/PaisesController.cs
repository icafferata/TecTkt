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
using TecTkt.Common.Models;
using TecTkt.Domain.Models;

namespace TecTkt.Api.Controllers
{
    public class PaisesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Paises
        public IQueryable<Pais> GetPais()
        {
            return db.Pais;
        }

        // GET: api/Paises/5
        [ResponseType(typeof(Pais))]
        public async Task<IHttpActionResult> GetPais(int id)
        {
            Pais pais = await db.Pais.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }

            return Ok(pais);
        }

        // PUT: api/Paises/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPais(int id, Pais pais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pais.CountryId)
            {
                return BadRequest();
            }

            db.Entry(pais).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaisExists(id))
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

        // POST: api/Paises
        [ResponseType(typeof(Pais))]
        public async Task<IHttpActionResult> PostPais(Pais pais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pais.Add(pais);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pais.CountryId }, pais);
        }

        // DELETE: api/Paises/5
        [ResponseType(typeof(Pais))]
        public async Task<IHttpActionResult> DeletePais(int id)
        {
            Pais pais = await db.Pais.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }

            db.Pais.Remove(pais);
            await db.SaveChangesAsync();

            return Ok(pais);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaisExists(int id)
        {
            return db.Pais.Count(e => e.CountryId == id) > 0;
        }
    }
}