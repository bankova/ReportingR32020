using System;
using System.Collections.Generic;
using System.Text;
using Bellatrix.Web.NUnit.Tests.ProductCatalogue.Data;
using OpenQA.Selenium;

namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class NavigationPage
    {
        internal void AssertMessageLoadedPageNumber(int totalPagesCount)
        {
            TotalPageLoadedMessage(totalPagesCount).ToBeVisible().WaitToBe();
        }

        internal void AssertExportMessage()
        {
            TooltipActionMessage.EnsureInnerTextIs("Preparing document to download. Please wait...");
        }

        internal void AssertStopRenderingMessage()
        {
            TooltipActionMessage.EnsureInnerTextIs(Messages.ReportProcessingCanceled);
        }

        internal void AssertCurrentPageNumberIs(int expectedPage)
        {
            System.Threading.Thread.Sleep(200);
            int actualPage = (int)CurrentPageNumber.GetNumber();
            Assert.AreEqual(expectedPage, actualPage);
        }

        internal void AssertFirstPage()
        {
            AssertCurrentPageNumberIs(1);

            GotoFirstPageAnchor.ToBeDisabled();
            GotoPreviousPageAnchor.ToBeDisabled();
        }

        internal void AssertLastPage(int number = 5)
        {
            AssertCurrentPageNumberIs(number);

            GotoLastPageAnchor.ToBeDisabled();
            GotoNextPageAnchor.ToBeDisabled();
        }

        internal void AssertPageInvalid(int pageNumber, int expectedFinalPage)
        {
            InteractionsService service = new InteractionsService();

            CurrentPageNumber.SetNumber(pageNumber);
            CurrentPageNumber.Focus();
            service.SendKeys(Keys.Enter).Perform();
            AssertCurrentPageNumberIs(expectedFinalPage);
        }

        internal void AssertWindowCount(int expected)
        {
            int actual = GetWindowCount();
            Assert.AreEqual(expected, actual);
        }
    }
}
