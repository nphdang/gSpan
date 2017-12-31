using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSpan
{
    class Graph
    {
        public int id { get; set; }
        public int support { get; set; }
        public List<Node> nodes { get; set; }        
        public List<DFS_Code> edges { get; set; }
    }
}
