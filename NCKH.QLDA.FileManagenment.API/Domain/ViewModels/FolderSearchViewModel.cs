using System.Collections.Generic;
namespace NCKH.QLDA.FileManagenment.API.Domain.ViewModels
{
    public class FolderSearchViewModel
    {
        public List<FileViewModel> Files { get; set; }
        public List<FolderViewModel> Folders { get; set; }
        public int TotalFiles { get; set; }
        public int TotalFolders { get; set; }
    }
}