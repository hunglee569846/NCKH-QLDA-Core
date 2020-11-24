using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.Model
{
    public class Students
    {
        public string id { get; set; }
        public string idStudent { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string IdClass { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public Students()
        {
            CreateDate = DateTime.Now;
            IsActive = true;
            IsDelete = false;
        }
    }
}
