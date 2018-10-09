using System;
using ObjectAlgebras.Confree;

public interface App<C, A> {
}

interface IConfigAlgebra<A> {
    App<A, int> Integer(string field);
    App<A, bool> Flag(string field);
    App<A, int> Port(string field);
    App<A, string> Server(string field);
    App<A, string> File(string field);
    App<A, (T1, T2)> Add<T1, T2>(App<A, T1> a1, App<A, T2> a2);
    App<A, int> Sub(string field, A config);
}


public static class DecodeExt {
    public static R apply<T, R>(this DecodeJson<T> d, Func<T, R> f) {
        return f(d.Read());
    }

    public static R apply<T, T1, R>(this DecodeJson<(T,T1)> d, Func<T, T1, R> f) {
        return f(d.t.Item1, d.t.Item2);
    }
    
    public static R apply<T, T1, T2, R>(this DecodeJson<(T,(T1,T2))> d, Func<T, T1, T2, R> f) {
        
        return f(d.t.Item1, d.t.Item2.Item1, d.t.Item2.Item2);
    }
    
}
public class DecodeJsonF {
}

public class G : IConfigAlgebra<DecodeJsonF> {
    public App<DecodeJsonF, int> Integer(string field) {
        return new DecodeJson<int>(field);
    }

    public App<DecodeJsonF, bool> Flag(string field) {
        return new DecodeJson<bool>(field);
    }

    public App<DecodeJsonF, int> Port(string field) {
        return new DecodeJson<int>(field);
    }

    public App<DecodeJsonF, string> Server(string field) {
        return new DecodeJson<string>(field);
    }

    public App<DecodeJsonF, string> File(string field) {
        throw new NotImplementedException();
    }

    public App<DecodeJsonF, (T1, T2)> Add<T1, T2>(App<DecodeJsonF, T1> a1, App<DecodeJsonF, T2> a2) {
        return ((DecodeJson<T1>) a1).Add((DecodeJson<T2>) a2);
    }

    public App<DecodeJsonF, int> Sub(string field, DecodeJsonF config) {
        throw new NotImplementedException();
    }
}

class AuthConfig {
    AuthConfig(int port, int a, string host) {
    }

    public static AuthConfig Make(int port, int a, string host) {
        return new AuthConfig(port, a, host);
    }
}

//
//class ServerConfig {
//    private ServerConfig(bool logging, AuthConfig auth) {
//    }
//
//    public static ServerConfig Make(bool logging, AuthConfig auth) {
//        return new ServerConfig(logging, auth);
//    }
//
//    public static void a() {
//        var a = curry<bool, AuthConfig, ServerConfig>(ServerConfig.Make);
//        var b = a(true);
//    }
//}
//
static class ExampleConfig {
    public static App<A, (int, (int, string))> AuthConfig<A>(IConfigAlgebra<A> a) {
        return a.Add(a.Integer("aaa"), a.Add(a.Port("port"), a.Server("host")));
    }

}


public static class Program {
    public static void Main() {
        var a = ExampleConfig.AuthConfig(new G());
        var b = (DecodeJson<ValueTuple<int, ValueTuple<int, string>>>) a;
        var d = new Func<int, int, string, AuthConfig>(AuthConfig.Make);
        var c = b.apply(d);
    }
}

