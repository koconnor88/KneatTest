using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KneatTest.Framework.Components.FilterTags
{
    public class FilterTagControl : BaseControl
    {
        public FilterTagControl(IWebElement referenceElement) : base(referenceElement) { }

        public string FilterName => WrappedElement.Text.Trim();

        public void ClearFilter() => WrappedElement.Click();
    }
}
