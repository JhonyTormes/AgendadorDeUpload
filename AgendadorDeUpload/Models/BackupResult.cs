using System;

namespace AgendadorDeUpload.Models
{
    public class BackupResult
    {
        public bool Success { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public long FileSizeBytes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ErrorMessage { get; set; }
        public string GoogleDriveFileId { get; set; }
        public string GoogleDriveLink { get; set; }
    }
}
