using Microsoft.AspNetCore.Http;

namespace NCKH.QLDA.FileManagenment.API.Domain.ModelMetas
{
    public class FileUploadMeta
    {
        public int? FolderId { get; set; }
        public IFormFileCollection FormFileCollection { get; set; }
    }
}
