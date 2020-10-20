using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KneatTest.Framework.Components.SelectGuestsControl
{
    public class SelectGuestNumberControl : BaseControl
    {
        public SelectGuestNumberControl(IWebElement referenceElement) : base(referenceElement){}

        protected IWebElement DecreaseButtonElement => WrappedElement.FindElement(By.CssSelector(".bui-stepper__subtract-button"));
        protected IWebElement IncreaseNumberElement => WrappedElement.FindElement(By.CssSelector(".bui-stepper__add-button"));
        
        protected IWebElement CurrentNumberElement => WrappedElement.FindElement(By.CssSelector(".bui-stepper__display"));

        public int CurrentValue => int.Parse(CurrentNumberElement.Text);

        public void IncreaseNumber()
        {
            IncreaseNumberElement.Click();
        }

        public void DecreaseNumber()
        {
            DecreaseButtonElement.Click();
        }

        public void SetNumber(int number)
        {
            if (number == CurrentValue)
                return;

            var NoOfClicks = number - CurrentValue;

            if(NoOfClicks > 0)
            {
                for (var i = 0; i < NoOfClicks; i++)
                    IncreaseNumber();
            }
            else
            {
                for (var i = 0; i > NoOfClicks; i--)
                    IncreaseNumber();
            }
        }
    }
}
