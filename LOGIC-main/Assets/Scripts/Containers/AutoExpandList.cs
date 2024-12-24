using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containers
{
    internal class AutoExpandList<T>:List<T>
    {
        public new T this[int index]
        {
            get
            {
                EnsureSize(index + 1);
                return base[index];
            }
            set
            {
                EnsureSize(index + 1);
                base[index] = value;
            }
        }
        private void EnsureSize(int size)
        {
            while (Count < size)
            {
                Add(default);
            }
        }
    }
}
