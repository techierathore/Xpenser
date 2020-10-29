
namespace Xpenser.Models
{
	public class SvcData
	{
		public string ComplexData { get; set; }
		public string OrgCode { get; set; }
		public string LoginEmail { get; set; }
		public string LoginPass { get; set; }
		public string JwToken { get; set; }
	}

	public class RefreshRequest
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}
}
