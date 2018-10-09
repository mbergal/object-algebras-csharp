using System;

namespace ObjectAlgebraMod {
    public interface IntAlg<A> {
        A lit(int x);
        A add(A e1, A e2);
    }

    public interface Exp {
        int eval();
    }

    public interface IPrint {
        string print();
    }

    class Bool : Exp {
        private bool b;

        public Bool(Boolean b) => this.b = b;

        public int eval() {
            return this.b;
        }
    }

    class Iff : Exp {
        private readonly Exp e1;
        private readonly Exp e2;
        private readonly Exp e3;

        public Iff(Exp e1, Exp e2, Exp e3) {
            this.e1 = e1;
            this.e2 = e2;
            this.e3 = e3;
        }

        public int eval() {
            return this.e1.eval() > 0 ? this.e2.eval() : this.e3.eval();
        }
    }

    public class IntFactory : IntAlg<Exp> {
        class Lit : Exp {
            public Lit(int i) => this.value = i;
            public int eval() => this.value;
            private int value;
        }

        class Add : Exp {
            public Add(Exp e1, Exp e2) {
                this.e1 = e1;
                this.e2 = e2;
            }

            public int eval() => this.e1.eval() + this.e2.eval();
            readonly Exp e1;
            readonly Exp e2;
        }

        public Exp lit(int x) => new Lit(x);
        public Exp add(Exp e1, Exp e2) => new Add(e1, e2);
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

    class Print2Factory : IntAlg<string> {
        public string lit(int x) => x.ToString();
        public string add(string e1, string e2) => e1 + " + " + e2;
    }

    interface IntBoolAlg<A> : IntAlg<A> {
        A boolean(bool b);
        A iff(A e1, A e2, A e3);
        
    }
    
    class IntBoolFactory : IntFactory, IntBoolAlg<Exp> {
        public Exp boolean(Boolean b) {return new Bool(b);}
        public Exp iff(Exp e1, Exp e2, Exp e3) {return new Iff(e1,e2,e3);
    }
        
    /* Extended Retroactive Implementation for Printing */
    class IntBoolPrint :  PrintFactory, IntBoolAlg<IPrint> {
    public IPrint boolean(Boolean b) {
        return new IPrint() {
            public String print() {return new Boolean(b).toString();}
        };
    }

        public IPrint iff(IPrint e1, IPrint e2, IPrint e3) {
        return new IPrint() {
            public String print() {
            return "if (" + e1.print() + ") then " + e2.print() + " else " + e3.
            print();
        }
        };
    }
    }
    
}

/*
@startuml

class IntAlg<A> {
    A lit(int x);
    A add(A e1, A e2);
}

Exp <|-- Bool 
Exp <|-- Iff

interface Exp {
    int eval();
}

interface IPrint {
    string print();
}

IntAlg <|-- IntFactory
IntAlg <|-- PrintFactory 

   	

class IntFactory  {
    Exp lit(int x)
    Exp add(IEval e1, IEval e2)
}


PrintFactory <|-- IntBoolPrint
IntBoolAlg <|--  IntBoolPrint

IntFactory <|-- IntBoolFactory
IntBoolAlg <|--  IntBoolFactory

class IntBoolAlg<A> {
    A boolean(bool b);
    A iff(A e1, A e2, A e3);
}

class PrintFactory  {
    IPrint lit(int x)
    IPrint add(IPrint e1, IPrint e2)
}

class IntBoolFactory {
    Exp boolean(Boolean b)
    Exp iff(Exp e1, Exp e2, Exp e3)
}
class IntBoolPrint {
    IPrint boolean(Boolean b)
    IPrint iff(IPrint e1, IPrint e2, IPrint e3)
}

@enduml

*/