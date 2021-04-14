using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SGDAPI;

namespace SGDAPI.Controllers
{
    public class UserConnections1Controller : ApiController
    {
        private phanmemsgdcomEntities db = new phanmemsgdcomEntities();

        // GET: api/UserConnections1
       
        public IQueryable<UserConnection> GetUserConnections()
        {
            return db.UserConnections;
        }

        // GET: api/UserConnections1/5
        [ResponseType(typeof(UserConnection))]
        public async Task< IHttpActionResult> GetUserConnection(int id)
        {
       
            UserConnection userConnection = await db.UserConnections.FindAsync(id);

            if (userConnection == null)
            {
                return NotFound();
            }

            return Ok(userConnection);
        }

        // PUT: api/UserConnections1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserConnection(int id, UserConnection userConnection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userConnection.stt)
            {
                return BadRequest();
            }

            db.Entry(userConnection).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserConnectionExists(id))
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

        // POST: api/UserConnections1
        [ResponseType(typeof(UserConnection))]
        public async Task<IHttpActionResult> PostUserConnection(UserConnection userConnection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var u = from us in db.UserConnections
                    where us.ComputerName == userConnection.ComputerName && us.DatabaseName == userConnection.DatabaseName
                    select us;
            if (u.Count() > 0)
            {
                UserConnection uc = u.First();
                uc.LicenceKey = userConnection.LicenceKey;
                uc.TimeEx = userConnection.TimeEx;
                userConnection.StructDb = uc.StructDb;
                userConnection.stt = uc.stt;

            }
            else
            {
                db.UserConnections.Add(userConnection);
            }
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userConnection.stt }, userConnection);
        }
        [ResponseType(typeof(UserConnection))]
        public async Task<IHttpActionResult> PostGetConnection(UserConnection userConnection)
        {
            var u = from us in db.UserConnections
                    where us.ComputerName == userConnection.ComputerName && us.DatabaseName == userConnection.DatabaseName
                    select us;
            userConnection = await u.FirstAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else

                return CreatedAtRoute("DefaultApi", new { id = userConnection.stt }, userConnection);
        }
        [ResponseType(typeof(UserConnection))]
        public async Task<IHttpActionResult> PostGetConnectionbyDBName(UserConnection userConnection)
        {
            var u = from us in db.UserConnections
                    where  us.DatabaseName == userConnection.DatabaseName
                    select us;
             userConnection = await u.FirstAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {                
                return CreatedAtRoute("DefaultApi", new { id = userConnection.stt }, userConnection);
            }
        }
        // DELETE: api/UserConnections1/5
        [ResponseType(typeof(UserConnection))]
        public async Task<IHttpActionResult> DeleteUserConnection(int id)
        {
            UserConnection userConnection = await db.UserConnections.FindAsync(id);
            if (userConnection == null)
            {
                return NotFound();
            }

            db.UserConnections.Remove(userConnection);
            await db.SaveChangesAsync();

            return Ok(userConnection);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserConnectionExists(int id)
        {
            return db.UserConnections.Count(e => e.stt == id) > 0;
        }
    }
}