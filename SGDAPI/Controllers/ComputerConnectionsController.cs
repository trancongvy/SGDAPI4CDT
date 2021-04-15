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
using SGDAPI;

namespace SGDAPI.Controllers
{
    public class ComputerConnectionsController : ApiController
    {
        private phanmemsgdcomEntities db = new phanmemsgdcomEntities();

        // GET: api/ComputerConnections
        public IQueryable<ComputerConnection> GetComputerConnections()
        {
            return db.ComputerConnections;
        }

        // GET: api/ComputerConnections/5
        [ResponseType(typeof(ComputerConnection))]
        public async Task<IHttpActionResult> GetComputerConnection(int id)
        {
            ComputerConnection computerConnection = await db.ComputerConnections.FindAsync(id);
            if (computerConnection == null)
            {
                return NotFound();
            }

            return Ok(computerConnection);
        }

        // PUT: api/ComputerConnections/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComputerConnection(int id, ComputerConnection computerConnection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != computerConnection.stt)
            {
                return BadRequest();
            }

            db.Entry(computerConnection).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerConnectionExists(id))
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

        // POST: api/ComputerConnections
        [ResponseType(typeof(ComputerConnection))]
        public async Task<IHttpActionResult> PostComputerConnection(ComputerConnection computerConnection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ComputerConnections.Add(computerConnection);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = computerConnection.stt }, computerConnection);
        }
        // POST: api/PostGetComputerConnectionbyOject
        [ResponseType(typeof(ComputerConnection))]
        public async Task< IHttpActionResult> PostGetComputerConnectionbyOject(ComputerConnection computerConnection)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var u = from us in db.ComputerConnections
                    where us.ComputerName == computerConnection.ComputerName && us.DatabaseName == computerConnection.DatabaseName
                    && us.CPUID == computerConnection.CPUID && us.StructDB == computerConnection.StructDB
                    select us;
           // int i = await u.CountAsync();
            if (await u.CountAsync() > 0)
            {
                return Ok(computerConnection);
            }
            else
            {
                return NotFound();
            }


        }
        // DELETE: api/ComputerConnections/5
        [ResponseType(typeof(ComputerConnection))]
        public async Task<IHttpActionResult> DeleteComputerConnection(int id)
        {
            ComputerConnection computerConnection = await db.ComputerConnections.FindAsync(id);
            if (computerConnection == null)
            {
                return NotFound();
            }

            db.ComputerConnections.Remove(computerConnection);
            await db.SaveChangesAsync();

            return Ok(computerConnection);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComputerConnectionExists(int id)
        {
            return db.ComputerConnections.Count(e => e.stt == id) > 0;
        }
    }
}