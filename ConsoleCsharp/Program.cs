using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Runtime.CompilerServices;

namespace TestApp
{
    class X
    {
        public int Value;
    }

    unsafe class Program
    {
        static ulong AsUnmanaged<T>(T r) where T : class => (ulong)Unsafe.As<T, IntPtr>(ref r);

        static ulong AsUnmanaged<T>(ref T r) => (ulong)Unsafe.AsPointer(ref r);

        static void Main()
        {
            void GenerateGarbage()
            {
                for (int i = 0; i < 1000000; i++)
                {
                    var dummy = new object();
                }
            }

            GenerateGarbage();

            var x = new X { Value = 12345678 };
            ref var r = ref x.Value;

            var addressOfX = AsUnmanaged(x);
            var addressOfValue = AsUnmanaged(ref r);

            WriteLine((addressOfX, addressOfValue));

            GenerateGarbage();

            GC.Collect(0, GCCollectionMode.Forced);
            WriteLine("---GC発生---");

            WriteLine("unmanaged " + (addressOfX, addressOfValue));

            WriteLine("managed " + (AsUnmanaged(x), AsUnmanaged(ref r)));

            fixed (int* p = &x.Value)
            {
                GenerateGarbage();
                GC.Collect(0, GCCollectionMode.Forced);
                WriteLine("---GC発生---");
                WriteLine("managed " + (AsUnmanaged(x), AsUnmanaged(ref r)));
            }

            UnsafeTest.Execute();

            ReadKey();
        }

    }
}

