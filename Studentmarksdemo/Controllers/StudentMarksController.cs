using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Studentmarksdemo.Models;

namespace Studentmarksdemo.Controllers
{
    public class StudentMarksController : ApiController
    {
        private StudentdbEntities db = new StudentdbEntities();

        // GET: api/StudentMarks
        public IQueryable<StudentMark> GetStudentMarks()
        {
            return db.StudentMarks;
        }

        // GET: api/StudentMarks/5
        [ResponseType(typeof(StudentMark))]
        public IHttpActionResult GetStudentMark(int id)
        {
            StudentMark studentMark = db.StudentMarks.Find(id);
            if (studentMark == null)
            {
                return NotFound();
            }

            return Ok(studentMark);
        }

        // PUT: api/StudentMarks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentMark(int id, StudentMark studentMark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentMark.StudentID)
            {
                return BadRequest();
            }

            db.Entry(studentMark).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentMarkExists(id))
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

        // POST: api/StudentMarks
        [ResponseType(typeof(StudentMark))]
        public IHttpActionResult PostStudentMark(StudentMark studentMark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentMarks.Add(studentMark);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentMarkExists(studentMark.StudentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = studentMark.StudentID }, studentMark);
        }

        // DELETE: api/StudentMarks/5
        [ResponseType(typeof(StudentMark))]
        public IHttpActionResult DeleteStudentMark(int id)
        {
            StudentMark studentMark = db.StudentMarks.Find(id);
            if (studentMark == null)
            {
                return NotFound();
            }

            db.StudentMarks.Remove(studentMark);
            db.SaveChanges();

            return Ok(studentMark);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentMarkExists(int id)
        {
            return db.StudentMarks.Count(e => e.StudentID == id) > 0;
        }
    }
}