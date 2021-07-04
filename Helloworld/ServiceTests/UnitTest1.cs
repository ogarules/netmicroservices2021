using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace ServiceTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCalculatorServiceAddTwonumbers()
        {
            var service = new CalculatorService();
            Assert.AreEqual(service.Add2(1, 4), service.Add(1, 4));
        }
    }
}
