using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Globalization;

namespace KneatTest.Framework.Components.CalendarControl
{
    public class CalendarMonthControl : BaseControl
    {
        public CalendarMonthControl(IWebElement referenceElement) : base(referenceElement)
        {}

        protected IWebElement MonthElement => WrappedElement.FindElement(By.CssSelector(".bui-calendar__month"));
        //protected string DateString => MonthElement.GetAttribute("");
        public DateTime FromDate => DateTime.Parse(MonthElement.Text);

        public DateTime ToDate => new DateTime(FromDate.Year, FromDate.Month, DateTime.DaysInMonth(FromDate.Year, FromDate.Month));

        protected List<DateCellControl> DateElements => WrappedElement.FindElements(By.CssSelector("table.bui-calendar__dates tbody td[role='gridcell']"))
            .Select(x => new DateCellControl(x)).ToList();

        public void SelectDate(DateTime date)
        {
            var dateCell = DateElements.FirstOrDefault(dt => dt.Date == date.Date);
            dateCell?.Select();
        }
    }

    public class DateCellControl : BaseControl
    {
        public DateCellControl(IWebElement referenceElement) : base(referenceElement)
        { }

        private string DateString => WrappedElement.GetAttribute("data-date");
        public DateTime Date => DateTime.ParseExact(DateString, "yyyy-mm-dd", CultureInfo.InvariantCulture).Date;

        public void Select()
        {
            WrappedElement.Click();
        }
    }
}
