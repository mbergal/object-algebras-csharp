namespace ObjectAlgebra {
    interface IntAlg<A> {
        A lit(int x);
        A add(A e1, A e2);
    }

    public class IntFactory : IntAlg<int> {
        public int lit(int x) {
            return x;
        }

        public int add(int e1, int e2) {
            return e1 + e2;
        }
    }

    public class Test {
        static A make3Plus5<A>(IntAlg<A> f) {
            return f.add(f.lit(3), f.lit(5));
        }


        public static void Main() {
            void test() {
                var e = make3Plus5(new IntFactory());
            }
        }
    }

    interface IPrint {
        string print();
    }

    class IntPrint : IntAlg<IPrint> {
        class Lit : IPrint {
            private int value;

            public Lit(int i) {
                this.value = i;
            }

            public string print() {
                return this.value.ToString();
            }
        }

        public IPrint lit(int x) {
            return new Lit(x);
        }

        public IPrint add(IPrint e1, IPrint e2) {
            return new Add(e1, e2);
        }

        class Add : IPrint {
            readonly IPrint e1;
            readonly IPrint e2;

            public Add(IPrint e1, IPrint e2) {
                this.e1 = e1;
                this.e2 = e2;
            }

            public string print() {
                return this.e1.print() + " + " + this.e2.print();
            }
        }
    }
}