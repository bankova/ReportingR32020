using System;
using System.Collections.Generic;
using System.Text;
using Bellatrix.Utilities;
using Bellatrix.Web.NUnit.Tests.ProductCatalogue.Data;
using OpenQA.Selenium;
using ReportViewer.Tests.Common;

namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class NavigationPage
    {
        internal void WaitMessageLoadedPagesVisible(int totalPagesCount)
        {
            TotalPageLoadedMessage(totalPagesCount).ToBeVisible().WaitToBe();
        }

        internal void WaitMessageLoadedPagesNotVisible(int totalPagesCount)
        {
            TotalPageLoadedMessage(totalPagesCount).ToNotBeVisible().WaitToBe();
        }

        internal void AssertExportMessage()
        {
            TooltipActionMessage.EnsureInnerTextIs(Messages.PrepareDocumentToDownload);
        }

        internal void AssertStopRenderingMessage()
        {
            TooltipActionMessage.EnsureInnerTextIs(Messages.ReportProcessingCanceled);
        }

        internal void AssertCurrentPageNumberIs(int expectedPage)
        {
            bool pageNumbersAreEqual()
            {
                int actualPage = (int)CurrentPageNumber.GetNumber();
                return expectedPage == actualPage;
            }

            Wait.Until(pageNumbersAreEqual);

            if (expectedPage > 1)
            {
                _reportPage.GetDataPageNumber(expectedPage).EnsureInnerHtmlContains("List Price");
            }
        }

        internal void AssertFirstPage()
        {
            AssertCurrentPageNumberIs(1);

            _reportPage.GetDataPageNumber(1).EnsureInnerHtmlContains("Table of Contents");

            GotoFirstPageLi.WaitUntilIsDisabled();
            GotoPreviousPageLi.WaitUntilIsDisabled();
        }

        internal void AssertLastPage(int number = 5)
        {
            AssertCurrentPageNumberIs(number);

            GotoLastPageLi.WaitUntilIsDisabled();
            GotoNextPageLi.WaitUntilIsDisabled();
        }

        internal void AssertPageInvalid(int pageNumber, int expectedFinalPage)
        {
            SetPage(pageNumber);
            AssertCurrentPageNumberIs(expectedFinalPage);
        }

        internal void AssertWindowCount(int expected)
        {
            int actual = GetWindowCount();
            Assert.AreEqual(expected, actual);
        }
    }
}
