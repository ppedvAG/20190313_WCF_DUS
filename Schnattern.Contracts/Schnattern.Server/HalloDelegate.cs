using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schnattern.Server
{
    public delegate void EinfacherDelegate();
    public delegate void DelegateMitPara(string text);
    public delegate long CalcDelegate(int a, int b);

    public class HalloDelegate
    {
        public event Action<object, EventArgs> AnyEvent;
        public event EventHandler AnyEvent2;

        public HalloDelegate()
        {
            EinfacherDelegate einDings = EinfacheMethode;
            Action einDingsAlsAction = EinfacheMethode;
            Action einDingsAlsActionAno = delegate () { Console.WriteLine("Hallo"); };
            Action einDingsAlsActionAno2 = () => { Console.WriteLine("Hallo"); };
            Action einDingsAlsActionAno3 = () => Console.WriteLine("Hallo");

            DelegateMitPara dingsMitPara = MethodeMitPara;
            Action<string> dingsMitParaAlsAction = MethodeMitPara;
            Action<string> dingsMitParaAlsActionAno = delegate (string txt) { Console.WriteLine(txt); };
            Action<string> dingsMitParaAlsActionAno2 = (string txt) => { Console.WriteLine(txt); };
            Action<string> dingsMitParaAlsActionAno3 = x => Console.WriteLine(x);

            CalcDelegate calc = Sum;
            Func<int, int, long> calcFunc = Sum;
            Func<int, int, long> calcFuncAno = (x, y) => { return x + y; };
            Func<int, int, long> calcFuncAno2 = (x, y) => x + y;

            var texte = new List<string>();
            texte.Where(Filter);
            texte.Where(x => x.StartsWith("b"));

        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        private void MethodeMitPara(string mmmmmsg)
        {
            Console.WriteLine(mmmmmsg);
        }

        private void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }
    }
}
