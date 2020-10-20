using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using KneatTest.Common;

namespace KneatTest.Framework.Components.ResulltsFilterControls
{
    public class FilterCheckboxControl : BaseControl
    {
        public FilterCheckboxControl(IWebElement referenceElement) :base(referenceElement){}

        //checkbox element
        protected IWebElement CheckboxElement => WrappedElement.FindElement(By.CssSelector("input"));
        //label element
        protected IWebElement LabelElement => WrappedElement.FindElement(By.CssSelector(".filter_label"));
        //count element
        protected IWebElement CountElement => WrappedElement.FindElement(By.CssSelector(".filter_count"));

        public bool Selected
        {
            get => CheckboxElement.Selected;
            set
            {
                if (CheckboxElement.Selected != value)
                    WrappedElement.Click();
                
                //Wait for overlay to appear and then disappear
                //This could probably be replaced by waiting on the js events with more time
                AutomationWait<IWebDriver>.On(WrappedDriver)
                    .UpTo(TimeSpan.FromMilliseconds(500))
                    .SilentUntil(x => x.FindElements(By.CssSelector(".sr-usp-overlay")) != null);

                AutomationWait<IWebDriver>.On(WrappedDriver)
                    .UpTo(TimeSpan.FromMilliseconds(1500))
                    .SilentUntil(x => x.FindElements(By.CssSelector(".sr-usp-overlay")) == null);


            }
        }

        public string Option => LabelElement.Text;

        public int Count => int.Parse(CountElement.Text.Trim());
    }
}
