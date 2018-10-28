using FinanceTestTask.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

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
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
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

            FilterByCurrencyBlock filter = mainPage.GetCurrencyFilterBlock();

            filter.SelectCurrency("USD");
            mainPage.GetRatesTableBlock().IsSaleHigherThanBuy();

            filter.SelectCurrency("EUR");
            mainPage.GetRatesTableBlock().IsSaleHigherThanBuy();

            filter.SelectCurrency("RUB");
            mainPage.GetRatesTableBlock().IsSaleHigherThanBuy();
        }

        [TestCase("1000", "USD", "SALE", "Аркада")]
        [TestCase("123.53", "EUR", "BUY", "НБУ")]
        [TestCase("2245.66", "RUB", "SALE", "ПУМБ")]
        public void CheckConvertedAmountFor(string exchangeAmount, string сurrencyName, string byOrSale, string bank)
        {
            ConverterPage converterPage = new ConverterPage(driver);

            converterPage.GoToPage();

            CurrencyConverterBlock converterForm = converterPage.GetConverterBlock();

            converterForm.FillForm(exchangeAmount, сurrencyName, byOrSale, bank).IsConvertedAmountCorrect();
        }

        [Test]
        public void SortByBankNameDesc()
        {
            MainPage mainPage = new MainPage(driver);

            mainPage.GoToPage();

            RatesTableBlock ratesTable = mainPage.GetRatesTableBlock();

            var banksTableBeforeSort = ratesTable.GetCurrentExchangeRatesOfBanksTable();

            ratesTable.SortByBankNameColumn("DESC");

            var expectedResult = banksTableBeforeSort.OrderByDescending(i => i.Key);
            var actualResult = ratesTable.GetCurrentExchangeRatesOfBanksTable();

            Assert.True(expectedResult.Select(e => e.Key).SequenceEqual(actualResult.Select(a => a.Key)));
        }
    }
}
