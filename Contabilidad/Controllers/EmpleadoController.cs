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
    public class EmpleadoController : ApiController
    {
        private GarCasEntities db = new GarCasEntities();

        // GET api/Empleado
        public IQueryable<Empleado> GetEmpleadoes()
        {
            return db.Empleadoes;
        }

        // GET api/Empleado/5
        [ResponseType(typeof(Empleado))]
        public async Task<IHttpActionResult> GetEmpleado(int id)
        {
            Empleado empleado = await db.Empleadoes.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        // PUT api/Empleado/5
        public async Task<IHttpActionResult> PutEmpleado(int id, Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empleado.EmpresaID)
            {
                return BadRequest();
            }

            db.Entry(empleado).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(id))
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

        // POST api/Empleado
        [ResponseType(typeof(Empleado))]
        public async Task<IHttpActionResult> PostEmpleado(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Empleadoes.Add(empleado);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmpleadoExists(empleado.EmpresaID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = empleado.EmpresaID }, empleado);
        }

        // DELETE api/Empleado/5
        [ResponseType(typeof(Empleado))]
        public async Task<IHttpActionResult> DeleteEmpleado(int id)
        {
            Empleado empleado = await db.Empleadoes.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            db.Empleadoes.Remove(empleado);
            await db.SaveChangesAsync();

            return Ok(empleado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpleadoExists(int id)
        {
            return db.Empleadoes.Count(e => e.EmpresaID == id) > 0;
        }
    }
}