using System.Text;

namespace Compression
{
    public class CompressionTesting
    {
        public struct Compressor
        {
            public Compressor( string name, Func<byte[], byte[]> compress, Func<byte[], byte[]> decompress )
            {
                this.name = name;
                this.compress = compress;
                this.decompress = decompress;
            }

            public string name;
            public Func<byte[], byte[]> compress;
            public Func<byte[], byte[]> decompress;
        }

        public static void TestCompression( string testName, byte[] input, Compressor compression )
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var compressed = compression.compress( input );
            var compressTime = timer.ElapsedMilliseconds;

            timer.Restart();
            var decompressed = compression.decompress( compressed );
            var decompressTime = timer.ElapsedMilliseconds;

            if( decompressed.SequenceEqual( input ) )
                Console.WriteLine( string.Format( "[SUCCESS] {0} - In: {1}, Out: {2}, {3:0.0}%, {4}ms {5}ms {6}ms", 
                    testName, 
                    input.Length, 
                    compressed.Length, 
                    100.0f * compressed.Length / input.Length,
                    compressTime, decompressTime, compressTime + decompressTime ) );
            else
            {
                Console.WriteLine( $"[FAILED] {testName} - Decompressed bytes did not match the input. Decompressed Size: {decompressed.Length}" );
                for( int i = 0; i < decompressed.Length; ++i )
                {
                    if( decompressed[i] != input[i] )
                    {
                        Console.WriteLine( $"Mismatching bytes: {decompressed[i]}, {input[i]}" );
                    }
                }
            }
        }

        public static void TestCompression( string testName, string input, Compressor compression )
        {
            TestCompression( testName, Encoding.UTF8.GetBytes( input ), compression );
        }
    }
}