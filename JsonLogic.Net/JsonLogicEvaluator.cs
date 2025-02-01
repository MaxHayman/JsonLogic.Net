using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace JsonLogic.Net
{
    public class JsonLogicEvaluator : IProcessJsonLogic
    {
        private readonly IManageOperators _operations;

        public JsonLogicEvaluator(IManageOperators operations)
        {
            _operations = operations;
        }

        public object Apply(JsonNode? rule, object data)
        {
            if (rule is null)
            {
                return null;
            }

            if (rule is JsonValue jsonValue)
            {
                return AdjustType(jsonValue.GetValue<JsonElement>());
            }

            if (rule is JsonArray jsonArray)
            {
                return jsonArray.Select(r => Apply(r, data)).ToArray();
            }

            var ruleObj = rule as JsonObject;

            if (ruleObj == null || !ruleObj.Any())
            {
                return null;
            }

            var p = ruleObj.First();
            var opName = p.Key;
            var opArgs = p.Value is JsonArray jsonArrayArgs ? jsonArrayArgs.ToArray() : new[] { p.Value };
            var op = _operations.GetOperator(opName);
            return op(this, opArgs, data);
        }

        private object? AdjustType(JsonElement jsonElement)
        {
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Number:
                    return jsonElement.GetDouble();
                case JsonValueKind.True:
                case JsonValueKind.False:
                    return jsonElement.GetBoolean();
                case JsonValueKind.Null:
                    return null;
                case JsonValueKind.String:
                default:
                    return jsonElement.GetString();
            }
        }
    }
}
