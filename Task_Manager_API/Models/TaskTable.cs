using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager_API.Models
{
    public class TaskTable
    {
        public int Task_ID { get; set; }
        public Nullable<int> Parent_ID { get; set; }
        public string Task { get; set; }
        public System.DateTime Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public int Priority { get; set; }
    }
}