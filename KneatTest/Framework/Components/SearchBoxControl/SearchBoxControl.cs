using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace KneatTest.Framework.Components.SearchBoxControl
{
    public class SearchBoxControl : BaseControl
    {
        public SearchBoxControl(IWebElement referenceElement) : base(referenceElement) { }

        public IWebElement SearchBoxElement => WrappedElement.FindElement(By.CssSelector("input[data-component*='search/destination/input']"));
        //Todo 
        //Input for sending keys
        //Results container

        protected IWebElement ResultsContainerElement => WrappedElement.FindElement(By.CssSelector("ul.c-autocomplete__list[role='listbox']"));


        public SearchBoxResultsContainer Search(string searchTerm)
        {
            var wait = new WebDriverWait(WrappedDriver, TimeSpan.FromSeconds(3));
            SearchBoxElement.Clear();
            SearchBoxElement.SendKeys(searchTerm);
            //wait for searchresults container to displayed
            WaitForElementToBeDisplayed(By.CssSelector("ul.c-autocomplete__list[role='listbox']"));


            return new SearchBoxResultsContainer(ResultsContainerElement);
        }

    }

    public class SearchBoxResultsContainer : BaseControl
    {
        public SearchBoxResultsContainer(IWebElement referenceElement) : base(referenceElement)
        { }

        public List<SearchBoxResultItem> Results => WrappedElement.FindElements(By.CssSelector("li.c-autocomplete__item[role='option']")).Select(x => new SearchBoxResultItem(x)).ToList();
    }

    public class SearchBoxResultItem : BaseControl, ISearchResultItem
    {
        public SearchBoxResultItem(IWebElement referenceElement) : base(referenceElement) { }

        public void Select()
        {
            WrappedElement.Click();
        }

        public virtual string Type
        {
            get
            {
                var classAttribute = WrappedElement.GetAttribute("class");
                if (classAttribute.Contains("city"))
                    return "City";
                if (classAttribute.Contains("region"))
                    return "Region";
                if (classAttribute.Contains("hotel"))
                    return "Hotel";
                if (classAttribute.Contains("airport"))
                    return "Airport";
                return "Region";
            }

        }
        public virtual string Value => WrappedElement.GetAttribute("data-label");

    }

    public interface ISearchResultItem
    {
        void Select();
        string Type { get; }//Hotel,City,Region,Airport
        string Value { get; }
    }
}
