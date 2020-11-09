using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.Models
{
    public class Specialized
    {
        public int Id { get; set; }
        public string IdSpecialized { get; set; }
        public string Name { get; set; }
        public string Office { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string IdIndustry { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public Specialized()
        {
            CreateDate = DateTime.Now;
            IsDelete = false;
            IsActive = true;
        }
    }
}
