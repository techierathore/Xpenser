
namespace Xpenser.Models
{
	public class Category
	{
		public long CategoryId	{ get; set; }
		public string CategoryName	{ get; set; }
		public string CategoryDescription	{ get; set; }
		public long ParentId	{ get; set; }
		public long IconPicId { get; set; }
		public string IconPath	{ get; set; }
		public long AppUserId { get; set; }
		public string Creator { get; set; }
		public string ParentName { get; set; }
	}
}
