using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public abstract class EntityKey
    {
        public int Hash()
        {
            return this.GetHashCode();
        }
    }
}
