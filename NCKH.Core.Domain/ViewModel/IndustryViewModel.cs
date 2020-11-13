using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.ViewModel
{
    public class IndustryViewModel
    {
        public string IdIndustry { get; set; }
        public string NameIndustry { get; set; }
        public string IdDepartment { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public string Details { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
