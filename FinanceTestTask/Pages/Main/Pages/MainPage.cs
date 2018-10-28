using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;

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

        [FindsBy(How = How.XPath, Using = "//div[starts-with(@id, 'holderAjax')]//iframe[starts-with(@id, 'holderIframe_holder')]")]
        private IWebElement AdvertasingDiv { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='close']/img")]
        private IWebElement CLoseAdvertasingDiv { get; set; }

        public void GoToPage()
        {
            driver.Navigate().GoToUrl("https://finance.i.ua/");
            Thread.Sleep(3000);

            try
            {
                if (AdvertasingDiv.Displayed)
                {
                    driver.SwitchTo().Frame(AdvertasingDiv);
                    CLoseAdvertasingDiv.Click();
                    driver.SwitchTo().DefaultContent();
                }
            }
            catch (NoSuchElementException)
            {
                return;
            }

        }

        public FilterByCurrencyBlock GetCurrencyFilterBlock()
        {
            return new FilterByCurrencyBlock(driver);
        }

        public RatesTableBlock GetRatesTableBlock()
        {
            return new RatesTableBlock(driver);
        }


    }
}
