using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KneatTest.Framework.Components.FilterTags
{
    public class FilterTagPane : PagePane
    {
        public FilterTagPane(IWebElement referenceElement) : base(referenceElement) { }

        public List<FilterTagControl> FilterTags => WrappedElement.FindElements(By.CssSelector(".bui-button--secondary"))
            .Select(X => new FilterTagControl(X)).ToList();

        public void ClearFilters()
        {
            var tag = FilterTags.FirstOrDefault(x => x.FilterName.Contains("Clear"));
            tag?.ClearFilter();
        }
    }
}
