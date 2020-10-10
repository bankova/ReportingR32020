﻿using System;
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
            TooltipActionMessage.EnsureInnerTextIs("Preparing document to download. Please wait...");
        }

        internal void AssertStopRenderingMessage()
        {
            TooltipActionMessage.EnsureInnerTextIs(Messages.ReportProcessingCanceled);
        }

        internal void AssertCurrentPageNumberIs(int expectedPage)
        {
            ////System.Threading.Thread.Sleep(200);
            int actualPage = (int)CurrentPageNumber.GetNumber();
            bool areEqual = expectedPage == actualPage;

            Wait.Until(() => areEqual);
            Assert.AreEqual(expectedPage, actualPage);

            if (expectedPage > 1)
            {
                _reportPage.GetSheetNumber(expectedPage).EnsureInnerHtmlContains("List Price");
            }
        }

        internal void AssertFirstPage()
        {
            AssertCurrentPageNumberIs(1);

            _reportPage.GetSheetNumber(1).EnsureInnerHtmlContains("Table of Contents");

            GotoFirstPageLi.AssertIsDisabled();
            GotoPreviousPageLi.AssertIsDisabled();
        }

        internal void AssertLastPage(int number = 5)
        {
            AssertCurrentPageNumberIs(number);

            GotoLastPageLi.AssertIsDisabled();
            GotoNextPageLi.AssertIsDisabled();
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
