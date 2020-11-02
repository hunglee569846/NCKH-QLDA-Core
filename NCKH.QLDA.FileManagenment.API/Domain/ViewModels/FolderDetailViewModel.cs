namespace NCKH.QLDA.FileManagenment.API.Domain.ViewModels
{
	public class FolderDetailViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string IdPath { get; set; }
		public string NamePath { get; set; }
		public int Order { get; set; }
		public string OrderPath { get; set; }
		public int? ParentId { get; set; }
		public string Description { get; set; }
		public int Level { get; set; }
		public int ChildCount { get; set; }
		public string ConcurrencyStamp { get; set; }
	}
}