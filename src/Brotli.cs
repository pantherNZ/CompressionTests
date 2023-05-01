using System.IO.Compression;

namespace Compression
{
    public class BrotliSharp
    {
        public static byte[] Compress( byte[] input )
        {
            return BrotliSharpLib.Brotli.CompressBuffer( input, 0, input.Length, 11, 24 );
        }

        public static byte[] Decompress( byte[] input )
        {
            return BrotliSharpLib.Brotli.DecompressBuffer( input, 0, input.Length );
        }
    }

    public class BrotliSystem
    {
        public static byte[] Compress( byte[] input )
        {
            using( var outputStream = new MemoryStream() )
            {
                using( var compressionStream = new BrotliStream( outputStream, CompressionLevel.Optimal ) )
                {
                    compressionStream.Write( input, 0, input.Length );
                }
                return outputStream.ToArray();
            }
        }

        public static byte[] Decompress( byte[] input )
        {
            using( var inputStream = new MemoryStream( input ) )
            {
                using( var outputStream = new MemoryStream() )
                {
                    using( var compressionStream = new BrotliStream( inputStream, CompressionMode.Decompress ) )
                    {
                        compressionStream.CopyTo( outputStream );
                    }
                    return outputStream.ToArray();
                }
            }
        }
    }
}