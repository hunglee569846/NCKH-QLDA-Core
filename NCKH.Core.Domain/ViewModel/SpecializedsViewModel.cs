using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.ViewModel
{
    public class SpecializedsViewModel
    {
        public string IdSpecialized { get; set; }
        public string NameSpecialized { get; set; }
        public string Office { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string IdIndustry { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
