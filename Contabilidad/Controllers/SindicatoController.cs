using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Contabilidad.Models;

namespace Contabilidad.Controllers
{
    public class SindicatoController : ApiController
    {
        private GarCasEntities db = new GarCasEntities();

        // GET api/Sindicato
        public IQueryable<Sindicato> GetSindicatoes()
        {
            return db.Sindicatoes;
        }

        // GET api/Sindicato/5
        [ResponseType(typeof(Sindicato))]
        public async Task<IHttpActionResult> GetSindicato(int id)
        {
            Sindicato sindicato = await db.Sindicatoes.FindAsync(id);
            if (sindicato == null)
            {
                return NotFound();
            }

            return Ok(sindicato);
        }

        // PUT api/Sindicato/5
        public async Task<IHttpActionResult> PutSindicato(int id, Sindicato sindicato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sindicato.SindicatoID)
            {
                return BadRequest();
            }

            db.Entry(sindicato).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SindicatoExists(id))
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

        // POST api/Sindicato
        [ResponseType(typeof(Sindicato))]
        public async Task<IHttpActionResult> PostSindicato(Sindicato sindicato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sindicatoes.Add(sindicato);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sindicato.SindicatoID }, sindicato);
        }

        // DELETE api/Sindicato/5
        [ResponseType(typeof(Sindicato))]
        public async Task<IHttpActionResult> DeleteSindicato(int id)
        {
            Sindicato sindicato = await db.Sindicatoes.FindAsync(id);
            if (sindicato == null)
            {
                return NotFound();
            }

            db.Sindicatoes.Remove(sindicato);
            await db.SaveChangesAsync();

            return Ok(sindicato);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SindicatoExists(int id)
        {
            return db.Sindicatoes.Count(e => e.SindicatoID == id) > 0;
        }
    }
}