using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using KneatTest.Framework.Components.ResulltsFilterControls;
using KneatTest.Framework.Components.ResultItemControl;
using KneatTest.Framework.Components.FilterTags;

namespace KneatTest.Framework.Pages
{
    public class ResultsPage : BasePage<ResultsPage>
    {
        public ResultsPage(IWebDriver driver) : base(driver) { }
        public override string PageUri => throw new NotImplementedException();

        public override By PageLoadedElementLocator { get => By.CssSelector(".hotellist_wrap"); }

        
        //Would create a filter section and results section class if creating a full framework
        //For the purposes of this exercise this allows me to access them when required
        public List<BaseFilterControl> Filters => _driver.FindElements(By.CssSelector(".filterbox")).Where(y => y.Displayed)
            .Select(x => new BaseFilterControl(x)).ToList();

        public List<ResultItemControl> Results => _driver.FindElements(By.CssSelector(".sr_item")).Select(x => new ResultItemControl(x)).ToList();

        public FilterTagPane TagFilters => new FilterTagPane(_driver.FindElements(By.CssSelector("sr-broaden-search")).FirstOrDefault());
    }
}
