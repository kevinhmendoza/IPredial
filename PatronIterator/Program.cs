using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronIterator
{
    class Program
    {
        static void Main(string[] args)
        {
            var Items = new ConcreteAggregate();
            Items[0] = "Item A";
            Items[1] = "Item B";
            Items[2] = "Item C";
            Items[3] = "Item D";          

            var iteratorItems = Items.CrearIterator();
            Console.WriteLine("Iterando Sobre la colección de Items:");

            var item = iteratorItems.Primero();
            while (item != null)
            {
                Console.WriteLine(item);
                item = iteratorItems.Siguiente();
            }



            var numeros = new ConcreteAggregate();
            numeros[0] = 1;
            numeros[1] = 2;
            numeros[2] = 3;
            numeros[3] = 4;
            var iteratorNumeros = numeros.CrearIterator();
            Console.WriteLine("Iterando Sobre la colección de Numeros:");
            var itemsNumeros = iteratorNumeros.Primero();
            while (itemsNumeros != null)
            {
                Console.WriteLine(itemsNumeros);
                itemsNumeros = iteratorNumeros.Siguiente();
            }

           

            Console.ReadKey();
        }
    }
}
