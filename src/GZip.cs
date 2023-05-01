using System.IO.Compression;

namespace Compression
{
    public class GZip
    {
        public static byte[] Compress( byte[] input )
        {
            using var result = new MemoryStream();
            var lengthBytes = BitConverter.GetBytes( input.Length );
            result.Write( lengthBytes, 0, 4 );

            using var compressionStream = new GZipStream( result, CompressionMode.Compress );
            compressionStream.Write( input, 0, input.Length );
            compressionStream.Flush();

            return result.ToArray();
        }

        public static byte[] Decompress( byte[] input )
        {
            using var source = new MemoryStream( input );
            byte[] lengthBytes = new byte[4];
            source.Read( lengthBytes, 0, lengthBytes.Length );

            var length = BitConverter.ToInt32( lengthBytes, 0 );
            using var decompressionStream = new GZipStream( source, CompressionMode.Decompress );
            var result = new byte[length];
            int totalRead = 0, bytesRead;
            while( ( bytesRead = decompressionStream.Read( result, totalRead, result.Length - totalRead ) ) > 0 )
            {
                totalRead += bytesRead;
            }
            return result;

        }
    }
}