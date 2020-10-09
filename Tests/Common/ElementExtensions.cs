using System;
using System.Collections.Generic;
using System.Text;
using Bellatrix.Utilities;
using Bellatrix.Web;
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

        internal static void AssertIsDisabled(this Element element)
        {
            string actualClass = element.CssClass;
            string expectedClass = "disabled";
            bool containsDisabledClass = actualClass.Contains(expectedClass);
            Wait.Until(() => containsDisabledClass);
        }
    }
}
