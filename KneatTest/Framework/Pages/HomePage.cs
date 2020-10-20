using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using KneatTest.Framework.Components.SearchBoxControl;
using KneatTest.Framework.Components.CalendarControl;
using KneatTest.Framework.Components.SelectGuestsControl;

namespace KneatTest.Framework.Pages
{
    public class HomePage : BasePage<HomePage>
    {
        public HomePage(IWebDriver driver) : base(driver) { }
        public override string PageUri => "https://www.booking.com/";

        public override By PageLoadedElementLocator { get => By.CssSelector("div[data-component='search/destination/input']"); }

        //Would make the entire search panel it's own class/pagepane if building full framework
        public SearchBoxControl DestinationSearchBox => new SearchBoxControl(_driver.FindElement(By.CssSelector("div[data-component='search/destination/input']")));

        public CalendarControl CalendarControl => new CalendarControl(_driver.FindElement(By.CssSelector(".xp__dates")));

        public SelectGuestsControl SelectGuestsControl => new SelectGuestsControl(_driver.FindElement(By.CssSelector(".xp__guests")));

        public IWebElement SearchButtonElement => _driver.FindElement(By.CssSelector(".sb-searchbox__button"));

        public ResultsPage Search()
        {
            SearchButtonElement.Click();
            var resultPage = new ResultsPage(_driver);
            resultPage.WaitFotPageToLoad();
            return resultPage;
        }


    }
}
