using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.ViewModel
{
    public class ClassViewModel
    {
        public string id { get; set; }
        public string IdClass { get; set; }
        public string ClassName { get; set; }
        public string IdSpecialized { get; set; }
        public string IdEducationProgram { get; set; }
        public string Course { get; set; }
        public DateTime Createdate { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
