using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Task_Manager_API.Models
{
    public class ParentTaskView
    {
        //public Task_Table TaskTable { get; set; }
        //public Parent_Task_Table Parent_Task { get; set; }
       
        public Nullable<int> Task_ID { get; set; }
        public Nullable<int> Parent_ID { get; set; }
        public string Task { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<int> Priority { get; set; }
        public string IsActive { get; set; }
        public string Parent_Task { get; set; }
    }
}