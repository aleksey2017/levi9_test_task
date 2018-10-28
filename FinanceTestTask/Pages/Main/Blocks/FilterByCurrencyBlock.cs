using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace FinanceTestTask.Pages
{
    public class FilterByCurrencyBlock
    {
        private readonly IWebDriver driver;
        public string CurrentCurrency { get; private set; }

        public FilterByCurrencyBlock(IWebDriver browser)
        {
            driver = browser;
            PageFactory.InitElements(driver, this);
            CurrentCurrency = GetCurrentCurrency();
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='latest_currency_selector']")]
        private IWebElement CurrencySelector { get; set; }

        public void SelectCurrency(string cur)
        {
            IWebElement CurrencyItem = GetCurrencyItem(cur);

            CurrencyItem.Click();
        }

        private string GetCurrentCurrency()
        {
            //var CurrentCurrency = CurrencySelector.FindElement(By.XPath("//li[contains(@class,'-current')]/a"));
            var currentCurrency = CurrencySelector.FindElement(By.ClassName("-current"));

            return currentCurrency.Text;
        }

        private IWebElement GetCurrencyItem(string cur) 
        {
            IWebElement Item;
            switch (cur)
            {
                case "USD":
                    Item = CurrencySelector.FindElement(By.ClassName("usd_selector"));
                    return Item;
                case "EUR":
                    Item = CurrencySelector.FindElement(By.ClassName("eur_selector"));
                    return Item;
                case "RUB":
                    Item = CurrencySelector.FindElement(By.ClassName("rub_selector"));
                    return Item;
                default:
                    return null;
            }
        }
    }
}
