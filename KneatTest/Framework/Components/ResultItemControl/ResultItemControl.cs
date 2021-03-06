﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KneatTest.Framework.Components.ResultItemControl
{
    public class ResultItemControl : BaseControl
    {
        public ResultItemControl(IWebElement referenceElement) : base(referenceElement) { }

        public IWebElement NameElement => WrappedElement.FindElement(By.CssSelector(".sr-hotel__name"));

        //Only need the Name for the purposes of this exercise
        public string Name => NameElement.Text;
    }
}
