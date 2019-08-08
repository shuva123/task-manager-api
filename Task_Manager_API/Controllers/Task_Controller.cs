using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Task_Manager_API.Models;

namespace Task_Manager_API.Controllers
{
   
    public class Task_Controller : ApiController
    {
        private Capsule_ProjectEntities db = new Capsule_ProjectEntities();

        // GET: api/Task_
        //public IQueryable<Task_Table> GetTask_Table()
        //{
        //    return db.Task_Table;
        //}


        public IHttpActionResult GetTask_Table()
        {
            var x = (from n in db.Parent_Task_Table
                    join c in db.Task_Table
                    on n.Parent_ID equals c.Parent_ID
                    select new
                    {
                        Id=c.Task_ID,
                        Name = c.Task,
                        Parent_Task_Name = n.Parent_Task,
                        Start_Date = c.Start_Date,
                        End_Date = c.End_Date,
                        Priority = c.Priority,
                        IsActive=c.IsActive
                    }).ToList();
            return Ok(x);
        }

        // GET: api/Task_/5
        [ResponseType(typeof(Task_Table))]
        public IHttpActionResult GetTask_Table(int id)
        {
           var filteredTask_Table = db.Parent_Task_Table.Join(db.Task_Table, pt=>pt.Parent_ID, uir => uir.Parent_ID,
            (u, uir) => new { u, uir }).Where(m => m.uir.Task_ID == id)
            .Select(m => new 
            {
                Id = m.uir.Task_ID,
                Name = m.uir.Task,
                Parent_Task_Name = m.u.Parent_Task,
                Parent_ID=m.u.Parent_ID,
                Start_Date = m.uir.Start_Date,
                End_Date = m.uir.End_Date,
                Priority = m.uir.Priority,
                IsActive = m.uir.IsActive
            });
            if (filteredTask_Table == null)
            {
                return NotFound();
            }
            return Ok(filteredTask_Table);
        }

        // PUT: api/Task_/5
        [ResponseType(typeof(Task_Table))]
        [HttpPut]
        public IHttpActionResult PutTask_Table(ParentTaskView pt)
        {
            

            return Ok();

            //if (id != task_Table.Task_ID)
            //{
            //    return BadRequest();
            //}

            //db.Entry(task_Table).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!Task_TableExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Task_
        [ResponseType(typeof(Task_Table))]
        public IHttpActionResult PostTask_Table(ParentTaskView pt)
        {
            if (pt.IsActive == null)
            {
                if (pt.Task_ID == null)
                {
                    Parent_Task_Table p = new Parent_Task_Table();
                    Task_Table t = new Task_Table();

                    //Parent Task Table insert
                    p.Parent_Task = pt.Parent_Task;
                    db.Parent_Task_Table.Add(p);
                    db.SaveChanges();

                    int latestParentId = Convert.ToInt32(p.Parent_ID);

                    //Task Table insert
                    t.Parent_ID = latestParentId;
                    t.Task = pt.Task;
                    t.Start_Date = Convert.ToDateTime(pt.Start_Date);
                    t.End_Date = pt.End_Date;
                    t.Priority = Convert.ToInt32(pt.Priority);
                    db.Task_Table.Add(t);
                    //  System.IO.File.WriteAllText(@"D:\taskapilog2.txt","Add method success");
                    db.SaveChanges();
                }
                else if (pt.Task_ID != null)
                {
                    int? latestParentId = null;
                    Parent_Task_Table p = new Parent_Task_Table();
                    //System.IO.File.WriteAllText(@"D:\taskapilogUp.txt", "Update method");

                    if (pt.Parent_ID == null)
                    {
                        //Parent Task Table insert

                        p.Parent_Task = pt.Parent_Task;
                        db.Parent_Task_Table.Add(p);
                        db.SaveChanges();
                        latestParentId = Convert.ToInt32(p.Parent_ID);
                    }
                    else
                    {
                        //System.IO.File.WriteAllText(@"D:\taskapilogUp1.txt", pt.Parent_ID + "");
                        var ParentTask_TableInDb = db.Parent_Task_Table.SingleOrDefault(p1 => p1.Parent_ID == pt.Parent_ID);
                        if (ParentTask_TableInDb == null)
                            throw new HttpResponseException(HttpStatusCode.NotFound);

                        //Parent Task Table update
                        ParentTask_TableInDb.Parent_Task = pt.Parent_Task;
                        db.SaveChanges();
                    }

                    var Task_TableInDb = db.Task_Table.SingleOrDefault(c => c.Task_ID == pt.Task_ID);
                    if (Task_TableInDb == null)
                        throw new HttpResponseException(HttpStatusCode.NotFound);

                    //Task Table update
                    if (latestParentId != null)
                    {
                        Task_TableInDb.Parent_ID = latestParentId;
                    }

                    Task_TableInDb.Task = pt.Task;
                    Task_TableInDb.Start_Date = Convert.ToDateTime(pt.Start_Date);
                    Task_TableInDb.End_Date = pt.End_Date;
                    Task_TableInDb.Priority = Convert.ToInt32(pt.Priority);

                    //  System.IO.File.WriteAllText(@"D:\taskapilog2.txt","Add method success");
                    db.SaveChanges();
                }
            }
            else if(pt.IsActive=="N")
            {
                //End Task Code
                var Task_TableIndb = db.Task_Table.SingleOrDefault(c1 => c1.Task_ID == pt.Task_ID);
                if (Task_TableIndb == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                Task_TableIndb.IsActive = pt.IsActive;
                db.SaveChanges();
            }
            // return CreatedAtRoute("DefaultApi", new { id = t.Task_ID }, pt);
            return Ok();
        }

        // DELETE: api/Task_/5
        [ResponseType(typeof(Task_Table))]
        public IHttpActionResult DeleteTask_Table(int id)
        {
            Task_Table task_Table = db.Task_Table.Find(id);
            if (task_Table == null)
            {
                return NotFound();
            }

            db.Task_Table.Remove(task_Table);
            db.SaveChanges();

            return Ok(task_Table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Task_TableExists(int id)
        {
            return db.Task_Table.Count(e => e.Task_ID == id) > 0;
        }
    }
}