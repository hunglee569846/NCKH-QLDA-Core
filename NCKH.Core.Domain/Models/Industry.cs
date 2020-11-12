using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.Models
{
    public class Industry
    {
        public string IdIndustry { get; set; }
        public string NameIndustry { get; set; }
        public string IdDepartment { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? Deletetime { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public Industry()
        {
            CreateDate = DateTime.Now;
            LastUpdate = null;
            Deletetime = null;
            IsDelete = false;
            IsActive = true;
        }
    }
}
