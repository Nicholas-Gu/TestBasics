using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBasics
{
    class TestLambda
    {
        private int _field = 5;

        public TestLambda()
        {
            int localVar = 4;
            Func<int, int> F3 = OneInOneOutWithLacalVar;
            Func<int, int> F4 = (x) =>
            {
                return x + localVar;
            };

            int ret1 = F3(5);
            int ret2 = F4(5);
            localVar = 7;
            int ret3 = F3(5);
            int ret4 = F4(5);
        }


        private int OneInOneOutWithLacalVar(int x)
        {
            return x + 5; // 变量未定义
        }


        private int OneInOneOutWithField(int x)
        {
            return x + _field;
        }


        static void Main(string[] args)
        {
            new TestLambda();

            Console.ReadKey();
        }
    }
}
