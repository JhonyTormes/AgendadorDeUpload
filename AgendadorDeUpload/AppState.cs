using System;
using System.Threading;

namespace AgendadorDeUpload
{
    internal static class AppState
    {
        public static string MasterPassword { get; set; }
        public static DateTime? LastAuthTime { get; set; }
        public static bool IsRunning { get; set; }
        public static CancellationTokenSource CancelSource { get; set; }
        public static string CachedConfigJson { get; set; }
    }
}
