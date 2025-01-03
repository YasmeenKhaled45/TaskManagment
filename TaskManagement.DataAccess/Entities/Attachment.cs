using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Entities
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public int TaskId { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.Now;

        public Tasks Task { get; set; } = null!;
    }
}
