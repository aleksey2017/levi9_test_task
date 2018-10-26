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
    public class ConverterPage
    {
        private readonly IWebDriver driver;

        public ConverterPage(IWebDriver browser)
        {
            driver = browser;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='section_nav']/div/div[1]/ul/li[1]")]
        private IWebElement mainPage { get; set; }

        public void GoToPage()
        {
            driver.Navigate().GoToUrl("https://finance.i.ua/converter/");
        }

        public CurrencyConverterBlock GetConverterBlock()
        {
            return new CurrencyConverterBlock(driver);
        }
    }
}
