using System;
using System.IO;

namespace AgendadorDeUpload.Services
{
    public class ProgressStream : Stream
    {
        private readonly Stream _inner;
        private readonly long _length;
        private long _position;
        private readonly Action<double> _onProgress;
        private int _lastLoggedPct;

        public ProgressStream(Stream inner, long length, Action<double> onProgress)
        {
            _inner = inner;
            _length = length;
            _onProgress = onProgress;
        }

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => _length;
        public override long Position { get => _position; set => throw new NotSupportedException(); }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int read = _inner.Read(buffer, offset, count);
            _position += read;

            if (_length > 0)
            {
                var pct = _position * 100.0 / _length;
                var pctInt = (int)pct;
                if (pctInt - _lastLoggedPct >= 5 || pctInt >= 100)
                {
                    _lastLoggedPct = pctInt;
                    _onProgress?.Invoke(pct / 100.0);
                }
            }

            return read;
        }

        public override void Flush() => _inner.Flush();
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }
}
