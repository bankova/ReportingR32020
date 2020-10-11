using System;
using System.Collections.Generic;
using System.Text;
using Bellatrix.Assertions;

namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public class PrintPage : Page
    {
        internal Element PrintPreviewApp => Element.CreateByXpath<Element>("//print-preview-app");

        internal void AssertPrintPagesCountText(int pagesCount = 22)
        {
            string sheetsofPaperText = " sheets of paper";
            int expectedPagesCountIfPrintOnBothSides = pagesCount / 2;
            var actualText = new JavaScriptService().Execute("return document.querySelector('body>print-preview-app').shadowRoot.querySelector('#sidebar').shadowRoot.querySelector('print-preview-header').shadowRoot.querySelector('span.summary').textContent;");
            string expectedIfPrintingOnOneSideOnly = pagesCount + sheetsofPaperText;
            string expectedIfPrintingOnBothSides = expectedPagesCountIfPrintOnBothSides + sheetsofPaperText;

            bool areEqual = expectedIfPrintingOnOneSideOnly.Equals(actualText) || expectedIfPrintingOnBothSides.Equals(actualText);
            Assert.IsTrue(areEqual, "Page sheets text different that expected");
        }
    }
}
