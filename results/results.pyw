import matplotlib as mpl
import matplotlib.pyplot as plt
import numpy as np

mpl.rcParams['toolbar'] = 'None'

results = open('results.csv', 'r')
header = results.readline().split(',')

compression_types = ("GZip", "Deflate", "System.IO Brotli", "BZip2", "Snappy")

entry_types = {
    'JSON (15kb)': 'Large JSON',
    'Random string (1kb)': 'Random str (1024 chars)',
    'Random string (100kb)': 'Random str (100000 chars)',
    'Repeating string (1kb)': 'Repeating str large (1024 chars)',
    'Repeating string (100kb)': 'Repeating str large (100000 chars)',
    'YGO Binder Data': 'YGO binder data raw eg1',
}

all_lines = results.readlines()
data_ratios = {}
data_speeds_cmp = {}
data_speeds_dcmp = {}

for key, value in entry_types.items():
    data_ratios[key] = []
    data_speeds_cmp[key] = []
    data_speeds_dcmp[key] = []

for x in all_lines:
    seperator = x.find(' - ')
    compressor = x[:seperator]
    test_name = x[seperator+3:x.find(',')]

    if compressor not in compression_types:
        continue

    for key, value in entry_types.items():
        if test_name == value:
            data_ratios[key].append(float(x.split(',')[4].replace('\n', '')))
            data_speeds_cmp[key].append(float(x.split(',')[2]))
            data_speeds_dcmp[key].append(float(x.split(',')[3]))
            break

x = np.arange(len(compression_types))  # the label locations
width = 0.1  # the width of the bars
multiplier = 0

fig, (ax1, ax2, ax3) = plt.subplots(3, layout='constrained', figsize=(16,12))

for compressor, results in data_ratios.items():
    offset = width * multiplier
    rects = ax1.bar(x + offset, tuple(results), width, label=compressor)
    ax1.bar_label(rects, padding=3)
    multiplier += 1

ax1.set_ylabel('Compression (%)')
ax1.set_title('Compression Size')
ax1.set_xticks(x + width, compression_types)
ax1.legend(loc='upper left', ncols=6)
ax1.set_ylim(0, 100)

multiplier = 0
for compressor, results in data_speeds_cmp.items():
    offset = width * multiplier
    rects = ax2.bar(x + offset, tuple(results), width, label=compressor)
    ax2.bar_label(rects, padding=3)
    multiplier += 1

ax2.set_ylabel('Compression (ms)')
ax2.set_title('Compression Speed')
ax2.set_xticks(x + width, compression_types)
ax2.legend(loc='upper left', ncols=6)
ax2.set_ylim(0, 100)

multiplier = 0
for compressor, results in data_speeds_dcmp.items():
    offset = width * multiplier
    rects = ax3.bar(x + offset, tuple(results), width, label=compressor)
    ax3.bar_label(rects, padding=3)
    multiplier += 1

ax3.set_ylabel('Decompression (ms)')
ax3.set_title('Decompression Speed')
ax3.set_xticks(x + width, compression_types)
ax3.legend(loc='upper left', ncols=6)
ax3.set_ylim(0, 30)

plt.show()