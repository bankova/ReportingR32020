using System;
using System.Collections.Generic;
using System.Text;
using Bellatrix.Utilities;
using Bellatrix.Web;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;

namespace ReportViewer.Tests.Common
{
    internal static class ElementExtensions
    {
        internal static void ClickVisibleAnchor(this Anchor anchor)
        {
            anchor.ToBeVisible().ToBeClickable().WaitToBe();

            anchor.Click();
        }

        internal static void MouseClick(this Element element)
        {
            element.ToBeVisible().ToBeClickable().WaitToBe();

            var action = new Actions(element.WrappedDriver);
            action.MoveToElement(element.WrappedElement).Click().Perform();
        }

        internal static void WaitUntilIsDisabled(this Element element)
        {
            string actualClass = element.CssClass;
            string expectedClass = "disabled";

            Func<bool> containsDisabledClass = () => actualClass.Contains(expectedClass);
            Wait.Until(containsDisabledClass);
        }
    }
}
