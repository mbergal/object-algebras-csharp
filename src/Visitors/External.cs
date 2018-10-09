namespace ExternalVisitor {
    public interface Tree {
        R Accept<R>(TreeVisitor<R> v);
    }

    public class Empty : Tree {
        public R Accept<R>(TreeVisitor<R> v) {
            return v.Empty();
        }
    }

    public class Fork : Tree {
        public Fork(int x, Tree l, Tree r) {
            this.x = x;
            this.l = l;
            this.r = r;
        }

        private readonly int x;
        private readonly Tree l;
        private readonly Tree r;
        
        public R Accept<R>(TreeVisitor<R> v) {
            return v.Fork(this.x, this.l, this.r);
        }
    }

    public interface TreeVisitor<R> {
        R Empty();
        R Fork(int x, Tree l, Tree r);
    }
}