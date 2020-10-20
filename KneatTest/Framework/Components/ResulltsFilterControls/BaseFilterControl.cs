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
        //Would make an abstract class BaseFilterControl<T> where T Filter pane which would include options
        //This would allow to use more than Checkbox filters i.e slider for price
        //For the purposes of this exercise having options here is enough
        public BaseFilterControl(IWebElement referenceElement) : base(referenceElement){}
        
        protected virtual IWebElement FilterTitleELement => WrappedElement.FindElement(By.ClassName("filtercategory-title"));

        public string FilterTitle => FilterTitleELement.Text;

        public virtual List<FilterCheckboxControl> Options => WrappedElement.FindElements(By.CssSelector("a"))
            .Select(x => new FilterCheckboxControl(x)).ToList();

    }
}
