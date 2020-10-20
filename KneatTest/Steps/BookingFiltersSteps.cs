using System;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using KneatTest.Framework.Pages;

namespace KneatTest.Steps
{
    [Binding]
    public class BookingFiltersSteps
    {
        private IWebDriver _driver;
        private HomePage _homePage;
        private ResultsPage _resultsPage;
        DateTime fromDate = DateTime.UtcNow.AddMonths(3);

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        [Given(@"User is on the booking\.com homepage")]
        public void GivenUserIsOnTheBooking_ComHomepage()
        {
            _homePage = new HomePage(_driver).NavigateTo();
            _homePage.AcceptCookies();
        }

        [Given(@"User perform a Search")]
        public void GivenUserPerformASearch(Table table)
        {
            var bookingInfo = table.CreateInstance<BookingInfo>();

            _homePage.DestinationSearchBox.Search(bookingInfo.Location).Results.FirstOrDefault(x => x.Value.Contains(bookingInfo.Location)).Select();
            _homePage.CalendarControl.FromDate = fromDate;
            _homePage.CalendarControl.ToDate = fromDate.AddDays(1);
            _homePage.SelectGuestsControl.Activate().AdultsControl.SetNumber(bookingInfo.NumberOfPeople);
            _homePage.SelectGuestsControl.Activate().RoomsControl.SetNumber(bookingInfo.NumberOfRooms);
            _homePage.SearchButtonElement.Click();

            _resultsPage = new ResultsPage(_driver);
            _resultsPage.WaitFotPageToLoad();
        }

        [When(@"User filters by (.*) and selects the (.*) filter")]
        public void WhenUserFiltersByAndSelectsTheFilter(string p0, string p1)
        {
            var filter = _resultsPage.Filters.FirstOrDefault(x => x.FilterTitle.ToLower().Contains(p0.ToLower()));
            Assert.NotNull(filter, $"No {p1} section displayed");
            var option = filter.Options.FirstOrDefault(x => x.Option.ToLower().Contains(p1.ToLower()));
            //sauna filter was not displayed in filter options randomly
            Assert.NotNull(option, $"No filter option {p0} in {p1} section");
            option.Selected = true;
        }


        [Then(@"(.*) listed in the results = (.*)")]
        public void ThenListedInTheResults(string p0, string p1)
        {
            bool expectedResult = bool.Parse(p1);
            Assert.AreEqual(expectedResult, _resultsPage.Results.FirstOrDefault(x => x.Name.Contains(p0)) != null, $"Hotel {p0} not displayed when result should have been {p1}");
        }
        

        [AfterScenario()]
        public void AfterScenario()
        {
            _driver?.Dispose();
        }
    }

    public class BookingInfo
    {
        public string Location { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfRooms { get; set; }
    }
}
