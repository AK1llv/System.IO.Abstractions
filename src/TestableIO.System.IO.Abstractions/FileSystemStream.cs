﻿using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Abstractions
{
    /// <summary>
    ///     Wraps the <see cref="FileStream" />.
    /// </summary>
    public abstract class FileSystemStream : Stream
    {
        /// <inheritdoc cref="Stream.CanRead" />
        public override bool CanRead
            => _stream.CanRead;

        /// <inheritdoc cref="Stream.CanSeek" />
        public override bool CanSeek
            => _stream.CanSeek;

        /// <inheritdoc cref="Stream.CanTimeout" />
        public override bool CanTimeout
            => _stream.CanTimeout;

        /// <inheritdoc cref="Stream.CanWrite" />
        public override bool CanWrite
            => _stream.CanWrite;

        /// <inheritdoc cref="FileStream.IsAsync" />
        public bool IsAsync { get; }

        /// <inheritdoc cref="Stream.Length" />
        public override long Length
            => _stream.Length;

        /// <inheritdoc cref="FileStream.Name" />
        public string Name { get; }

        /// <inheritdoc cref="Stream.Position" />
        public override long Position
        {
            get => _stream.Position;
            set => _stream.Position = value;
        }

        /// <inheritdoc cref="Stream.ReadTimeout" />
        public override int ReadTimeout
        {
            get => _stream.ReadTimeout;
            set => _stream.ReadTimeout = value;
        }

        /// <inheritdoc cref="Stream.WriteTimeout" />
        public override int WriteTimeout
        {
            get => _stream.WriteTimeout;
            set => _stream.WriteTimeout = value;
        }

        private readonly Stream _stream;

        /// <summary>
        ///     Initializes a new instance of <see cref="FileSystemStream" />.
        /// </summary>
        /// <param name="stream">The wrapped <see cref="Stream" />.</param>
        /// <param name="path">The <see cref="FileStream.Name" /> of the stream.</param>
        /// <param name="isAsync">
        ///     The <see cref="FileStream.IsAsync" /> flag, indicating if the <see cref="FileStream" /> was
        ///     opened asynchronously or synchronously.
        /// </param>
        protected FileSystemStream(Stream stream, string? path, bool isAsync)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path), "Path cannot be null.");
            }

            if (path.Length == 0)
            {
                throw new ArgumentException("Empty path name is not legal.", nameof(path));
            }

            _stream = stream;
            Name = path;
            IsAsync = isAsync;
        }

        /// <inheritdoc cref="Stream.BeginRead(byte[], int, int, AsyncCallback?, object?)" />
        public override IAsyncResult BeginRead(byte[] buffer,
            int offset,
            int count,
            AsyncCallback? callback,
            object? state)
            => _stream.BeginRead(buffer, offset, count, callback, state);

        /// <inheritdoc cref="Stream.BeginWrite(byte[], int, int, AsyncCallback?, object?)" />
        public override IAsyncResult BeginWrite(byte[] buffer,
            int offset,
            int count,
            AsyncCallback? callback,
            object? state)
            => _stream.BeginWrite(buffer, offset, count, callback, state);

        /// <inheritdoc cref="Stream.CopyTo(Stream, int)" />
#if NETSTANDARD2_0 || NET461
	public new virtual void CopyTo(Stream destination, int bufferSize)
		=> _stream.CopyTo(destination, bufferSize);
#else
        public override void CopyTo(Stream destination, int bufferSize)
            => _stream.CopyTo(destination, bufferSize);
#endif

        /// <inheritdoc cref="Stream.CopyToAsync(Stream, int, CancellationToken)" />
        public override Task CopyToAsync(Stream destination,
            int bufferSize,
            CancellationToken cancellationToken)
            => _stream.CopyToAsync(destination, bufferSize, cancellationToken);

        /// <inheritdoc cref="Stream.EndRead(IAsyncResult)" />
        public override int EndRead(IAsyncResult asyncResult)
            => _stream.EndRead(asyncResult);

        /// <inheritdoc cref="Stream.EndWrite(IAsyncResult)" />
        public override void EndWrite(IAsyncResult asyncResult)
            => _stream.EndWrite(asyncResult);

        /// <inheritdoc cref="Stream.Flush()" />
        public override void Flush()
            => _stream.Flush();

        /// <inheritdoc cref="Stream.FlushAsync(CancellationToken)" />
        public override Task FlushAsync(CancellationToken cancellationToken)
            => _stream.FlushAsync(cancellationToken);

        /// <inheritdoc cref="Stream.Read(byte[], int, int)" />
        public override int Read(byte[] buffer, int offset, int count)
            => _stream.Read(buffer, offset, count);

#if FEATURE_SPAN
	/// <inheritdoc cref="Stream.Read(Span{byte})" />
	public override int Read(Span<byte> buffer)
		=> _stream.Read(buffer);
#endif

        /// <inheritdoc cref="Stream.ReadAsync(byte[], int, int, CancellationToken)" />
        public override Task<int> ReadAsync(byte[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken)
            => _stream.ReadAsync(buffer, offset, count, cancellationToken);

#if FEATURE_SPAN
	/// <inheritdoc cref="Stream.ReadAsync(Memory{byte}, CancellationToken)" />
	public override ValueTask<int> ReadAsync(Memory<byte> buffer,
	                                         CancellationToken cancellationToken = new())
		=> _stream.ReadAsync(buffer, cancellationToken);
#endif

        /// <inheritdoc cref="Stream.ReadByte()" />
        public override int ReadByte()
            => _stream.ReadByte();

        /// <inheritdoc cref="Stream.Seek(long, SeekOrigin)" />
        public override long Seek(long offset, SeekOrigin origin)
            => _stream.Seek(offset, origin);

        /// <inheritdoc cref="Stream.SetLength(long)" />
        public override void SetLength(long value)
            => _stream.SetLength(value);

        /// <inheritdoc cref="object.ToString()" />
        public override string? ToString()
            => _stream.ToString();

        /// <inheritdoc cref="Stream.Write(byte[], int, int)" />
        public override void Write(byte[] buffer, int offset, int count)
            => _stream.Write(buffer, offset, count);

#if FEATURE_SPAN
	/// <inheritdoc cref="Stream.Write(ReadOnlySpan{byte})" />
	public override void Write(ReadOnlySpan<byte> buffer)
		=> _stream.Write(buffer);
#endif

        /// <inheritdoc cref="Stream.WriteAsync(byte[], int, int, CancellationToken)" />
        public override Task WriteAsync(byte[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken)
            => _stream.WriteAsync(buffer, offset, count, cancellationToken);

#if FEATURE_SPAN
	/// <inheritdoc cref="Stream.WriteAsync(ReadOnlyMemory{byte}, CancellationToken)" />
	public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer,
	                                     CancellationToken cancellationToken = new())
		=> _stream.WriteAsync(buffer, cancellationToken);
#endif

        /// <inheritdoc cref="Stream.WriteByte(byte)" />
        public override void WriteByte(byte value)
            => _stream.WriteByte(value);

        /// <inheritdoc cref="Stream.Dispose(bool)" />
        protected override void Dispose(bool disposing)
        {
            _stream.Dispose();
            base.Dispose(disposing);
        }
    }
}