
using Compression;
using static Compression.CompressionTesting;

class Testing
{
    static readonly Compressor[] compressions =
    {
        new( "GZip", x => GZip.Compress( x ), x => GZip.Decompress( x ) ),
        new( "Deflate", x => Deflate.Compress( x ), x => Deflate.Decompress( x ) ),
        new( "BrotliSharpLib", x => BrotliSharp.Compress( x ), x => BrotliSharp.Decompress( x ) ),
        new( "System.IO Brotli", x => BrotliSystem.Compress( x ), x => BrotliSystem.Decompress( x ) ),
        new( "BZip2", x => BZip.Compress( x ), x => BZip.Decompress( x ) ),
        new( "Snappy", x => Snappy.Compress( x ), x => Snappy.Decompress( x ) ),
    };

    static byte[] LoadBinaryFile( string filename )
    {
        return File.ReadAllBytes( filename );
    }

    static readonly Random random = new();
    static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    static string RandomString( int length )
    {
        return new string( Enumerable.Repeat( chars, length ).Select( s => s[random.Next( s.Length )] ).ToArray() );
    }

    static string RepeatingString( string str, int length )
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
                TestCompression( $"{compression.name} - Repeating str small ({len} chars)", RepeatingString( "test", len ), compression );
                TestCompression( $"{compression.name} - Repeating str large ({len} chars)", RepeatingString( "longer test repeating string", len ), compression );
            }
            
            //TestCompression( $"{compression.name} - Large JSON", LoadBinaryFile( "cards.json" ), compression );
            TestCompression( $"{compression.name} - YGO binder url eg1", "https://panthernz.github.io/YuGiOh-Portfolio/?binder=A[=7Bj;A}Ar oA A=|cA/A4A*A A8BRA9AmA]nB�AN !$$!BgBjBjBjA8=A*\"!BjBjBjBjBE4T%!BiBjBjBjmBdAy#!AvBjBjBj", compression );
            TestCompression( $"{compression.name} - YGO binder url eg2", "https://panthernz.github.io/YuGiOh-Portfolio/?binder=\"A[=7Bj;A}A\"r+oA+A=|cA/A4A*A+A8BRA9AmA]nB%BEAN+!$$!BgBjBjBjA8=A*\"\"!BjBjBjBjBE4T%\"!BiBjBjBjmBdAy#\"!AvBjBjBj", compression );
            
            TestCompression( $"{compression.name} - YGO binder data raw eg1", LoadBinaryFile( "Binder1.dat" ), compression );
            TestCompression( $"{compression.name} - YGO binder data raw eg2", LoadBinaryFile( "Binder2.dat" ), compression );
        }
    }

    static void Main( string[] _ )
    {
        RunAllTests();
    }
}