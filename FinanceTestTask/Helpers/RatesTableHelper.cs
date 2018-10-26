using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace FinanceTestTask.Pages
{
    public class RatesTable
    {
        private readonly IWebDriver driver;

        public RatesTableBodyBlock ListOfBanks { get; set; }

        public RatesTable(IWebDriver browser, string selectedCurrency)
        {
            driver = browser;
            PageFactory.InitElements(driver, this);

            ListOfBanks = new RatesTableBodyBlock(driver, selectedCurrency);
        }
    }
}
