using BenchmarkDotNet.Attributes;
using JsonLogic.Net;
using Newtonsoft.Json.Linq;

namespace JsonLogic.Net.Benchmarks;

[MemoryDiagnoser(true)]
public class Benchmarks
{
    JsonLogicEvaluator evaluator = new JsonLogicEvaluator(EvaluateOperators.Default);
    private JObject rule;
    TestDataWrapper data   = new TestDataWrapper(new TestData{Name = "Dave", Level = 8});

    [GlobalSetup]
    public void Setup()
    {

        string jsonText="{\">\": [{\"var\": \"PlayerNumber\"}, 3]}";

        rule = JObject.Parse(jsonText);

    }

    [Benchmark]
    public void Test()
    {
        evaluator.Apply(rule, data);
    }
}
