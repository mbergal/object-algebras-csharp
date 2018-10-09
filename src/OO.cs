using System;

namespace OO {
    public interface Value {
        int getInt();
        bool getBool();
    };

    interface Exp {
        Value eval();
    }

    public class VInt : Value {
        public VInt(int i) { this.value = i; }
        public int getInt() => this.value;
        public bool getBool() => throw new NotImplementedException();
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
        readonly int x;
    }

    internal class Add : Exp {
        public Add(Exp l, Exp r) {
            this.l = l;
            this.r = r;
        }
        public Value eval() => new VInt(l.eval().getInt() + r.eval().getInt());
        readonly Exp l;
        readonly Exp r;
    }


    public class Test {
        public static void OOMain() {
            var e = new Add(new Lit(10), new Lit(20));
            var v = e.eval();
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
}

class Add {
    Value eval()
}

@enduml

*/
