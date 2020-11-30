using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.ViewModel
{
    public class GetInforTeacherViewMode
    {
        public string Id { get; set; }
        public string IdTeacher { get; set; }
        public string NameTeacher { get; set; }
        public string IdDepartment { get; set; }
        public int CountTopics { get; set; }
        public bool IsTopicsFull { get; set; }
    }
}
