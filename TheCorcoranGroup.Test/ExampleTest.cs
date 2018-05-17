using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheCorcoranGroup.DLL;

namespace TheCorcoranGroup.Test
{
    [TestClass]
    public class ExampleTest
    {
        [TestMethod]
        public void TestContainAllResult()
        {
            var obj = new Example();
            var list = obj.Execute();
            Assert.IsTrue(list.Any(c => c == "Fizz"));
            Assert.IsTrue(list.Any(c => c == "Buzz"));
            Assert.IsTrue(list.Any(c=>c == "FizzBuzz"));
            Assert.IsTrue(list.Any(c => c == "2"));
            Assert.AreEqual(100, list.Count());
        }

        [TestMethod]
        public void TestContainCorrectResult()
        {
            var obj = new Example();
            var list = obj.Execute().ToArray();
            for (int i = 0; i < list.Count(); i++)
            {
                var result = list[i];
                var isMultiplesOfThree = (i+1) % 3 == 0;
                var isMultiplesOfFive = (i+1) % 5 == 0;
                if (isMultiplesOfThree && isMultiplesOfFive)
                {
                    if (result != "FizzBuzz")
                        Assert.IsTrue(false);
                }
                else if (isMultiplesOfThree)
                {
                    if (result != "Fizz")
                        Assert.IsTrue(false);
                }
                else if (isMultiplesOfFive)
                {
                    if (result != "Buzz")
                        Assert.IsTrue(false);
                }
            }
        }
    }
}
