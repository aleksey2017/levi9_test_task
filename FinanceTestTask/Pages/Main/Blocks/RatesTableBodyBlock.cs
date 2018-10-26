using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceTestTask.Pages
{
    public class RatesTableBodyBlock
    {
        private readonly IWebDriver driver;
        private string SelectedCurrency { get; set; }

        public RatesTableBodyBlock(IWebDriver browser, string currency)
        {
            driver = browser;
            SelectedCurrency = currency;
            PageFactory.InitElements(driver, this);
        }

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

        public Dictionary<string, Double> GetMinValues()
        {
            List<double> ratesListForBuy = new List<double>();
            List<double> ratesListForSell = new List<double>();

            Dictionary<string, Double> countersMinMax = new Dictionary<string, double>();

            var BanksRatesTable = GetRateTable(SelectedCurrency);            

            foreach (var bank in BanksRatesTable)
            {
                ratesListForBuy.Add(double.Parse(bank.Value["Buy"]));
                ratesListForSell.Add(double.Parse(bank.Value["Sell"]));
            }

            countersMinMax.Add("Buy", ratesListForBuy.ToArray().Min());
            countersMinMax.Add("Sell", ratesListForSell.ToArray().Min());

            return countersMinMax;
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
