namespace xperters.domain
{
   public abstract class AttachmentDto: BaseDto
    {
        public string MimeType { get; set; }
        public string Uri { get; set; }
        public string LocalPath { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public long FileSize { get; set; }
    }
}
