using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.Models
{
    public class RegistTeacher
    {
        public string id { get; set; }
        public string IdStudent { get; set; }
        public string IdTeacherMain { get; set; }
        public string IdTeacher2 { get; set; }
        public string IdTopic { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDone { get; set; }
        public RegistTeacher()
        {
            CreateDate = DateTime.Now;
            LastUpdate = null;
            IsActive = true;
            IsDone = false;
        }
    }
}
