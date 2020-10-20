using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KneatTest.Framework.Components.ResulltsFilterControls
{
    public class BaseFilterControl : BaseControl
    {
        public BaseFilterControl(IWebElement referenceElement) : base(referenceElement){}

        //.filteroptions > a gives list of options
        //have option control with Name and select/unselect and count
        //make virtual on base to over write if needed

        protected virtual IWebElement FilterTitleELement => WrappedElement.FindElement(By.ClassName("filtercategory-title"));

        public string FilterTitle => FilterTitleELement.Text;

        public virtual List<FilterCheckboxControl> Options => WrappedElement.FindElements(By.CssSelector("a"))
            .Select(x => new FilterCheckboxControl(x)).ToList();

    }
}
