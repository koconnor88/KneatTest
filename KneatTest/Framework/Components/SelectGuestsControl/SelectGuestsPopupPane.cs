using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KneatTest.Framework.Components.SelectGuestsControl
{
    public class SelectGuestsPopupPane : PagePane
    {
        public SelectGuestsPopupPane(IWebElement referenceElement) : base(referenceElement) { }

        public SelectGuestNumberControl AdultsControl => new SelectGuestNumberControl(WrappedElement.FindElement(By.CssSelector(".sb-group__field-adults")));

        public SelectGuestNumberControl ChildrenControl => new SelectGuestNumberControl(WrappedElement.FindElement(By.CssSelector(".sb-group-children")));

        public SelectGuestNumberControl RoomsControl => new SelectGuestNumberControl(WrappedElement.FindElement(By.CssSelector(".sb-group__field-rooms")));

    }
}
