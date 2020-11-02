using System;

namespace NCKH.QLDA.FileManagenment.API.Domain.ViewModels
{
    public class FolderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdPath { get; set; }
        public int? ParentId { get; set; }
        public int ChildCount { get; set; }
        public string CreatorId { get; set; }
        public string CreatorFullName { get; set; }
        public string CreatorAvatar { get; set; }
        public DateTime CreateTime { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
