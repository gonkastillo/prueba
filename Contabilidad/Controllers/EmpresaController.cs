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
    public class EmpresaController : ApiController
    {
        private GarCasEntities db = new GarCasEntities();

        // GET api/Empresa
        public IQueryable<Empresa> GetEmpresas()
        {
            return db.Empresas;
        }

        // GET api/Empresa/5
        [ResponseType(typeof(Empresa))]
        public async Task<IHttpActionResult> GetEmpresa(int id)
        {
            Empresa empresa = await db.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        // PUT api/Empresa/5
        public async Task<IHttpActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empresa.EmpresaID)
            {
                return BadRequest();
            }

            db.Entry(empresa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
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

        // POST api/Empresa
        [ResponseType(typeof(Empresa))]
        public async Task<IHttpActionResult> PostEmpresa(Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Empresas.Add(empresa);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = empresa.EmpresaID }, empresa);
        }

        // DELETE api/Empresa/5
        [ResponseType(typeof(Empresa))]
        public async Task<IHttpActionResult> DeleteEmpresa(int id)
        {
            Empresa empresa = await db.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            db.Empresas.Remove(empresa);
            await db.SaveChangesAsync();

            return Ok(empresa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpresaExists(int id)
        {
            return db.Empresas.Count(e => e.EmpresaID == id) > 0;
        }
    }
}