using System.IO.Compression;

namespace Compression
{
    public class Snappy
    {
        public static byte[] Compress( byte[] input )
        {
            return IronSnappy.Snappy.Encode( input );
        }

        public static byte[] Decompress( byte[] input )
        {
            return IronSnappy.Snappy.Decode( input );
        }
    }
}