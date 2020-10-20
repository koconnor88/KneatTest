using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace KneatTest.Framework.Components
{
    public class PagePane
    {
        public virtual IWebElement WrappedElement { get; }
        protected IWebDriver WrappedDriver { get; }
        public PagePane(IWebElement referenceElement)
        {
            WrappedElement = referenceElement;
            WrappedDriver = ((IWrapsDriver)referenceElement).WrappedDriver;
        }
        
        public virtual bool Enabled => WrappedElement.Enabled;

        public virtual bool Displayed => WrappedElement.Displayed;
    }
}
