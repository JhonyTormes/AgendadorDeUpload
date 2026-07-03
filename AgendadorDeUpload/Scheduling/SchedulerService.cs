using System;
using System.Globalization;
using System.IO;

namespace AgendadorDeUpload.Scheduling
{
    public class SchedulerService
    {
        private readonly string _scheduledTime;

        public SchedulerService(string scheduledTime)
        {
            _scheduledTime = scheduledTime;
        }

        public DateTime? GetScheduledDateTime()
        {
            if (DateTime.TryParseExact(_scheduledTime, "yyyy-MM-dd HH:mm",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt;
            return null;
        }

        public TimeSpan GetTimeUntilRun()
        {
            var scheduled = GetScheduledDateTime();
            if (scheduled == null)
                return TimeSpan.MaxValue;

            return scheduled.Value - DateTime.Now;
        }

        public bool ShouldRunNow()
        {
            var scheduled = GetScheduledDateTime();
            if (scheduled == null)
                return false;

            var diff = DateTime.Now - scheduled.Value;
            return diff.TotalMinutes >= 0 && diff.TotalMinutes < 1;
        }

        public bool HasRun()
        {
            var markerPath = GetMarkerPath();
            return File.Exists(markerPath);
        }

        public void MarkAsRun()
        {
            var markerPath = GetMarkerPath();
            var dir = Path.GetDirectoryName(markerPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(markerPath, DateTime.Now.ToString("O"));
        }

        public void ClearMarker()
        {
            var markerPath = GetMarkerPath();
            if (File.Exists(markerPath))
                File.Delete(markerPath);
        }

        private string GetMarkerPath()
        {
            var scheduled = GetScheduledDateTime();
            var key = scheduled?.ToString("yyyyMMdd_HHmm") ?? "unknown";
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AgendadorDeUpload", $"run_{key}.marker");
        }
    }
}
