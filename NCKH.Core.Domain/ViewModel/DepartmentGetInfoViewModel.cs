using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.ViewModel
{
    public class DepartmentGetInfoViewModel
    {
        public string IdDepartment { get; set; }
        public string NameDepartment { get; set; }
        public string Office { get; set; }
        public string Addres { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdFaculty { get; set; }
        public string NameFaculty { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
