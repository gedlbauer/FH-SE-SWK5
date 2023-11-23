using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    delegate void Procedure();
    delegate void ProcedureWithStringParam(string value);
    delegate void ProcedureWithIntParam(int value);
    delegate void GenericProcedure<T>(T value);

    delegate int IntFunction();
    delegate int StringToIntFunction(string value);
    public class Introduction
    {
        public static void Test()
        {
            static void SayHello() => Console.WriteLine("Hello");
            static void SayHelloTo(string value) => Console.WriteLine($"Hello {value}");
            static void PrintInteger(int value) => Console.WriteLine($"Value: {value}");

            Procedure p1;
            p1 = SayHello;
            p1();

            Procedure p2 = null;
            p2?.Invoke();

            ProcedureWithStringParam p3 = SayHelloTo;
            p3("Herbert");

            ProcedureWithIntParam p4 = PrintInteger;
            p4(4);

            ProcedureWithStringParam p5 = Console.WriteLine;
            p5("Hobidere");

            ProcedureWithIntParam p6 = x => Console.WriteLine(x);
            p6(6);

            GenericProcedure<int> p7 = PrintInteger;
            p7(7);

            GenericProcedure<string> p8 = SayHelloTo;
            p8("p8");

            IntFunction f1 = new Random().Next;
            PrintInteger(f1());

            StringToIntFunction f2 = int.Parse;
            PrintInteger(f2("123"));
        }
    }
}
