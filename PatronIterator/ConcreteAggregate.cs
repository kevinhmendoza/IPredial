using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronIterator
{
    public class ConcreteAggregate : Aggregate

    {
        private ArrayList _items = new ArrayList();

        public override Iterator CrearIterator()
        {
            return new ConcreteIterator(this);
        }

        // Gets item count

        public int Contador
        {
            get { return _items.Count; }
        }

        // Indexer

        public object this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }
    }
}
