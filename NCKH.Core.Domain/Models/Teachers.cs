using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.Model
{
    public class Teachers
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
        public DateTime CreateDate{ get; set; }
        public DateTime? LastUpdate{ get; set; }
        public bool IsTopicsFull{ get; set; }
        public bool IsActive{ get; set; }
        public bool IsDelete{ get; set; }
        public Teachers()
        {
            CreateDate = DateTime.Now;
            LastUpdate = null;
            IsTopicsFull = false;
            IsActive = true;
            IsDelete = false;
        }
    }
}