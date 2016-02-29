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
    public class CategoriaController : ApiController
    {
        private GarCasEntities db = new GarCasEntities();

        // GET api/Categoria
        public IQueryable<Categoria> GetCategorias()
        {
            return db.Categorias;
        }

        // GET api/Categoria/5
        [ResponseType(typeof(Categoria))]
        public async Task<IHttpActionResult> GetCategoria(int id)
        {
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        // PUT api/Categoria/5
        public async Task<IHttpActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.CategoriaID)
            {
                return BadRequest();
            }

            db.Entry(categoria).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST api/Categoria
        [ResponseType(typeof(Categoria))]
        public async Task<IHttpActionResult> PostCategoria(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = categoria.CategoriaID }, categoria);
        }

        // DELETE api/Categoria/5
        [ResponseType(typeof(Categoria))]
        public async Task<IHttpActionResult> DeleteCategoria(int id)
        {
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            db.Categorias.Remove(categoria);
            await db.SaveChangesAsync();

            return Ok(categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaExists(int id)
        {
            return db.Categorias.Count(e => e.CategoriaID == id) > 0;
        }
    }
}