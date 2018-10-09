namespace ObjectAlgebra {
    public interface IntAlg<A> {
        A lit(int x);
        A add(A e1, A e2);
    }

    public interface IEval {
        int eval();
    }

    public interface IPrint {
        string print();
    }

    public class IntFactory : IntAlg<IEval> {
        class Lit : IEval {
            public Lit(int i) => this.value = i;
            public int eval() => this.value;
            private int value;
        }

        class Add : IEval {
            public Add(IEval e1, IEval e2) {
                this.e1 = e1;
                this.e2 = e2;
            }

            public int eval() => this.e1.eval() + this.e2.eval();
            readonly IEval e1;
            readonly IEval e2;
        }

        public IEval lit(int x) => new Lit(x);
        public IEval add(IEval e1, IEval e2) => new Add(e1, e2);
    }


    class PrintFactory : IntAlg<IPrint> {
        class Lit : IPrint {
            public Lit(int i) => this.value = i;
            public string print() => this.value.ToString();
            private int value;
        }

        class Add : IPrint {
            public Add(IPrint e1, IPrint e2) {
                this.e1 = e1;
                this.e2 = e2;
            }

            public string print() => this.e1.print() + " + " + this.e2.print();
            readonly IPrint e1;
            readonly IPrint e2;
        }

        public IPrint lit(int x) => new Lit(x);
        public IPrint add(IPrint e1, IPrint e2) => new Add(e1, e2);


        public class Test {
            public static A exp<A>(IntAlg<A> v) {
                return v.add(v.lit(3), v.lit(4));
            }

            public static void VisitorModMain() {
                void test() {
                    IntFactory b = new IntFactory();
                    PrintFactory printFactory = new PrintFactory();
                    int x = exp(b).eval(); // int x = exp.eval();
                }
            }
        }
    }
}

/*
@startuml

class IntAlg<A> {
    A lit(int x);
    A add(A e1, A e2);
}

interface IEval {
    int eval();
}

interface IPrint {
    string print();
}

IntAlg <|-- IntFactory
IntAlg <|-- PrintFactory 

IEval o-- IntFactory 	

class IntFactory  {
    IEval lit(int x)
    IEval add(IEval e1, IEval e2)
}

IPrint o-- PrintFactory

class PrintFactory  {
    IPrint lit(int x)
    IPrint add(IPrint e1, IPrint e2)
}

@enduml

*/
