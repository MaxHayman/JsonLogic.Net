using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace JsonLogic.Net
{
    public class JsonLogicEvaluator : IProcessJsonLogic
    {
        private readonly IManageOperators _operations;

        public JsonLogicEvaluator(IManageOperators operations)
        {
            _operations = operations;
        }

        public object Apply(JToken rule, object data)
        {
            if (rule is null)
            {
                return null;
            }

            if (rule is JValue jValue)
            {
                return AdjustType(jValue.Value);
            }

            if (rule is JArray jArray)
            {
                return jArray.Select(r => Apply(r, data)).ToArray();
            }

            var ruleObj = (JObject) rule;
            var p = ruleObj.Properties().First();
            var opName = p.Name;
            var opArgs = p.Value is JArray pjArray ? pjArray.ToArray() : new[] { p.Value };
            var op = _operations.GetOperator(opName);
            return op(this, opArgs, data);
        }

        private object AdjustType(object value)
        {
            return value.IsNumeric() ? Convert.ToDouble(value) : value;
        }
    }
}
