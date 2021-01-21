
using System.IO;

namespace Xpenser.Models
{
	public class ImagesNDoc
	{
		public long ImgDocId { get; set; }

		public string ImgDocOrigName { get; set; }

		public string ImgDocName { get; set; }

		public string ImgDocPath { get; set; }

		public string ContentType { get; set; }

		public string ImgDocType { get; set; }

		public long BaseRecordId { get; set; }

		public Stream DocFile { get; set; }
	}
}
