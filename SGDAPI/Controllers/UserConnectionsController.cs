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
    public class UserConnectionsController : ApiController
    {
        private phanmemsgdcomEntities db = new phanmemsgdcomEntities();

        // GET: api/UserConnections
        public IQueryable<UserConnection> GetUserConnections()
        {
            return db.UserConnections;
        }

        // GET: api/UserConnections/5
        [ResponseType(typeof(UserConnection))]
        public IHttpActionResult GetUserConnection(int id)
        {
            UserConnection userConnection = db.UserConnections.Find(id);
            if (userConnection == null)
            {
                return NotFound();
            }

            return Ok(userConnection);
        }

        // PUT: api/UserConnections/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserConnection(int id, UserConnection userConnection)
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
                db.SaveChanges();
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

        // POST: api/UserConnections
        [ResponseType(typeof(UserConnection))]
        public IHttpActionResult PostUserConnection(UserConnection userConnection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserConnections.Add(userConnection);
             db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userConnection.stt }, userConnection);
        }

        // DELETE: api/UserConnections/5
        [ResponseType(typeof(UserConnection))]
        public IHttpActionResult DeleteUserConnection(int id)
        {
            UserConnection userConnection = db.UserConnections.Find(id);
            if (userConnection == null)
            {
                return NotFound();
            }

            db.UserConnections.Remove(userConnection);
            db.SaveChanges();

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