using System;
using System.Collections.Generic;
using System.Text;

namespace NCKH.Core.Domain.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string IdFaculty { get; set; }
        public string NameFaculty { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public Faculty()
        {
            IsActive = true;
            IsDelete = false;
        }
    }
}
