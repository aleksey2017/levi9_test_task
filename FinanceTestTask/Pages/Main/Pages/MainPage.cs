using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace FinanceTestTask.Pages
{
    public class MainPage
    {
        private readonly IWebDriver driver;
        public MainPage(IWebDriver browser)
        {
            driver = browser;
            PageFactory.InitElements(driver, this);
        }

        public void GoToPage()
        {
            driver.Navigate().GoToUrl("https://finance.i.ua/");
        }

        public FilterByCurrencyBlock GetCurrencyFilterBlock()
        {
            return new FilterByCurrencyBlock(driver);
        }
    }
}
