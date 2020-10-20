using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using KneatTest.Common;


namespace KneatTest.Framework.Components
{
    public class BaseControl
    {
        protected BaseControl(IWebElement referenceElement)
        {
            WrappedElement = referenceElement;
            WrappedDriver = ((IWrapsDriver)referenceElement).WrappedDriver;
        }

        public virtual IWebElement WrappedElement { get; }

        protected IWebDriver WrappedDriver { get; }

        public virtual bool Enabled => WrappedElement.Enabled;

        public virtual bool Displayed => WrappedElement.Displayed;

        protected bool WaitForElementToBeDisplayed(By elementLocator, int timeOut = 3)
        {
            return AutomationWait<IWebDriver>.On(WrappedDriver)
                .Until(x => x.FindElements(elementLocator) != null && x.FindElement(elementLocator).Displayed);
        }
    }
}
