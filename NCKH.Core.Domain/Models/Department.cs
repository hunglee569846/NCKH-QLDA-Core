
using System;
using System.ComponentModel.DataAnnotations;
namespace NCKH.Core.Domain.Models
{
    public class Department
    {
        public string Id { get; set; }
        public string IdDepartment { get; set; }
        public string NameDepartment { get; set; }
        public string IdFaculty { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastUpdate { get; set; }

        public Department()
        {
            IsActive = true;
            IsDelete = false;
            LastUpdate = null;
            CreateDate = DateTime.Now;
        }
    }
}
