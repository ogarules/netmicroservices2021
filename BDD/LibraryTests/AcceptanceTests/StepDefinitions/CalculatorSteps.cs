using LibraryApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace LibraryTests.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class CalculatorSteps
    {
        private readonly ScenarioContext context;
        public CalculatorSteps(ScenarioContext context)
        {
            this.context = context;
        }

        [Given("a first number (.*)")]
        public void GivenAFirstNumber(int num1)
        {
            this.context.Add("firstNumber", num1);
        }

        [Given("a second number (.*)")]
        public void GivenASecondNumber(int num1)
        {
            this.context.Add("secondNumber", num1);
        }

        [When("two numbers are (.*)")]
        public void WhenTwoNumberOperation(string operation)
        {
            var first = this.context.Get<int>("firstNumber");
            var second = this.context.Get<int>("secondNumber");

            var calculatorService = new CalculatorService();
            var result = operation == "added" ? calculatorService.Add(first, second) : calculatorService.Multiply(first, second);

            this.context.Add("calculatorResult", result);
        }

        [Then("the result must be (.*)")]
        public void TheResultMustBe(int result)
        {
            var operationResult = this.context.Get<int>("calculatorResult");

            Assert.AreEqual(result, operationResult);
        }
    }
}