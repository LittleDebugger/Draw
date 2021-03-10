using Draw.Console.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Draw.Console.Tests
{
    [TestClass]
    public class ConsoleInputDataTests
    {
        [TestMethod]
        public void ConsoleInputData_ParsesInputCorrectly()
        {
            var subject = new ConsoleInputContext("a 1 2 c");

            Assert.AreEqual("A", subject.Command);

            Assert.AreEqual(4, subject.InputParts.Length);
            Assert.AreEqual("a", subject.InputParts[0]);
            Assert.AreEqual("1", subject.InputParts[1]);
            Assert.AreEqual("2", subject.InputParts[2]);
            Assert.AreEqual("c", subject.InputParts[3]);
        }
    }
}