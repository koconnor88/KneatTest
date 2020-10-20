using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using KneatTest.Framework.Pages;
using System.Globalization;

namespace KneatTest.Tests
{
    [TestFixture]
    class BookingSearchFilterTests
    {
        protected IWebDriver driver;

        [SetUp]
        public void TestSetup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TestTeardown()
        {
            driver.Dispose();
        }

        [Test]
        public void Test1()
        {
            var fromDate = DateTime.UtcNow.AddMonths(3);
            var toDate = fromDate.AddDays(1);
            var homePage = new HomePage(driver).NavigateTo();
            homePage.AcceptCookies();
            homePage.DestinationSearchBox.Search("limerick").Results.FirstOrDefault(x => x.Value.Contains("Limerick")).Select();
            homePage.CalendarControl.FromDate = fromDate;
            homePage.CalendarControl.ToDate = toDate;
            homePage.SelectGuestsControl.Activate().AdultsControl.SetNumber(4);
            homePage.SelectGuestsControl.Activate().RoomsControl.SetNumber(2);
            homePage.SearchButtonElement.Click();

            var resultsPage = new ResultsPage(driver);
            resultsPage.WaitFotPageToLoad();
            

            var filter = resultsPage.Filters.FirstOrDefault(x => x.FilterTitle.Contains("Fun Things To Do"));
            
            var option = filter.Options.FirstOrDefault(x => x.Option.Contains("Fitness cent"));

            option.Selected = true;

            var strand = resultsPage.Results.FirstOrDefault(x => x.Name.Contains("Limerick Strand Hotel"));
            Assert.NotNull(strand, "Hotel not displayed");
        }

        [Test]
        public void dateparse()
        {
            var dateString = "October 2020";
            var dateString2 = "2020-10-14";
            var date = DateTime.Parse(dateString);

            var lastDay = DateTime.DaysInMonth(date.Year, date.Month);
            var toDate = new DateTime(date.Year, date.Month, lastDay);

            var date2 = DateTime.ParseExact(dateString2, "yyyy-mm-dd", CultureInfo.InvariantCulture);
        }

        [Test]
        public void bookingDate()
        {
            var fromDate = DateTime.UtcNow.AddMonths(3);
            var toDate = fromDate.AddDays(1);
            var date = new DateTime(2020, 10, 01);
            var date2 = new DateTime(2020, 11, 30);
            //if(!(bookingDate >= date && bookingDate <= date2))
            //{
            //    var diff = (bookingDate.Month - date.Month) + 12 * (bookingDate.Year - date.Year);
            //}
            SelectDates(fromDate, toDate);

            
        }

        public void SelectDates(DateTime fromDate, DateTime toDate)
        {
            var DisplayedFromDate = new DateTime(2020, 10, 01);
            var DisplayedToDate = new DateTime(2020, 11, 30);
            var click = 0;
            if (!(fromDate >= DisplayedFromDate && fromDate <= DisplayedToDate))
            {
                var diff = (fromDate.Month - DisplayedFromDate.Month) + 12 * (fromDate.Year - DisplayedFromDate.Year);
                diff = -3;
                //if diff > 0 click next * diff times
                //if diff < 0 click prev * diff times
                if(diff > 0)
                {
                    for(var i = 0; i < diff; i++)
                    {
                        click++;
                    }
                }
                if(diff < 0)
                {
                    for (var i = 0; i > diff; i--)
                    {
                        click++;
                    }
                }
            }

            Assert.AreEqual(3, click, "Click not 3");
        }

        [Test]
        public void BoolTest()
        {
            string test = "True";
            bool testBool = bool.Parse(test);
        }
    }
}
