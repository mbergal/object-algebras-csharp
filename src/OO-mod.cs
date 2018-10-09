using System;

namespace OOModifications {
    public interface Value {
        int getInt();
        bool getBool();
    };

    interface Exp {
        Value eval();
        string print();
    }

    public class VInt : Value {
        public VInt(int i) => this.value = i;
        public int getInt() => this.value;
        bool Value.getBool() => throw new NotImplementedException();
        private readonly int value;
    }

    public class VBool : Value {
        public VBool(bool value) => this.value = value;
        public int getInt() => throw new NotImplementedException();
        public bool getBool() => this.value;
        private readonly bool value;
    }


    public class Lit : Exp {
        public Lit(int x) => this.x = x;
        public Value eval() => new VInt(x);
        public string print() => $"{this.x}";
        readonly int x;
    }

    internal class Add : Exp {
        readonly Exp l;
        readonly Exp r;

        public Add(Exp l, Exp r) {
            this.l = l;
            this.r = r;
        }

        public Value eval() {
            return new VInt(l.eval().getInt() + r.eval().getInt());
        }

        public string print() {
            return $"{l.print()} + {r.print()}";
        }
    }


    public class Test {
        public static void OOModMain() {
            var e = new Add(new Lit(10), new Lit(20));
            var v = e.eval();
            var s = e.print();
        }
    }
}

/*
@startuml
Exp <|-- Lit
Exp <|-- Add
Add o-- Exp

Value <|-- VInt
Value <|-- VBool

interface Exp {
    Value eval()   
    <color:#blue> string print()  
}

interface Value {
    int getInt()
    bool getBool()  
}

class VInt {
    int getInt()
}


class VBool {
    int getBool()
}

class Lit {
    Value eval()
    <color:#blue> string print()    
}

class Add {
    Value eval()
    <color:#blue> string print()
}

@enduml

*/
