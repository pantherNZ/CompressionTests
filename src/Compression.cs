using System.Text;

public class CompressionTesting
{
    public struct Compression
    {
        public Compression( string name, Func<byte[], byte[]> compress, Func<byte[], byte[]> decompress ) 
        { 
            this.name = name;
            this.compress = compress; 
            this.decompress = decompress;
        }

        public string name;
        public Func<byte[], byte[]> compress;
        public Func<byte[], byte[]> decompress;
    }

    public static void TestCompression( string testName, byte[] input, Compression compression )
    {
        Console.WriteLine( $"{testName} - Input bytes size: {input.Length}" );
        var compressed = compression.compress( input );
        Console.WriteLine( $"{testName} - Compressed bytes size: {compressed.Length}" );
        var decompressed = compression.decompress( compressed );
        if( decompressed.SequenceEqual( input ) )
            Console.WriteLine( $"[SUCCESS] {testName} - Total compression from input: {( 100.0f * compressed.Length / input.Length )}%" );
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

    public static void TestCompression( string testName, string input, Compression compression )
    {
        TestCompression( testName, Encoding.UTF8.GetBytes( input ), compression );
    }
}