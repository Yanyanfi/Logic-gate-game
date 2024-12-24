using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interface
{
    public interface IStateDisplayable
    {
        public int StateValue { get; }
        event EventHandler StateChanged;
    }
}
