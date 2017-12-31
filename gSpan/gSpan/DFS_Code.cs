using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSpan
{
    class DFS_Code : IEquatable<DFS_Code>
    {
        public int u { get; set; } // vertex u
        public int v { get; set; } // vertex v
        public int l_u { get; set; } // label of vertex u
        public int l_v { get; set; } // label of vertex v
        public int l_w { get; set; } // label of edge uv
        public int support { get; set; } // support of this DFS code (edge)
        public int GraphID { get; set; } // graph contains this DFS code (edge)

        public bool Equals(DFS_Code other)
        {
            if (this.u == other.u && this.v == other.v && this.l_u == other.l_u && this.l_v == other.l_v 
                && this.l_w == other.l_w && this.GraphID == other.GraphID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LessThan(DFS_Code other)
        {
            // compare labels of two edges
            if (this.u == other.u && this.v == other.v)
            {
                if (this.l_u < other.l_u)
                {
                    return true;
                }
                else if (this.l_u == other.l_u)
                {
                    if (this.l_v < other.l_v)
                    {
                        return true;
                    }
                    else if (this.l_v == other.l_v)
                    {
                        if (this.l_w < other.l_w)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            // compare two edges
            // condition 1: both edges are forward
            if (this.u < this.v && other.u < other.v)
            {
                if (this.v < other.v || (this.v == other.v && this.u > other.u))
                {
                    return true;
                }
            }

            // condition 2: both edges are backward
            if (this.u > this.v && other.u > other.v)
            {
                if (this.u < other.u || (this.u == other.u && this.v < other.v))
                {
                    return true;
                }
            }

            // condition 3: this edge is forward and the other is backward
            if (this.u < this.v && other.u > other.v)
            {
                if (this.v <= other.u)
                {
                    return true;
                }
            }

            // condition 4: this edge is backward and the other is forward
            if (this.u > this.v && other.u < other.v)
            {
                if (this.u < other.v)
                {
                    return true;
                }
            }

            return false;
        }        
    }
}
