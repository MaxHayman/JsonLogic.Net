using System;
using System.Text.Json.Nodes;

namespace JsonLogic.Net {
    public interface IManageOperators
    {
        void AddOperator(string name, Func<IProcessJsonLogic, JsonNode[], object, object> operation);

        Func<IProcessJsonLogic, JsonNode[], object, object> GetOperator(string name);

        void DeleteOperator(string name);
    }
}