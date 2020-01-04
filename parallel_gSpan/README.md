# Tool to mine frequent subgraphs
- File "fsg_miner.exe" can be used as a standalone tool to discover frequent subgraphs
- It runs fast since it is implemented in parallel
- Its source code is in "fsg_miner.zip"
- Its parameters are as follows:
```
        -graphset <file>
        use graphs from <file> to mine FSGs
        -graphlabel <file>
        obtain graph labels from <file>
        -minsup <float>
        set minimum support threshold in [0,1]; default is 0.5
        -fsg <file>
        save discovered FSGs to <file> (optional)
        -output <file>
        convert each graph to a set of FSGs and save it to <file> (optional)
```
# How to run
- Use Windows console and type the command, e.g.
```
fsg_miner.exe -graphset ./data/mutag/mutag_graph.txt -graphlabel ./data/mutag/mutag_label.txt -minsup 0.35 -fsg mutag_fsg.txt
```
- This command finds frequent subgraphs in the dataset "mutag" with the minimum support threshold of 0.35 (35%) and saves the discovered frequent subgraphs into the file "mutag_fsg.txt" in the current working folder.
- The console also summarizes the total number of frequent subgraphs found along with runtime in seconds.

![fsg_miner command](https://github.com/nphdang/gSpan/blob/master/fsg_miner_command.jpg)

# Reference
1. Dang Nguyen, Wei Luo, Tu Dinh Nguyen, Svetha Venkatesh, Dinh Phung (2018). Learning Graph Representation via Frequent Subgraphs. SDM 2018, San Diego, USA. SIAM, 306-314.

2. Bay Vo, Dang Nguyen, Thanh-Long Nguyen (2015). A parallel algorithm for frequent subgraph mining. ICCSAMA 2015, Metz, France. Springer AISC, 358, 163-173.
