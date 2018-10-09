using System;
using System.Json;

namespace ObjectAlgebras.Confree {
    public class DecodeJson<T> : App<DecodeJsonF, T> {
        private readonly string field;
        private readonly Func<JsonObject, T> func;

        public DecodeJson(string field, Func<JsonObject, T> func) {
            this.field = field;
            this.func = func;
        }

        public T Read(JsonObject jsonObject) {
            return this.func(jsonObject);
        }

        public DecodeJson<(T, T1)> Add<T1>(DecodeJson<T1> t2) {
            return new DecodeJson<(T, T1)>(null, (JsonObject o) => (this.Read(o), t2.Read(o)));
        }
    }
}