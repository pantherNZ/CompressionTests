﻿using System.IO.Compression;

namespace Compression
{
    public class Deflate
    {
        public static byte[] Compress( byte[] input )
        {
            var uncompressedStream = new MemoryStream( input );
            var compressedStream = new MemoryStream();
            var compressorStream = new DeflateStream( compressedStream, CompressionLevel.SmallestSize, true );
            uncompressedStream.CopyTo( compressorStream );
            compressorStream.Close();
            return compressedStream.ToArray();
        }

        public static byte[] Decompress( byte[] input )
        {
            var compressedStream = new MemoryStream( input );
            using var decompressorStream = new DeflateStream( compressedStream, CompressionMode.Decompress );
            using var decompressedStream = new MemoryStream();
            decompressorStream.CopyTo( decompressedStream );
            decompressorStream.Close();
            return decompressedStream.ToArray();
        }
    }
}