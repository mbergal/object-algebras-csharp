namespace VisitorMod {
    interface IntAlg<A> {
        A lit(int x);
        A add(A e1, A e2);
        A mul(A e1, A e2);
    }

    interface Exp {
        A accept<A>(IntAlg<A> vis);
    }

    class Add : Exp {
        readonly Exp left;
        readonly Exp right;

        public Add(Exp left, Exp right) {
            this.left = left;
            this.right = right;
        }

        public A accept<A>(IntAlg<A> vis) => vis.add(left.accept(vis), right.accept(vis));
    }

    class Lit : Exp {
        private readonly int value;

        public Lit(int i) => this.value = i;
        public A accept<A>(IntAlg<A> vis) => vis.lit(this.value);
    }

    class Mul : Exp {
        readonly Exp left;
        readonly Exp right;

        public Mul(Exp left, Exp right) {
            this.left = left;
            this.right = right;
        }

        public A accept<A>(IntAlg<A> vis) => vis.mul(left.accept(vis), right.accept(vis));
    }

    public class Eval : IntAlg<int> {
        public int lit(int x) => x;
        public int add(int e1, int e2) => e1 + e2;
        public int mul(int e1, int e2) => e1 * e2;
    }


    public class Print : IntAlg<string> {
        public string lit(int x) => $"{x}";
        public string add(string e1, string e2) => $"{e1} + {e2}";
        public string mul(string e1, string e2) => $"{e1} * {e2}";
    }


    public class Test {
        public static void VisitorModMain() {
            var e = new Add(new Lit(10), new Lit(20));
            var v = e.accept(new Eval());
            var s = e.accept(new Print());
        }
    }
}

/*
@startuml

Exp <|-- Add
Exp <|-- Lit
Exp <|-- Mul
IntAlg  <|-- Eval 
IntAlg <|-- Print  

class IntAlg<A> {
    A lit(int x);
    A add(A e1, A e2);
    <color:#blue>A mul(A e1, A e2);
}

class Eval {
    int lit(int x);
    int add(int e1, int e2);
    <color:#blue>int mul(int e1, int e2);
}

class Print {
    string lit(int x);
    string add(string e1, string e2);
    <color:#blue>string mul(string e1, string e2);
}

interface Exp {
    A accept<A>(IntAlg<A> vis);
}

class Add {
    A accept<A>(IntAlg<A> vis);
}

class Lit {
    A accept<A>(IntAlg<A> vis);
}

class Mul {
    A accept<A>(IntAlg<A> vis);
}

@enduml

*/