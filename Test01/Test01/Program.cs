
using System;

namespace Test01
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b, c = 0;
            char x;
            a = int.Parse(Console.ReadLine());
            x = char.Parse(Console.ReadLine());
            b = int.Parse(Console.ReadLine());
            switch (x)
            {
                case '+': c = a + b; break;
                case '_': c = a - b; break;
                case '*': c = a * b; break;
                case '/':
                    if (b != 0)
                    {
                        c = a / b; break;
                    }
                    else
                    {

                        break;
                    }
                case '%':
                    if (b != 0)
                    {
                        c = a % b; break;
                    }
                    else
                    {
                        break;
                    }
            }
            Console.WriteLine(c);
        }

    }
}

