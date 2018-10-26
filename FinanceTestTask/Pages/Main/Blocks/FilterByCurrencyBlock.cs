using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace FinanceTestTask.Pages
{
    public class FilterByCurrencyBlock
    {
        private readonly IWebDriver driver;
        public FilterByCurrencyBlock(IWebDriver browser)
        {
            driver = browser;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='latest_currency_selector']")]
        private IWebElement CurrencySelector { get; set; }

        public RatesTable SelectCurrency(string cur)
        {
            IWebElement CurrencyItem = GetCurrencyItem(cur);
            CurrencyItem.Click();

            return new RatesTable(driver, cur);
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
