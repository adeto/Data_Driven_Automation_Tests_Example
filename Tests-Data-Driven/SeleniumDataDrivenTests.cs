using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Tests_Data_Driven
{
    class SeleniumDataDrivenTests
    {
        RemoteWebDriver driver;
        IWebElement textboxNum1;
        IWebElement textboxNum2;
        IWebElement dropdownOptions;
        IWebElement buttonCalculate;
        IWebElement buttonReset;
        IWebElement result;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://number-calculator.nakov.repl.co/");

            textboxNum1 = driver.FindElementById("number1");
            dropdownOptions = driver.FindElementById("operation");
            textboxNum2 = driver.FindElementById("number2");
            result = driver.FindElementById("result");
            buttonCalculate = driver.FindElementById("calcButton");
            buttonReset = driver.FindElementById("resetButton");


        }

        //Test with valid integers
        [TestCase("5", "+", "3", "Result: 8")]
        [TestCase("5", "*", "3", "Result: 15")]
        [TestCase("12", "/", "3", "Result: 4")]
        [TestCase("5", "-", "3", "Result: 2")]

        //Test with valid decimals
        [TestCase("5.23", "+", "3.88", "Result: 9.11")]
        [TestCase("3.14", "-", "12.763", "Result: -9.623")]
        [TestCase("3.14", "*", "-7.534", "Result: -23.65676")]
        [TestCase("12.5", "/", "4", "Result: 3.125")]


        //Test with invalid input
        [TestCase("", "+", "3", "Result: invalid input")]
        [TestCase("5", "+", "", "Result: invalid input")]
        [TestCase("asass", "+", "3", "Result: invalid input")]
        [TestCase("", "+", "", "Result: invalid input")]
        [TestCase("asa", "+", "sasa", "Result: invalid input")]
        [TestCase("4", "+", "sasa", "Result: invalid input")]

        [TestCase("", "-", "3", "Result: invalid input")]
        [TestCase("5", "-", "", "Result: invalid input")]
        [TestCase("asass", "-", "3", "Result: invalid input")]
        [TestCase("", "-", "", "Result: invalid input")]
        [TestCase("asa", "-", "sasa", "Result: invalid input")]
        [TestCase("4", "-", "sasa", "Result: invalid input")]

        [TestCase("", "*", "3", "Result: invalid input")]
        [TestCase("5", "*", "", "Result: invalid input")]
        [TestCase("asass", "*", "3", "Result: invalid input")]
        [TestCase("", "*", "", "Result: invalid input")]
        [TestCase("asa", "*", "sasa", "Result: invalid input")]
        [TestCase("4", "*", "sasa", "Result: invalid input")]

        [TestCase("", "/", "3", "Result: invalid input")]
        [TestCase("5", "/", "", "Result: invalid input")]
        [TestCase("asass", "/", "3", "Result: invalid input")]
        [TestCase("", "/", "", "Result: invalid input")]
        [TestCase("asa", "/", "sasa", "Result: invalid input")]
        [TestCase("4", "/", "sasa", "Result: invalid input")]

        //Invalid operations
        [TestCase("4", "@", "7", "Result: invalid operation")]
        [TestCase("4", "!!!!", "7", "Result: invalid operation")]
        [TestCase("4", "", "7", "Result: invalid operation")]

        //Test with Infinity
        [TestCase("Infinity", "+", "1", "Result: Infinity")]
        [TestCase("Infinity", "-", "1", "Result: Infinity")]
        [TestCase("Infinity", "*", "1", "Result: Infinity")]
        [TestCase("Infinity", "/", "1", "Result: Infinity")]
        [TestCase("1", "+", "Infinity", "Result: Infinity")]
        [TestCase("1", "-", "Infinity", "Result: -Infinity")]
        [TestCase("1", "*", "Infinity", "Result: Infinity")]
        [TestCase("1", "/", "Infinity", "Result: 0")]
        [TestCase("Infinity", "*", "Infinity", "Result: Infinity")]
        [TestCase("Infinity", "+", "Infinity", "Result: Infinity")]

        //Test with invalid calculation
        [TestCase("Infinity", "/", "Infinity", "Result: invalid calculation")]
        [TestCase("Infinity", "-", "Infinity", "Result: invalid calculation")]

        //Test with exponential numbers
        [TestCase("1.5e53", "*", "150", "Result: 2.25e+55")]
        [TestCase("1.5e53", "/", "150", "Result: 1e+51")]

        public void TestCalculatorWebApp(string num1, string op, string num2, string expectedResult)
        {
            //Arrange
            buttonReset.Click();
            if (num1 != "")
            {
                textboxNum1.SendKeys(num1);
            }
            if (op != "")
            {
                dropdownOptions.SendKeys(op);
            }
            if (num2 != "")
            {
                textboxNum2.SendKeys(num2);
            }

            //Act
            buttonCalculate.Click();

            //Assert
            var actualResult = result.Text;
            Assert.AreEqual(expectedResult, actualResult);
            
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
