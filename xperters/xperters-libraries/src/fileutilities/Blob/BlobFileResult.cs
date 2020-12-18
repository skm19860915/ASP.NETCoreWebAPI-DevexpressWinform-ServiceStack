using System.IO;

namespace xperters.fileutilities.Blob
{
    public class BlobFileResult
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public long ContentLength { get; set; }
        public MemoryStream File { get; set; }
    }
}
