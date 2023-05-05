# Compression Testing

Small C# .Net program to test various compression algorithms on different data sources. Primary goal was to find the best algorithm to store a large JSON file for my other project (Yu-Gi-Oh online binder), where for advanced search and offline mode I needed to store the whole 15kb card database. Single threaded processing currently.

All processing is done in Release, byte array to byte array. PC settings:
Windows 10 Home N-64 bit
AMD Ryzen 5 5600X 6-core (12 threads), 3.7ghz
32GB RAM
NVIDIA GeForce RTX 3070Ti (EVGA)

Compression Types:
 - GZip
 - Deflate
 - System.IO Brotli
 - BZip2
 - Snappy

Data Sources:
- JSON (Full Yu-Gi-OH card database library - 15kb)
- Random string (1kb)
- Random string (100kb)
- Repeating string ('abcd' repeated - 1024 bytes)
- Repeating string ('abcd' repeated - 10 kb)',
- YGO Binder Data (Raw bytes for binder saved data - 350 bytes, 100 bytes)

Results file also generates data output for several other data sources of different sizes. Available in results/results.csv.

![Alt text](/results/Size.png?raw=true)
![Alt text](/results/SpeedCmp.png?raw=true)
![Alt text](/results/SpeedDecmp.png?raw=true)
