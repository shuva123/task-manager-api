using Task_Manager_API.Models;
using System.Linq;
using System;
using System.Data.Entity;

namespace Task_Manager_Unit_Test
{
    internal class TestTaskDbSet:TestDbSet<Task_Table>
    {
        public override Task_Table Find(params object[] keyValues)
        {
            return this.SingleOrDefault(item => item.Task_ID == (int)keyValues.Single());
        }

        public static implicit operator DbSet<object>(TestTaskDbSet v)
        {
            throw new NotImplementedException();
        }
    }
}
