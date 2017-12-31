using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace gSpan
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        int FrequentSubgraphs = 0;
        //List<Graph> result = null; // NEW

        private void btBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDataset.Text = openFileDialog1.FileName;
            }
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            string _sFile = txtDataset.Text.Trim();
            readDataset(_sFile);
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<Graph> readDataset(string _sFile)
        {
            List<Graph> D = new List<Graph>();
            string line = "";
            using (StreamReader sr = File.OpenText(_sFile))
            {
                Graph G = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("t")) // graph
                    {
                        G = new Graph();
                        G.nodes = new List<Node>();
                        G.edges = new List<DFS_Code>();
                        // add graph to dataset
                        D.Add(G);
                        G.id = int.Parse(line.Split()[2]);
                    }
                    else if (line.Contains("v")) // vertex
                    {
                        if (G != null)
                        {
                            Node node = new Node();
                            node.id = int.Parse(line.Split()[1]);
                            node.label = int.Parse(line.Split()[2]);

                            G.nodes.Add(node);
                        }
                    }
                    else if (line.Contains("e")) // edge
                    {
                        if (G != null)
                        {
                            DFS_Code code = new DFS_Code();
                            code.u = int.Parse(line.Split()[1]);
                            code.v = int.Parse(line.Split()[2]);
                            code.l_u = G.nodes[code.u].label;
                            code.l_v = G.nodes[code.v].label;
                            code.l_w = int.Parse(line.Split()[3]);
                            code.support = 1;
                            code.GraphID = G.id;

                            G.edges.Add(code);
                        }
                    }
                }
            }

            return D;
        }
                   
        void printDataset(Graph G, string file)
        {
            using (StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine("t # " + G.id);
                foreach (Node node in G.nodes)
                {
                    sw.WriteLine("v " + node.id + " " + node.label);
                }
                foreach (DFS_Code edge in G.edges)
                {
                    sw.WriteLine("e " + edge.u + " " + edge.v + " " + edge.l_w);
                }
            }
        }
                
        private void btMine_Click(object sender, EventArgs e)
        {
            FrequentSubgraphs = 0;
            //result = new List<Graph>();
            List<DFS_Code> C = new List<DFS_Code>();
            string _sFile = txtDataset.Text.Trim();

            Stopwatch sw = Stopwatch.StartNew();
            List<Graph> D = readDataset(_sFile);
            double m = (double.Parse(txtMinSup.Text.Trim()) * D.Count()) / 100;
            double minSup = (int)m;
            if (Math.Abs(minSup - m) >= 0.5)
            {
                minSup++;
            }
            if (minSup < 1)
            {
                minSup = 1;
            }
            TreeNode tnode = tvResult.Nodes.Add(_sFile + " (minSup = " + txtMinSup.Text.Trim() + "%)");
            gSpan(C, D, minSup);
            sw.Stop();
            long time = sw.ElapsedMilliseconds;
            tnode.Nodes.Add("Frequent Subgraphs: " + FrequentSubgraphs + ". Mining time: " + time / 1000.0 + " (s).");
            
            // NEW            
            // print result
            //string file = "gSpan_Dang.txt";
            //if (File.Exists(file))
            //{
            //    File.Delete(file);
            //}
            //for (int i = 0; i < result.Count(); i++)
            //{
            //    Graph G = result[i];
            //    G.id = i;
            //    printGraph(G, file);
            //}
            // NEW
        }    
                            
        void gSpan(List<DFS_Code> C, List<Graph> D, double minSup)
        {                                
            List<DFS_Code> extensions = RightMostPath_Extensions(C, D); // get extensions of graph G(C)

            foreach (DFS_Code t in extensions)
            {
                // deep copy (clone) C to C1
                List<DFS_Code> C1 = new List<DFS_Code>(C); 
                C1.Add(t); // generate a new graph by adding an extension to the old graph                           
                
                if (t.support >= minSup && IsCanonical(C1, t.support)) // support of the new graph is support of extension t
                {
                    FrequentSubgraphs++;                    
                    gSpan(C1, D, minSup);                    
                }
            }
        }

        List<DFS_Code> RightMostPath_Extensions(List<DFS_Code> C, List<Graph> D)
        {
            List<Node> R = null;
            Node ur = null;

            if (C.Count > 0)
            {
                // find nodes on the rightmost path in C
                R = getNodesOnRightMostPath(C);
                // get DFS Code of the rightmost child in C
                ur = R[0];
            }            

            List<DFS_Code> extensions = new List<DFS_Code>();

            foreach (Graph G in D)
            {
                if (C.Count == 0) // root node
                { 
                    // add distinct label tuples in G as forward extensions
                    foreach (DFS_Code dfs in G.edges)
                    {
                        DFS_Code f = new DFS_Code() { u = 0, v = 1, l_u = dfs.l_u, l_v = dfs.l_v, l_w = dfs.l_w, support = 1, GraphID = G.id };
                        if (!extensions.Contains(f)) // extensions do not contain f yet!
                        {
                            extensions.Add(f);
                        }

                        // NEW CODE
                        f = new DFS_Code() { u = 0, v = 1, l_u = dfs.l_v, l_v = dfs.l_u, l_w = dfs.l_w, support = 1, GraphID = G.id };
                        if (!extensions.Contains(f)) // extensions do not contain f yet!
                        {
                            extensions.Add(f);
                        }                        
                    }
                }
                else
                {
                    List<Isomorphism> iso = SubGraphIsomorphisms(C, G);
                    foreach (Isomorphism o in iso)
                    { 
                        // backward extensions from the rightmost child
                        Node node_ur = new Node() { id = o.map[ur.id], label = ur.label }; // node ur in G
                        foreach (Neighbor x in getNeighbors(node_ur, G))
                        {
                            Node node_v = new Node(); // node v is a mapping of node x in C
                            //node_v.id = o.map.FirstOrDefault(a => a.Value == x.id).Key;
                            node_v.id = -1;
                            foreach (var kv in o.map)
                            {
                                if (kv.Value == x.id)
                                {
                                    node_v.id = kv.Key;
                                    break;
                                }
                            }
                            node_v.label = x.label;
                            if (node_v.id != -1) // node v is existing in C
                            {
                                // rightmost path contains node v && edge (ur, v) is new in C                                
                                if (R.Contains(node_v) && !checkEdge(ur.id, node_v.id, C)) 
                                {
                                    DFS_Code f = new DFS_Code() { u = ur.id, v = node_v.id, l_u = ur.label, l_v = node_v.label, l_w = x.edge, support = 1, GraphID = G.id };
                                    if (!extensions.Contains(f)) // do not add duplicate tupes
                                    {
                                        extensions.Add(f);
                                    }                                    
                                }
                            }
                        }

                        // forward extensions from nodes on rightmost path
                        foreach (Node u in R)
                        {
                            Node node_u = new Node() { id = o.map[u.id], label = u.label }; // node u in G
                            foreach (Neighbor x in getNeighbors(node_u, G))
                            {
                                // node v is a mapping of node x in C
                                //int v = o.map.FirstOrDefault(a => a.Value == x.id).Key;
                                int v = -1;
                                foreach (var kv in o.map)
                                {
                                    if (kv.Value == x.id)
                                    {
                                        v = kv.Key;
                                        break;
                                    }
                                }
                                if (v == -1)
                                {
                                    DFS_Code f = new DFS_Code() { u = u.id, v = ur.id + 1, l_u = node_u.label, l_v = x.label, l_w = x.edge, support = 1, GraphID = G.id };
                                    if (!extensions.Contains(f)) // do not add duplicate tupes
                                    {
                                        extensions.Add(f);
                                    }                                    
                                }
                            }
                        }
                    }
                }
            }

            // in extensions, there are no duplicate tupes in the same graph 
            // compute the support of each extension            
            for (int i = 0; i < extensions.Count() - 1; i++)
            {
                DFS_Code s = extensions[i];
                for (int j = i + 1; j < extensions.Count(); j++)
                {
                    DFS_Code t = extensions[j];

                    if (s.u == t.u && s.v == t.v && s.l_u == t.l_u && s.l_v == t.l_v && s.l_w == t.l_w)
                    {
                        s.support += 1;
                        extensions.RemoveAt(j);
                        j--;
                    }                    
                }
            }

            // sort extensions
            for (int i = 0; i < extensions.Count() - 1; i++)
            {                
                for (int j = i + 1; j < extensions.Count(); j++)
                {                                                          
                    if (extensions[j].LessThan(extensions[i]))
                    {
                        DFS_Code code = extensions[i];
                        extensions[i] = extensions[j];
                        extensions[j] = code;
                    }
                }
            }

            return extensions;
        }

        List<Isomorphism> SubGraphIsomorphisms(List<DFS_Code> C, Graph G)
        {
            List<Isomorphism> iso = new List<Isomorphism>();
            // map vertex 0 in C to vertex x in G if label(0)=label(x)
            foreach (Node node in G.nodes)
            {
                if (node.label == C[0].l_u)
                {
                    Isomorphism o = new Isomorphism() { map = new Dictionary<int, int>() };
                    o.map.Add(0, node.id);
                    iso.Add(o);
                }
            }

            foreach (DFS_Code t in C)
            {
                List<Isomorphism> iso1 = new List<Isomorphism>();
                foreach (Isomorphism o in iso)
                {                    
                    Node node_u = new Node() { id = o.map[t.u], label = t.l_u }; // node u in G    
                    List<Neighbor> neighbors_node_u = getNeighbors(node_u, G);             

                    if (t.v > t.u) // forward edge
                    {
                        //Node node_u = new Node() { id = o.map[t.u], label = t.l_u }; // node u in G

                        //foreach (Neighbor x in getNeighbors(node_u, G))
                        foreach (Neighbor x in neighbors_node_u)
                        {
                            if (!o.map.ContainsValue(x.id) && x.label == t.l_v && x.edge == t.l_w)
                            {
                                // copy o to o1
                                Isomorphism o1 = new Isomorphism() { map = new Dictionary<int, int>(o.map) };
                                o1.map.Add(t.v, x.id);
                                // add o1 to iso1
                                iso1.Add(o1);
                            }                            
                        }
                    }
                    else // backward edge
                    {
                        bool isNeighbor = false;
                        Node node_v = new Node() { id = o.map[t.v], label = t.l_v }; // node v in G         
                        // check if node_v is a neighbor of node_u in G
                        //foreach (Neighbor x in getNeighbors(node_u, G))
                        foreach (Neighbor x in neighbors_node_u)
                        {
                            if (node_v.id == x.id && node_v.label == x.label)
                            {
                                isNeighbor = true;
                                break;
                            } 
                        }
                                                                                   
                        if (isNeighbor)
                        {
                            iso1.Add(o); // valid isomorphism
                        }
                    }                    
                }
                // replace iso by iso1                
                iso.Clear();
                iso.AddRange(iso1);
            }

            return iso;
        }

        bool IsCanonical(List<DFS_Code> C, int support)
        {        
            Graph GC = new Graph(); // graph corresponding to code C
            GC.id = -1;
            GC.support = support;
            GC.nodes = new List<Node>();
            GC.edges = new List<DFS_Code>(C);
            foreach (DFS_Code code in C)
            {
                Node node_u = new Node() { id = code.u, label = code.l_u };
                if (!GC.nodes.Contains(node_u))
                {
                    GC.nodes.Add(node_u);
                }
                Node node_v = new Node() { id = code.v, label = code.l_v };
                if (!GC.nodes.Contains(node_v))
                {
                    GC.nodes.Add(node_v);
                }
            }

            List<Graph> DC = new List<Graph>();
            DC.Add(GC);

            List<DFS_Code> C1 = new List<DFS_Code>(); // initialize canonical DFS code
            foreach (DFS_Code t in C)
            {
                List<DFS_Code> extensions = RightMostPath_Extensions(C1, DC); // extensions of C1
                // get least righmost edge extension of C1
                DFS_Code s = extensions[0];
                if (s.LessThan(t))
                {
                    return false; // C1 is smaller, thus C is not canonical
                }
                C1.Add(s);
            }
            //result.Add(GC); // NEW
            return true; // no smaller code exists; C is canonical
        }

        List<Node> getNodesOnRightMostPath(List<DFS_Code> C)
        {
            List<Node> nodes = new List<Node>(); // result
            List<Node> rmp = new List<Node>(); // rightmost path
            // create an empty rightmost path
            for (int i = 0; i < C.Count(); i++)
            {
                rmp.Add(new Node());
            }
            int rmp_len = 0; // rightmost path length
            foreach (DFS_Code dfs in C)
            {
                // consider only forward edges
                if (dfs.v > dfs.u && dfs.v > rmp[dfs.u].id)
                {
                    rmp[dfs.u].id = dfs.v;
                    rmp[dfs.u].label = dfs.l_v;
                    rmp_len = dfs.u;
                }
            }

            for (int i = rmp_len; i >= 0; i--)
            {
                nodes.Add(new Node() { id = rmp[i].id, label = rmp[i].label });
            }

            // add the first node
            nodes.Add(new Node() { id = C[0].u, label = C[0].l_u });

            return nodes;
        }               

        bool checkEdge(int x, int y, List<DFS_Code> C)
        {
            foreach (DFS_Code code in C)
            {
                if ((x == code.u && y == code.v) || (x == code.v && y == code.u))
                {
                    return true;
                }                
            }

            return false;
        }
             
        List<Neighbor> getNeighbors(Node node, Graph G)
        {
            List<Neighbor> neighbors = new List<Neighbor>();

            foreach (DFS_Code edge in G.edges)
            {
                if (node.id == edge.u && node.label == edge.l_u) // node is u ==> its neighbor is v
                {
                    neighbors.Add(new Neighbor() { id = edge.v, label = edge.l_v, edge = edge.l_w });
                }
                else if (node.id == edge.v && node.label == edge.l_v) // node is v ==> its neighbor is u
                {
                    neighbors.Add(new Neighbor() { id = edge.u, label = edge.l_u, edge = edge.l_w });
                }
            }

            return neighbors;
        }             
    }
}
