namespace Xperters.Admin.UI.Common
{
	public class LookupItemDto<TId>
	{
		public TId Id { get; set; }
		public TId ParentId { get; set; }
		public string Category { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; } = true;
		public bool IsSelected { get; set; } = false;
		public int SortOrder { get; set; }
	}
}