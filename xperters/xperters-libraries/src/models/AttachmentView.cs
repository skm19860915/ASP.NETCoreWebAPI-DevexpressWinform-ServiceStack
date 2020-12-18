using System;

namespace xperters.models
{
  public abstract class AttachmentView : BaseView  
    {
        public string MimeType { get; set; }
        public string Uri { get; set; }
        public string LocalPath { get; set; }
        public string FileName { get; set; }
        public Guid JobId { get; set; }
        public byte[] FileData { get; set; }
        public long FileSize { get; set; }
    }
}
