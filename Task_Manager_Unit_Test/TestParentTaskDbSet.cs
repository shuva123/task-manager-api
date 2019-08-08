using System;
using System.Data.Entity;
using System.Linq;
using Task_Manager_API.Models;

namespace Task_Manager_Unit_Test
{
    internal class TestParentTaskDbSet : TestDbSet<Parent_Task_Table>
    {
        //public override Parent_Task_Table Find(params object[] keyValues)
        //{
        //    return this.SingleOrDefault(item => item.Parent_ID == (int)keyValues.Single());
        //}

        //public static implicit operator DbSet<object>(TestParentTaskDbSet v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}