using System;

namespace Services
{
    public class CalculatorService
    {
        public int Add(int num1, int num2)
        {
            return num1 + num2;
        }

        public int Add2(int num1, int num2) => num1 + num2;
    }
}
