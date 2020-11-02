namespace NCKH.QLDA.FileManagenment.API.Domain.ViewModels
{
	public class FileDetailViewModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public long Size { get; set; }
		public string Url { get; set; }
		public string Extension { get; set; }
		public int? FolderId { get; set; }
		public string ConcurrencyStamp { get; set; }
	}
}