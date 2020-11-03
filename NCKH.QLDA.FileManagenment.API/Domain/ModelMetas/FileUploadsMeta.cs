using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace NCKH.QLDA.FileManagenment.API.Domain.ModelMetas
{
    public class FileUploadsMeta
    {
        public int? FolderId { get; set; }
        public List<IFormFile> FormFiles { get; set; }
    }
}
