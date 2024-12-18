using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.Models
{
    public class PicUpload
    {
        public string FileId {  get; set; }
        public string FileName { get; set; }
        public string FileUrl {  get; set; }
        public IEnumerable<PicUpload> FileList { get; set; }
    }
}