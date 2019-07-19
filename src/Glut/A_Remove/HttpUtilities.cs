using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Glut
{
    // TODO: not used remove
    public static partial class HttpUtilities
    {
        public const string Http10Version = "HTTP/1.0";
        public const string Http11Version = "HTTP/1.1";
        public const string Http2Version = "HTTP/2";

        public static ReadOnlySpan<byte> EndHeadersBytes => new byte[] { (byte)'\r', (byte)'\n', (byte)'\r', (byte)'\n' };

        public static Version GetHttpVersion(string value)
        {
            if (value == Http11Version)
            {
                return HttpVersion.Version11;
            }
            else if (value == Http10Version)
            {
                return HttpVersion.Version10;
            }
            else if (value == Http2Version)
            {
                return new Version(2, 0);
            }
            else
            {
                return new Version(0, 0);
            }
        }
    }
}
