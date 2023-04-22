
using static CompressionTesting;

class Testing
{
    static Compression[] compressions =
    {
        new( "gzip", x => GZip.Compress( x ), x => GZip.Decompress( x ) ),
        new( "deflate", x => Deflate.Compress( x ), x => Deflate.Decompress( x ) ),
    };

    static byte[] LoadCards( string filename )
    {
        return File.ReadAllBytes( filename );
    }

    private static Random random = new();

    internal static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
    public static string RandomString( int length )
    {
        return new string( Enumerable.Repeat( chars, length )
            .Select( s => s[random.Next( s.Length )] ).ToArray() );
    }

    public static string RepeatingString( string str, int length )
    {
        return string.Concat( Enumerable.Repeat( str, ( int )Math.Ceiling( length / 4.0f ) ) )[..length];
    }

    static void RunAllTests()
    {
        foreach( var compression in compressions )
        {
            List<int> randomStringTests = new() { 4, 8, 20, 40, 100, 256, 512, 1024, 2056, 4096, 10000, 20000, 50000, 100000 };
            foreach( var len in randomStringTests )
            {
                TestCompression( $"{compression.name} - Random str ({len} chars)", RandomString( len ), compression );
                TestCompression( $"{compression.name}  - Repeating str small ({len} chars)", RepeatingString( "test", len ), compression );
                TestCompression( $"{compression.name}  - Repeating str large ({len} chars)", RepeatingString( "longer test repeating string", len ), compression );
            }

            TestCompression( $"{compression.name}  - Large JSON", LoadCards( "cards.json" ), compression );
        }
    }

    static void Main( string[] _ )
    {
        RunAllTests();
    }
}