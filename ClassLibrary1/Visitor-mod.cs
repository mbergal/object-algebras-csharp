
using System;
using OOModifications;

namespace E4
{
    interface IntAlg<A>
    {
        A lit(int x);
        A add(A e1, A e2);
    }

    interface Exp
    {
        A accept<A>(IntAlg<A> vis);
    }

    class Add : Exp
    {
        readonly Exp left;
        readonly Exp right;

        public Add(Exp left, Exp right)
        {
            this.left = left;
            this.right = right;
        }

        public A accept<A>(IntAlg<A> vis)
        {
            return vis.add(left.accept(vis), right.accept(vis));
        }
    }

    class Lit : Exp
    {
        private readonly int value;

        public Lit(int i)
        {
            this.value = i;
        }

        public A accept<A>(IntAlg<A> vis)
        {
            return vis.lit(this.value);
        }
    }

    public class Eval : IntAlg<int>
    {
        public int lit(int x)
        {
            return x;
        }

        public int add(int e1, int e2)
        {
            return e1 + e2;
        }
    } 

    public class Print : IntAlg<string>
    {
        public string lit(int x)
        {
            return $"{x}";
        }

        public string add(string e1, string e2)
        {
            return $"{e1} + {e2}";
        }
    } 


    public class Test
    {
        public static void Main()
        {
            var e = new Add(new Lit(10), new Lit(20));
            var v = e.accept(new Eval());
            var s = e.accept(new Print());
        }
    }
}