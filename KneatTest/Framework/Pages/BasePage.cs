using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using KneatTest.Common;

namespace KneatTest.Framework.Pages
{
    public abstract class BasePage<TPage>
        where TPage : BasePage<TPage>
    {
        public IWebDriver _driver;
        //Would set base from config if building a full framework to allow for different env
        //With time constraints and basic nature of exercise this serves the purpose
        public abstract string PageUri { get; }
        
        public abstract By PageLoadedElementLocator { get; }

        protected BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        //Would implement LoadadleComponent pattern with Page Object Pattern if building a full framwork.
        //For the purposes of this exercise I am using this so I can check explicitly if page had loaded
        public TPage NavigateTo()
        {
            _driver.Navigate().GoToUrl(PageUri);
            WaitFotPageToLoad();
            return (TPage)Activator.CreateInstance(typeof(TPage), _driver);
        }

        //Would implement PageObject Factory with Loadable Component pattern
        //if implementing a full framework
        //For now this serves the purpose of checking if page loaded and can be interacted with
        public virtual void WaitFotPageToLoad()
        {
            if (!WaitForElementToExist(PageLoadedElementLocator))
                throw new WebDriverTimeoutException($"{typeof(TPage).Name} Failed to load. Could not find element locaated by {PageLoadedElementLocator.ToString()}");
        }

        //A bit rough and ready but serves as a quick check if ElementExists on page
        //With more time would implement as an Extension method on driver
        //Would make timeout configurable if building full framework
        public virtual bool WaitForElementToExist(By elementLocator, int timeOut = 30)
        {
            
            return AutomationWait<IWebDriver>.On(_driver)
                .UpTo(TimeSpan.FromSeconds(timeOut))
                .Until(x => x.FindElement(elementLocator)).Displayed;
        }

        public void AcceptCookies()
        {
            //Would create a class for cookies banner if creating a full framework
            var panelElement = By.Id("onetrust-banner-sdk");
            var acceptButtonLocator = By.Id("onetrust-accept-btn-handler");
            
            //Wait for Cookies banner to be in position before clicking
            AutomationWait<IWebDriver>.On(_driver)
                .SilentUntil(x => x.FindElements(panelElement) != null && x.FindElement(panelElement).GetAttribute("style").Contains("0px"));
            
            _driver.FindElement(acceptButtonLocator).Click();
        }
    }
}
