
using System;
using System.ComponentModel.DataAnnotations;
namespace NCKH.Core.Domain.Models
{
    public class BoMon
    {
        public string Id { get; set; }
        public string MaBoMon { get; set; }
        public string TenBoMon { get; set; }
        public string IdFaculty { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastUpdate { get; set; }

        public BoMon()
        {
            IsActive = true;
            IsDelete = false;
            LastUpdate = null;
            CreateDate = DateTime.Now;
        }
    }
}
