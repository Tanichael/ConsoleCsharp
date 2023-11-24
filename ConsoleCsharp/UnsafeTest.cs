using System;

namespace TestApp
{
    struct Point
    {
        public short X;
        public short Y;
    }

    public class UnsafeTest
    {
        public unsafe static void Execute()
        {
            var p = new Point();

            var pp = &p;

            int* pi = (int*)pp;
            *pi = 0x00010002;

            Console.WriteLine(pp->X);
            Console.WriteLine(pp->Y);

            byte* pb = (byte*)pp;

            Console.WriteLine(pb[0]);
            Console.WriteLine(pb[1]);
            Console.WriteLine(pb[2]);
            Console.WriteLine(pb[3]);
        }
    }
}

