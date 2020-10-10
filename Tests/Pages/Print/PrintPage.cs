using System;
using System.Collections.Generic;
using System.Text;
using Bellatrix.Assertions;

namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public class PrintPage : Page
    {
        internal Element PrintPreviewApp => Element.CreateByXpath<Element>("//print-preview-app");

        internal void AssertPrintPagesCountText(string expectedText = "22 sheets of paper")
        {
            var actualText = new JavaScriptService().Execute("return document.querySelector('body>print-preview-app').shadowRoot.querySelector('#sidebar').shadowRoot.querySelector('print-preview-header').shadowRoot.querySelector('span.summary').textContent;");
            Assert.AreEqual(expectedText, actualText);
        }
    }
}
