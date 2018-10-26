using FinanceTestTask.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FinanceTestTask
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Close();
        }        

        [Test]
        public void IsSaleRateHigherThanBuyRateAllBanks()
        {
            MainPage mainPage = new MainPage(driver);
            mainPage.GoToPage();
            mainPage.GetCurrencyFilterBlock().SelectCurrency("USD").ListOfBanks.IsSaleHigherThanBuy();
            mainPage.GetCurrencyFilterBlock().SelectCurrency("EUR").ListOfBanks.IsSaleHigherThanBuy();
            mainPage.GetCurrencyFilterBlock().SelectCurrency("RUB").ListOfBanks.IsSaleHigherThanBuy();
        }

        [TestCase("1000", "USD", "SALE", "Аркада")]
        [TestCase("123.53", "EUR", "BUY", "НБУ")]
        [TestCase("2245.66", "RUB", "SALE", "ПУМБ")]
        public void CheckConvertedAmountFor(string exchangeAmount, string сurrencyName, string byOrSale, string bank)
        {
            ConverterPage converterPage = new ConverterPage(driver);
            converterPage.GoToPage();
            converterPage.GetConverterBlock().FillForm(exchangeAmount, сurrencyName, byOrSale, bank).IsConvertedAmountCorrect();
        }
    }
}
