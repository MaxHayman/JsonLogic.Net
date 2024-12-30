using System.Text.Json.Nodes;

namespace JsonLogic.Net
{
    public interface IProcessJsonLogic
    {
        object Apply(JsonNode rule, object data);
    }
}
