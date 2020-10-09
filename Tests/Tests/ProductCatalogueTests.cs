using System;
using System.Linq;
using Bellatrix.Layout;
using Bellatrix.Utilities;
using Bellatrix.Web.NUnit.Tests.ProductCatalogue.Data;
using Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using ReportViewer.Tests.Common;

namespace Bellatrix.Web.NUnit.Tests
{
    [TestFixture]
    [Browser(BrowserType.Chrome, BrowserBehavior.ReuseIfStarted)]
    public class ProductCatalogueTests : WebTest
    {
        private NavigationPage _navigationPage;
        private ReportPage _reportPage;
        private PrintDialog _printPage;
        public override void TestInit()
        {
            _navigationPage = App.GoTo<NavigationPage>();
            _reportPage = new ReportPage();
            _printPage = new PrintDialog();

            _navigationPage.WaitMessageLoadedPagesVisible(Messages.TotalPageCountViewer);
            _navigationPage.WaitMessageLoadedPagesNotVisible(Messages.TotalPageCountViewer);
        }

        [Test]
        public void Export_Should()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\ProductCatalog.en.pdf";
            System.IO.File.Delete(path);
            Assert.IsFalse(System.IO.File.Exists(path));

            App.InteractionsService.SendKeys(_navigationPage.ExportAnchor, Keys.Space).Perform();
            _navigationPage.ExportPdfAnchor.ClickVisibleAnchor();
            _navigationPage.AssertExportMessage();

            Wait.Until(() => System.IO.File.Exists(path));
        }

        [Test]
        public void TogglePrintPreview_Should()
        {
            _navigationPage.TogglePrintPreviewAnchor.ClickVisibleAnchor();
            _navigationPage.WaitMessageLoadedPagesVisible(Messages.TotalPageCountPrintPreview);
        }

        [Test]
        public void Print_ShouldOpenDialog()
        {
            _navigationPage.AssertWindowCount(1);

            _navigationPage.TooltipActionMessage.EnsureInnerTextIsNot(Messages.PrintPrepareDocumentMessage);
            _navigationPage.PrintAnchor.ClickVisibleAnchor();
            _navigationPage.TooltipActionMessage.EnsureInnerTextIs(Messages.PrintPrepareDocumentMessage);

            _navigationPage.WaitForNewBrowserWindowToConnect();
            _navigationPage.AssertWindowCount(2);
            ////App.BrowserService.SwitchToFrame(_printPage.PrintFrame);
            ////_printPage.PrintHeader.EnsureInnerHtmlContains("22");
            ////Screen.EnsureIsVisible("PrintPreviewChrome", similarity: 0.7, timeoutInSeconds: 20);
        }

        [Test]
        public void Refresh_Should()
        {
            _navigationPage.AssertFirstPage();

            _navigationPage.GotoNextPageAnchor.ClickVisibleAnchor();
            _navigationPage.AssertCurrentPageNumberIs(2);

            _navigationPage.RefreshAnchor.ClickVisibleAnchor();
            _navigationPage.WaitMessageLoadedPagesVisible(Messages.TotalPageCountViewer);

            _navigationPage.AssertFirstPage();
        }

        [Test]
        public void PageNavigation_Should()
        {
            _navigationPage.AssertFirstPage();

            _navigationPage.GotoNextPageAnchor.ClickVisibleAnchor();

            _navigationPage.AssertCurrentPageNumberIs(2);
            _navigationPage.GotoLastPageAnchor.Click();

            _navigationPage.AssertLastPage(5);

            _navigationPage.SetPage(1);
            _navigationPage.AssertFirstPage();
        }

        [Test]
        public void PageNavigationPrintPreview_Should()
        {
            _navigationPage.TogglePrintPreviewAnchor.ClickVisibleAnchor();
            _navigationPage.WaitMessageLoadedPagesVisible(Messages.TotalPageCountPrintPreview);

            _navigationPage.AssertFirstPage();
            _navigationPage.GotoLastPageAnchor.Click();
            _navigationPage.AssertLastPage(22);
        }

        [Test]
        public void PageNavigation_Invalid()
        {
            _navigationPage.CurrentPageNumber.ToBeVisible().WaitToBe();

            _navigationPage.AssertPageInvalid(6, 5);
            _navigationPage.AssertPageInvalid(0, 1);
        }

        [Test]
        public void PagesHaveContent_Should()
        {
            _reportPage.GetSheetNumber(1).EnsureInnerHtmlContains("Table of Contents");
            for (int i = 2; i <= Messages.TotalPageCountViewer; i++)
            {
                _navigationPage.GotoNextPageAnchor.ClickVisibleAnchor();
                _reportPage.GetSheetNumber(i).EnsureInnerHtmlContains("List Price");
                Console.WriteLine(i);
            }
        }

        [Test]
        public void CancelReportProcessing_Should()
        {
            _navigationPage.TogglePrintPreviewAnchor.ClickVisibleAnchor();
            _navigationPage.StopRenderingAnchor.ClickVisibleAnchor();

            _navigationPage.AssertStopRenderingMessage();
            _reportPage.PageContainer.EnsureInnerTextIs(string.Empty);
            _navigationPage.AssertCurrentPageNumberIs(0);
        }

        [Test]
        public void SearchPrintPreview_Should()
        {
            string searchText = "Weatherproof";
            _navigationPage.TogglePrintPreviewAnchor.ClickVisibleAnchor();
            _navigationPage.WaitMessageLoadedPagesVisible(Messages.TotalPageCountPrintPreview);

            _navigationPage.ToggleSearchAnchor.ClickVisibleAnchor();
            _navigationPage.SearchInput.ToBeClickable().WaitToBe();

            _navigationPage.SearchResults.EnsureInnerTextContains("No results");
            _navigationPage.SearchInput.SetText(searchText);
            _navigationPage.SearchResults.EnsureInnerTextContains("Result 1 of 3");
            _navigationPage.AssertCurrentPageNumberIs(2);
        }
    }
}
