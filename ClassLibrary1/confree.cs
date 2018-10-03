using System;

interface IConfigAlgebra<A> {
    A integer(string field);
    A flag(string field);
    A port(string field);
    A server(string field);
    A file(string field);
    A s(A[] p);
    A sub(string field, A config);
}

class GenHelp : IConfigAlgebra<string> {
    public string integer(string field) {
        throw new System.NotImplementedException();
    }

    public string flag(string field) {
        return $"{field}: flag";
    }

    public string port(string field) {
        return $"{field}: port";
    }

    public string server(string field) {
        return $"{field}: server";
    }

    public string file(string field) {
        throw new System.NotImplementedException();
    }

    public string s(string[] p) {
        return string.Join("\n", p);
    }

    public string sub(string field, string config) {
        return $"{field}\n{config}";
    }
}

class AuthConfig {
    AuthConfig(int port, string host) {
    }
}

class ServerConfig{
    ServerConfig(bool logging, AuthConfig auth) {}
}

static class ExampleConfig {
    static A authConfig<A>(IConfigAlgebra<A> a) {
        return a.s(new[] {
            a.port("port"),
            a.server("host")
        });
    }

    public static A serverConfig<A>(IConfigAlgebra<A> a) {
        return a.s(new[] {
            a.flag("logging"),
            a.sub("auth", authConfig(a))
        });
    }
}

public static class Program {
    public static void Main() {
        var help = ExampleConfig.serverConfig(new GenHelp());
        Console.Out.WriteLine(help);
    }
}

//object dsl {
//import algebra._
//
//type Dsl[A] = FreeAp[ConfigF, A]
//
//private def lift[A](value: ConfigF[A]): Dsl[A] = FreeAp.lift[ConfigF, A](value)
//
//        def int   (field: String): Dsl[Int]     = lift(ConfigInt   (field, identity))
//def flag  (field: String): Dsl[Boolean] = lift(ConfigFlag  (field, identity))
//def port  (field: String): Dsl[Int]     = lift(ConfigPort  (field, identity))
//def server(field: String): Dsl[String]  = lift(ConfigServer(field, identity))
//def file  (field: String): Dsl[String]  = lift(ConfigFile  (field, identity))
//def sub[A](field: String) 
//(value: Dsl[A])               = lift(ConfigSub   (field, value))
//}