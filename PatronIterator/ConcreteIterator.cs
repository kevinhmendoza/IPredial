using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronIterator
{
    public class ConcreteIterator : Iterator

    {
        private ConcreteAggregate _aggregate;
        private int _actual = 0;

        // Constructor

        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            this._aggregate = aggregate;
        }

        // Gets first iteration item

        public override object Primero()
        {
            return _aggregate[0];
        }

        // Gets next iteration item

        public override object Siguiente()
        {
            object ret = null;
            if (_actual < _aggregate.Contador - 1)
            {
                ret = _aggregate[++_actual];
            }

            return ret;
        }

        // Gets current iteration item

        public override object ActualItem()
        {
            return _aggregate[_actual];
        }

        // Gets whether iterations are complete

        public override bool Termino()
        {
            return _actual >= _aggregate.Contador;
        }
    }
}
