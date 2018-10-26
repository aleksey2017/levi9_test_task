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
    public class CurrencyConverterBlock
    {
        private readonly IWebDriver driver;

        public CurrencyConverterBlock(IWebDriver browser)
        {
            driver = browser;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='sell']")]
        private IWebElement saleButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='buy']")]
        private IWebElement byButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='converter_currency']")]
        private IWebElement converterCurrency { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='currency_amount']")]
        private IWebElement currencyAmount { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='converter_bank']")]
        private IWebElement converterBank { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='form_converter_result']/p[@id='UAH']/input[@id='currency_exchange']")]
        private IWebElement converterResultUA { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='form_converter_result']/p[@id='UAH']/input[@id='currency_rate']")]
        private IWebElement currencyRate { get; set; }

        public ResultsBlock FillForm(string exchangeAmount, string сurrencyName, string byOrSale, string bank)
        {
            InputCurrencyAmount(exchangeAmount);
            SelectCurrency(сurrencyName);
            switch (byOrSale)
            {
                case "BUY":
                    IWishBy();
                    break;
                case "SALE":
                    IWishSale();
                    break;
                default:
                    IWishSale();
                    break;
            }
            SelectBank(bank);

            return GetResultsBlock(exchangeAmount);
        }

        private void InputCurrencyAmount(string cur)
        {
            currencyAmount.Clear();
            currencyAmount.SendKeys(cur);
        }

        private void IWishBy() { byButton.Click(); }

        private void IWishSale() { saleButton.Click(); }

        private void SelectBank(string bankName)
        {
            var selectElement = new SelectElement(converterBank);
            selectElement.SelectByText(bankName);
        }

        private void SelectCurrency(string сurrencyName)
        {
            var selectElement = new SelectElement(converterCurrency);
            selectElement.SelectByText(сurrencyName);
        }

        private ResultsBlock GetResultsBlock(string exchangeAmount)
        {
            return new ResultsBlock(driver, exchangeAmount);
        }
    }
}
