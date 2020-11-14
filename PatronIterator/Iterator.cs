using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronIterator
{
    public abstract class Iterator

    {
        public abstract object Primero();
        public abstract object Siguiente();
        public abstract bool Termino();
        public abstract object ActualItem();
    }
}
