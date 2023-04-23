
public class Brotli
{
    /*public static byte[] Compress( byte[] input )
    {
        using( var fs = File.OpenRead( filePath ) )
        using( var ms = new MemoryStream() )
        {
            using( var bs = new BrotliStream( ms, CompressionMode.Compress ) )
            {
                // By default, BrotliSharpLib uses a quality value of 1 and window size of 22 if the methods are not called.
                /** bs.SetQuality(quality); **/
                /** bs.SetWindow(windowSize); **/
                /** bs.SetCustomDictionary(customDict); **/
               /* fs.CopyTo( bs );
            }

            byte[] compressed = ms.ToArray();
        }
    }

    public static byte[] Decompress( byte[] input )
    {
        using( var ms = new MemoryStream() )
        using( var bs = new BrotliStream( compressedStream, CompressionMode.Decompress ) )
        {
            bs.CopyTo( ms );
        }
    }*/
}