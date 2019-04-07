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
# Reference
Bay Vo, Dang Nguyen, Thanh-Long Nguyen (2015). A parallel algorithm for frequent subgraph mining. ICCSAMA 2015, Metz, France. Springer AISC, 358, 163-173.
