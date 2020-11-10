using System;
namespace NCKH.Core.Domain.Models
{
    public class Department
    {
        public string IdDepartment { get; set; }
        public string NameDepartment { get; set; }
        public string Office { get; set; }
        public string Addres { get; set; }
        public string Email { get; set; }
        public string  PhoneNumber { get; set; }
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
