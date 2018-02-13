using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SuperShoesModels.Entities;
using SuperShoesApi.Security;
using SuperShoesApi.Models;

namespace SuperShoesApi.Controllers
{
    [BasicAuthorization("admin", "12345")]
    public class StoresController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: api/Stores
        public IHttpActionResult GetStores()
        {
            var stores = db.Stores.Select(r => new { r.Id, r.Name, r.Address }).ToList();
            return Json(new { Stores = stores, Success = true, Total_elements = stores.Count });
        }

        [Route("services/articles/stores/{ix}")]
        public IHttpActionResult GetArticlesByStoreId(string ix, int id = 0)
        {
            if (!int.TryParse(ix, out id))
            {
                return Json(new { Error_msg = "Bad Request", Error_code = 400, Success = false });
            }
            var articles = db.Stores.Find(id).Articles.Select(r => new {
                r.Id,
                r.Name,
                r.Description,
                r.Price,
                r.Total_in_shelf,
                r.Total_in_vault,
                Store_name = r.Store.Name }).ToList();
            if (articles.Count() == 0)
            {
                return Json(new { Error_msg = "Record not Found", Error_code = 404, Success = false });
            }
            return Json(new { Articles = articles, Success = true, Total_elements = articles.Count() });
        }

        // GET: api/Stores/5
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> GetStore(int id)
        {
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // PUT: api/Stores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStore(int id, Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != store.Id)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/Stores
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> PostStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stores.Add(store);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = store.Id }, store);
        }

        // DELETE: api/Stores/5
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> DeleteStore(int id)
        {
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            db.Stores.Remove(store);
            await db.SaveChangesAsync();

            return Ok(store);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoreExists(int id)
        {
            return db.Stores.Count(e => e.Id == id) > 0;
        }
    }
}