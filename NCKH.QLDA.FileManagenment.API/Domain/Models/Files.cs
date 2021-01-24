 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCKH.QLDA.FileManagenment.API.Domain.Models
{
    public class Files
    {
        public string Id { get; set; }
        public string FileCode { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string CreatorId { get; set; }
        public int? Folderld { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? DeleteTime { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public Files()
        {
            CreateDate = DateTime.Now;
            LastUpdate = null;
            DeleteTime = null;
            IsActive = true; 
            IsDelete = false;
        }
    }
}
