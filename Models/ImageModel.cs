using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ImageModel
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileType { get; set; }
        public long LastUpdateiedTime { get; set; }
        public DateTime LastUpdateiedDate { get; set; }
        public string FileAsBase64 { get; set; }
        public byte[] FileAsByteArray { get; set; }
    }
}
