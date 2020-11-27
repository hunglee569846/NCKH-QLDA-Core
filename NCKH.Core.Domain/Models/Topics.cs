using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.Models
{
    public class Topics
    {
        public string id { get; set; }
        public string IdTopics { get; set; }
        public string NameTopics { get; set; }

        public string IdStudent { get; set; }
        public string IdTeacherMain { get; set; }
        public string IdTeacher2 { get; set; }
        //trang thai de tai  duoc khoa duyet hay chua
        public bool IsApproval { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Topics()
        {
            CreateDate = DateTime.Now;
            LastUpdate = null;
            IsApproval = false;
            IsActive = true;
            IsDelete = false;
        }
    }
}
