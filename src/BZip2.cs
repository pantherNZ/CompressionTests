using ICSharpCode.SharpZipLib.BZip2;

namespace Compression
{
    public class BZip
    {
        public static byte[] Compress( byte[] input )
        {
            using( var inputStream = new MemoryStream( input ) )
            {
                using( var outputStream = new MemoryStream() )
                {
                    BZip2.Compress( inputStream, outputStream, true, 9 );
                    return outputStream.ToArray();
                }
            }
        }

        public static byte[] Decompress( byte[] input )
        {
            using( var inputStream = new MemoryStream( input ) )
            {
                using( var outputStream = new MemoryStream() )
                {
                    BZip2.Decompress( inputStream, outputStream, true );
                    return outputStream.ToArray();
                }
            }
        }
    }

}
