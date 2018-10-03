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
        private readonly int value;

        public VInt(int i) {
            this.value = i;
        }

        public int getInt() {
            return this.value;
        }

        public bool getBool() {
            throw new NotImplementedException();
        }
    }

    public class VBool : Value {
        private readonly bool value;

        public VBool(bool value) {
            this.value = value;
        }

        public int getInt() {
            throw new NotImplementedException();
        }

        public bool getBool() {
            return this.value;
        }
    }


    public class Lit : Exp {
        readonly int x;

        public Lit(int x) {
            this.x = x;
        }

        public Value eval() {
            return new VInt(x);
        }
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
    }


    public class Test {
        public static void Main() {
            var e = new Add(new Lit(10), new Lit(20));
            var v = e.eval();
        }
    }
}