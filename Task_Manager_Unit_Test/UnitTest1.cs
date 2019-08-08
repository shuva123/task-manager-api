using System;
using Task_Manager_API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_Manager_API.Models;
using System.Threading;
using System.Net.Http;
using System.Web.Http.Results;
using System.Net;
using System.Collections.Generic;

namespace Task_Manager_Unit_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAllTasks()
        {
            var context = new TestContext();
            context.Tasks.Add(new Task_Table {Parent_Task_Table= context.parentTasks.Add(new Parent_Task_Table
            { Parent_ID= 30,Parent_Task = "Parent Task 30" }),Parent_ID=30,Start_Date = Convert.ToDateTime("2019/01/01"),
              End_Date = Convert.ToDateTime("2019/09/09"),Priority=3,IsActive=null });
            context.Tasks.Add(new Task_Table {Parent_Task_Table= context.parentTasks.Add(new Parent_Task_Table
            { Parent_ID = 40, Parent_Task = "Parent Task 40" }),Parent_ID = 40,Start_Date = Convert.ToDateTime("2019/10/01"),
             End_Date = Convert.ToDateTime("2019/10/09"), Priority = 2,IsActive="N" });
            context.Tasks.Add(new Task_Table{Parent_Task_Table = context.parentTasks.Add(new Parent_Task_Table
            { Parent_ID = 50, Parent_Task = "Parent Task 50" }), Parent_ID= 50,Start_Date = Convert.ToDateTime("2019/11/10"),
             End_Date = Convert.ToDateTime("2019/03/09"), Priority = 1, IsActive=null });
            var controller = new Task_Controller(context);
            var contentResult = controller.GetAllTasks_Table() as List<AllTaskView>;
            Assert.AreEqual(contentResult.Count, 3);
        }
        [TestMethod]
        public void DeleteTask_ShouldReturnOK()
        {
            var context = new TestContext();
            var task = GetDemoTask();
            context.Tasks.Add(task);

            var controller = new Task_Controller(context);
            var result = controller.DeleteTask_Table(30) as OkNegotiatedContentResult<Task_Table>;

            Assert.IsNotNull(result);
            Assert.AreEqual(task.Task_ID, result.Content.Task_ID);
        }
        [TestMethod]
        public void PostTask_ShouldReturnSameItem()
        {
            var controller = new Task_Controller(new TestContext());

            var task = GetDemoTask();
            ParentTaskView pt = new ParentTaskView();
            pt = new ParentTaskView
            {
                Start_Date = task.Start_Date,
                End_Date = task.End_Date,
                Priority = task.Priority,
                IsActive = task.IsActive,
                Parent_Task = task.Parent_Task_Table.Parent_Task,
                Task=task.Task
            };
            var result =controller.PostTask_Table(pt) as CreatedAtRouteNegotiatedContentResult<ParentTaskView>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Task_ID);
            Assert.AreEqual(result.Content.Task, task.Task);
        }
        [TestMethod]
        public void GetSupplier_ShouldReturnItemWithSameID()
        {
            var context = new TestContext();
            context.Tasks.Add(GetDemoTask());

            var controller = new Task_Controller(context);
            var result = controller.GetTask_Table(30);
            Assert.IsNotNull(result);
            //Assert.AreEqual(30, result.Content.Task_ID);
        }
        [TestMethod]
        public void PutSupplier_ShouldFail_WhenDifferentID()
        {
            var controller = new Task_Controller(new TestContext());

            var task = GetDemoTask();
            ParentTaskView pt = new ParentTaskView();
            pt = new ParentTaskView
            {
                Start_Date = task.Start_Date,
                End_Date = task.End_Date,
                Priority = task.Priority,
                IsActive = task.IsActive,
                Parent_Task = task.Parent_Task_Table.Parent_Task,
                Task = task.Task,
            };
            var result = controller.PutTask_Table(pt,1000);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        Task_Table GetDemoTask()
        {
            return new Task_Table
            {
                Parent_Task_Table = new Parent_Task_Table
                { Parent_ID = 30, Parent_Task = "Parent Task 30" },
                Task="Task 30",
                Task_ID=30,
                Parent_ID = 30,
                Start_Date = Convert.ToDateTime("2019/01/01"),
                End_Date = Convert.ToDateTime("2019/09/09"),
                Priority = 3,
                IsActive = null
            };         
        }
    }
}
