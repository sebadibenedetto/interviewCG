using System.Collections.Generic;

namespace TheCorcoranGroup.DLL
{
    public class Example
    {
        public IEnumerable<string> Execute()
        {
            for (int i = 1; i <= 100; i++)
            {
                var isMultiplesOfThree = i % 3 == 0;
                var isMultiplesOfFive = i % 5 == 0;
                if (isMultiplesOfThree && isMultiplesOfFive)
                {
                    yield return "FizzBuzz";
                }
                else if(isMultiplesOfThree)
                {
                    yield return "Fizz";
                }
                else if (isMultiplesOfFive)
                {
                    yield return "Buzz";
                }
                else
                {
                    yield return i.ToString();
                }
            }
        }
    }
}
