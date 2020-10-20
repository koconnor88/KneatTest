using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KneatTest.Framework.Components.SelectGuestsControl
{
    public class SelectGuestsControl : BaseControl
    {
        public SelectGuestsControl(IWebElement referenceElement) : base(referenceElement){}

        protected IWebElement ToggleElement => WrappedElement.FindElement(By.CssSelector(".xp__guests__count"));

        public SelectGuestsPopupPane Activate()
        {
            if (!SelectGuestsPane.Displayed)
                ToggleElement.Click();
            return SelectGuestsPane;
        }

        public SelectGuestsPopupPane SelectGuestsPane => new SelectGuestsPopupPane(WrappedElement.FindElement(By.Id("xp__guests__inputs-container")));
    }
}
