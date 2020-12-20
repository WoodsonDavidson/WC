using System;
using System.Collections;
using System.Threading;
using System.Text;

namespace WoodsonsCalculator2020
{
    class Program
    {
        static void Main(string[] args)
        {//doesn't work: if same priority then i-1 move to outputStack
            while (true)
            {
                string inpStr = Console.ReadLine();
                //string inpStr = "15/3*15/5";
                ClassForMethods calc = new ClassForMethods(inpStr);
                Console.Clear();
                Console.WriteLine(inpStr + "=" + calc.Calculate());
            }
        }
    }
}
