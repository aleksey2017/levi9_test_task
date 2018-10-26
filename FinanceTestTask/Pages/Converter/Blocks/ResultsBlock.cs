using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTestTask.Pages
{
    public class ResultsBlock
    {
        private readonly IWebDriver driver;
        private string ExchangeAmount { get; set; }

        public ResultsBlock(IWebDriver browser, string amount)
        {
            ExchangeAmount = amount;
            driver = browser;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='form_converter_result']/p[@id='UAH']/input[@id='currency_exchange']")]
        private IWebElement converterResultUA { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='form_converter_result']/p[@id='UAH']/input[@id='currency_rate']")]
        private IWebElement currencyRate { get; set; }

        public void IsConvertedAmountCorrect()
        {
            var parseExchangeAmount = double.Parse(ExchangeAmount);
            var parseCurrencyRate = double.Parse(currencyRate.GetAttribute("value"));

            var actualAmount = double.Parse(converterResultUA.GetAttribute("value").Replace(" ", string.Empty));
            var expectedAmount = Math.Round(parseExchangeAmount * parseCurrencyRate, 2);

            Assert.AreEqual(expectedAmount, actualAmount);
        }
    }
}
