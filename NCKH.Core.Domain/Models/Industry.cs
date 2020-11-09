using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.Models
{
    public class Industry
    {
        public int Id { get; set; }
        public string IdIndustry { get; set; }
        public string NameIndustry { get; set; }
        public string IdDepartment { get; set; }
        public string Details { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public Industry()
        {
            CreateDate = DateTime.Now;
            LastUpdate = null;
            IsDelete = false;
            IsActive = true;
        }
    }
}
