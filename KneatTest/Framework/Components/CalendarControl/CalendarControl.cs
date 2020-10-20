using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using KneatTest.Common;

namespace KneatTest.Framework.Components.CalendarControl
{
    public class CalendarControl : BaseControl
    {
        public CalendarControl(IWebElement referenceElement) : base(referenceElement){}

        //Element to open select date control
        protected IWebElement CheckInElement => WrappedElement.FindElement(By.CssSelector(".xp__dates__checkin"));

        public CalendarSelectDatesControl Activate()
        {
            if(!SelectDatesPanel.WrappedElement.Displayed)
            {
                CheckInElement.Click();
                AutomationWait<CalendarSelectDatesControl>.On(SelectDatesPanel)
                    .UpTo(TimeSpan.FromMilliseconds(500))
                    .TimeoutMessage("Select dates panel not displayed when control clicked")
                    .Until(x => x.Displayed);
            }
            return SelectDatesPanel;
        }

        public CalendarSelectDatesControl SelectDatesPanel => new CalendarSelectDatesControl(WrappedElement.FindElement(By.CssSelector(".bui-calendar")));

        public DateTime FromDate
        {
            //get
            //{
            //    //get from bottom of panel
            //}
            set
            {
                Activate();
                SelectDatesPanel.SelectDate(value);
            }
        }

        public DateTime ToDate
        {
            //get
            //{
            //    //get from bottom of panel
            //}
            set
            {
                Activate();
                SelectDatesPanel.SelectDate(value);
            }
        }
    }
}
