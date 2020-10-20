using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KneatTest.Framework.Components.CalendarControl
{
    public class CalendarSelectDatesControl : BaseControl
    {
        public CalendarSelectDatesControl(IWebElement referenceElement) : base(referenceElement)
        {}

        protected IWebElement PrevMonthElement => WrappedElement.FindElement(By.CssSelector(".bui-calendar__control--prev"));

        protected IWebElement NextMonthElement => WrappedElement.FindElement(By.CssSelector(".bui-calendar__control--next"));

        protected List<CalendarMonthControl> DisplayedCalendars => WrappedElement.FindElements(By.ClassName("bui-calendar__wrapper"))
            .Select(x => new CalendarMonthControl(x)).ToList();

        public DateTime DisplayedFromDate => DisplayedCalendars.First().FromDate;
        public DateTime DisplayedToDate => DisplayedCalendars.Last().ToDate;

        //selectDates(datetime frm, datetime to)
        //check from a 1st calendar display and to on last cal displayed => if not between get dif in months click next 
        //repeat for to date
        public void SelectDate(DateTime date)
        {
            SelectMonth(date);
            DisplayedCalendars.First().SelectDate(date);
        }

        public void SelectMonth(DateTime date)
        {
            if (!(date >= DisplayedFromDate && date <= DisplayedToDate))
            {
                var diff = (date.Month - DisplayedFromDate.Month) + 12 * (date.Year - DisplayedFromDate.Year);

                if (diff > 0)
                {
                    for (var i = 0; i < diff; i++)
                    {
                        NextMonthElement.Click();
                    }
                }
                if (diff < 0)
                {
                    for (var i = 0; i > diff; i--)
                    {
                        PrevMonthElement.Click();
                    }
                }
            }
        }
    }
}
