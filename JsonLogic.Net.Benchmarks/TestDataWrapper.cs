namespace JsonLogic.Net.Benchmarks;

public class TestDataWrapper
{
    private readonly TestData _testData;

    public TestDataWrapper(TestData testData)
    {
        _testData = testData;
    }

    public string PlayerName
    {
        get
        {
            // Console.WriteLine("Evaluated Name");
            return _testData.Name;
        }
    }

    public uint Level
    {
        get
        {
            //Console.WriteLine("Evaluated Number");
            return _testData.Level;
        }
    }

    public uint Career
    {
        get
        {
            //Console.WriteLine("Evaluated Career");
            return _testData.Career;
        }
    }
}
