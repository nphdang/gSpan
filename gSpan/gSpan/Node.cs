using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSpan
{
    class Node : IEquatable<Node>
    {
        public int id { get; set; }
        public int label { get; set; }

        public bool Equals(Node other)
        {
            if (this.id == other.id && this.label == other.label)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
