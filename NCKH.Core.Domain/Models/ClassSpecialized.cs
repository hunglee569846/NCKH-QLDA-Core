using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.Models
{
    public class ClassSpecialized
    {
        public string id { get; set; }
        public string IdClass { get; set; }
        public string ClassName { get; set; }
        public string IdSpecialized { get; set; }
        public string IdEducationProgram { get; set; }
        public string Course { get; set; }
        public DateTime Createdate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public ClassSpecialized()
        {
            Createdate = DateTime.Now;
            LastUpdate = null;
            IsDelete = false;
            IsActive = true;
        }
    }
}
