using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.ViewModel
{
    public class TeacherViewModel
    {
        public string Id { get; set; }
        public string IdTeacher { get; set; }
        public string NameTeacher { get; set; }
        public string IdDepartment { get; set; }
        public string Note { get; set; }
        public string WorkingCompany { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CountTopics { get; set; }
    }
}
