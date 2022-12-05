using CsvLinq;
namespace CsvLinq.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using var stream = new System.IO.FileStream("test.csv", System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            var csv = new CsvContext(stream);
        }
    }
}