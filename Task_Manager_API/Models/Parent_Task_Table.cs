//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Task_Manager_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Parent_Task_Table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Parent_Task_Table()
        {
            this.Task_Table = new HashSet<Task_Table>();
        }
    
        public int Parent_ID { get; set; }
        public string Parent_Task { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task_Table> Task_Table { get; set; }
    }
}
