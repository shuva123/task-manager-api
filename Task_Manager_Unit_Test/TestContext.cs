using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_API;
using Task_Manager_API.Models;

namespace Task_Manager_Unit_Test
{
    class TestContext:IAppContext
    {
        public TestContext()
        {
            this.Tasks = new TestTaskDbSet();
            this.parentTasks = new TestParentTaskDbSet();
        }

        public DbSet<Task_Table> Tasks { get; set; }
        public DbSet<Parent_Task_Table> parentTasks { get; }

        public int SaveChanges()
        {
            return 0;
        }
        //public void MarkAsModified(Task_Table task) { }
        //public void MarkAsModified(Parent_Task_Table parentTask) { }
        public void Dispose()
        { }
    }
}
