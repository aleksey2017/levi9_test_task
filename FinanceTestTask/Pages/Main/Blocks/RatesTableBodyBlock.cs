using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceTestTask.Pages
{
    public class RatesTableBlock
    {
        private readonly IWebDriver driver;
        private string SelectedCurrency { get; set; }

        public RatesTableBlock(IWebDriver browser)
        {
            driver = browser;            
            PageFactory.InitElements(driver, this);
            SelectedCurrency = GetSelectedCurrency();
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='latest_currency_container']/thead")]
        private IWebElement RateTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//tbody[@class='bank_rates_usd']")]
        private IWebElement RateTableUSD { get; set; }

        [FindsBy(How = How.XPath, Using = "//tbody[@class='bank_rates_eur']")]
        private IWebElement RateTableEUR { get; set; }

        [FindsBy(How = How.XPath, Using = "//tbody[@class='bank_rates_rub']")]
        private IWebElement RateTableRUB { get; set; }

        public void IsSaleHigherThanBuy()
        {
            var BanksRatesTable = GetRateTable(SelectedCurrency);

            Assert.NotNull(BanksRatesTable);

            foreach (var bank in BanksRatesTable)
            {
                Assert.True(double.Parse(bank.Value["Buy"]) < double.Parse(bank.Value["Sell"]));
            }
        }

        public void SortByBankNameColumn(string ascOrDesc)
        {
            SetUpSorting(ascOrDesc, By.ClassName("col-bank"));
        }

        public void SortByBuyColumn(string ascOrDesc)
        {
            SetUpSorting(ascOrDesc, By.ClassName("col-buy"));
        }

        public void SortBySaleColumn(string ascOrDesc)
        {
            SetUpSorting(ascOrDesc, By.ClassName("col-sale"));
        }

        public Dictionary<string, Dictionary<string, string>> GetCurrentExchangeRatesOfBanksTable()
        {
            return GetRateTable(SelectedCurrency);
        }

        private string GetCurrentSorting(IWebElement columnName)
        {
            switch (columnName.GetAttribute("aria-sort"))
            {
                case "ascending":
                    return "ASC";
                case "descending":
                    return "DESC";
                default:
                    return "NONE";
            }
        }

        private void SetUpSorting(string direction, By locator)
        {
            var colunmName = RateTableHeader.FindElement(locator);

            switch (direction)
            {
                case "ASC":
                    while (GetCurrentSorting(colunmName) != "ASC")
                    {
                        colunmName.Click();
                    }
                    break;
                case "DESC":
                    while (GetCurrentSorting(colunmName) != "DESC")
                    {
                        colunmName.Click();
                    }
                    break;
            }
        }

        private string GetSelectedCurrency()
        {
            var filterBlock = new FilterByCurrencyBlock(driver);
            return filterBlock.CurrentCurrency;
        }

        private Dictionary<string, Dictionary<string, string>> GetRateTable(string selectedCurrency)
        {
            switch (selectedCurrency)
            {
                case "USD":
                    return CreateRateTable(RateTableUSD);
                case "EUR":
                    return CreateRateTable(RateTableEUR);
                case "RUB":
                    return CreateRateTable(RateTableRUB);
                default:
                    return null;
            }
        }

        private Dictionary<string, Dictionary<string, string>> CreateRateTable(IWebElement rateTable)
        {
            IList<IWebElement> TbaleRows = rateTable.FindElements(By.TagName("tr"));

            Dictionary<string, Dictionary<string, string>> BankRates = new Dictionary<string, Dictionary<string, string>>();

            foreach (IWebElement cell in TbaleRows)
            {
                Dictionary<string, string> BuySellRates = new Dictionary<string, string>();

                BuySellRates.Add("Buy", cell.FindElement(By.ClassName("buy_rate")).Text);
                BuySellRates.Add("Sell", cell.FindElement(By.ClassName("sell_rate")).Text);

                BankRates.Add(cell.FindElement(By.ClassName("td-title")).Text, BuySellRates);
            }

            return BankRates;
        }
    }
}
