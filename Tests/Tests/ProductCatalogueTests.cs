using System;
using System.Linq;
using Bellatrix.Assertions;
using Bellatrix.Web.NUnit.Tests.ProductCatalogue.Data;
using Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Bellatrix.Web.NUnit.Tests
{
    [TestFixture]
    [Browser(BrowserType.Chrome, BrowserBehavior.ReuseIfStarted)]
    [VideoRecording(VideoRecordingMode.DoNotRecord)]
    [ScreenshotOnFail(true)]
    public class ProductCatalogueTests : WebTest
    {
        private NavigationPage _navigationPage;
        private PrintDialog _printPage;
        public override void TestInit()
        {
            _navigationPage = App.GoTo<NavigationPage>();
            _printPage = new PrintDialog();

            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountViewer);
        }

        [Test]
        public void Export_Should()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\" + "ProductCatalog.en.pdf";
            System.IO.File.Delete(path);
            Bellatrix.Assertions.Assert.IsFalse(System.IO.File.Exists(path));

            App.InteractionsService.SendKeys(_navigationPage.ExportAnchor, Keys.Space).Perform();
            _navigationPage.ExportPdf.ToBeClickable().ToBeVisible().Click();
            _navigationPage.AssertExportMessage();

            System.Threading.Thread.Sleep(10000);
            Bellatrix.Assertions.Assert.IsTrue(System.IO.File.Exists(path));
        }

        [Test]
        public void TogglePrintPreview_Should()
        {
            _navigationPage.TogglePrintPreviewAnchor.ToBeClickable().ToBeVisible().WaitToBe();
            _navigationPage.TogglePrintPreviewAnchor.Click();
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountPrintPreview);
        }

        [Test]
        public void Print_ShouldOpenDialog()
        {
            _navigationPage.AssertWindowCount(1);

            _navigationPage.TooltipActionMessage.EnsureInnerTextIsNot(Messages.PrintPrepareDocumentMessage);
            System.Threading.Thread.Sleep(10000);
            _navigationPage.PrintAnchor.ToBeClickable().Click();
            _navigationPage.TooltipActionMessage.EnsureInnerTextIs(Messages.PrintPrepareDocumentMessage);

            _navigationPage.WaitForNewBrowserWindowToConnect();
            _navigationPage.AssertWindowCount(2);
            Screen.EnsureIsVisible("PrintPreviewChrome", similarity: 0.7, timeoutInSeconds: 10);
        }

        [Test]
        public void Refresh_Should()
        {
            _navigationPage.RefreshAnchor.ToBeClickable().ToBeVisible().WaitToBe();
            _navigationPage.RefreshAnchor.Click();
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountViewer);
        }

        [Test]
        public void PageNavigation_Should()
        {
            _navigationPage.AssertFirstPage();

            _navigationPage.GotoNextPageAnchor.ToBeClickable().Click();

            _navigationPage.AssertCurrentPageNumberIs(2);
            _navigationPage.GotoLastPageAnchor.ToBeClickable().Click();

            _navigationPage.AssertLastPage(5);

            _navigationPage.CurrentPageNumber.SetNumber(1);
            App.InteractionsService.SendKeys(Keys.Enter).Perform();
            _navigationPage.AssertCurrentPageNumberIs(1);
        }

        [Test]
        public void PageNavigation_Invalid()
        {
            _navigationPage.CurrentPageNumber.ToBeVisible();

            _navigationPage.AssertPageInvalid(6, 5);
            _navigationPage.AssertPageInvalid(0, 1);
        }

        [Test]
        public void CancelReportProcessing_Should()
        {
            _navigationPage.TogglePrintPreviewAnchor.ToBeClickable().ToBeVisible().WaitToBe();
            _navigationPage.TogglePrintPreviewAnchor.Click();
            _navigationPage.StopRenderingAnchor.Click();
            _navigationPage.AssertStopRenderingMessage();
        }

        [Test]
        public void SearchPrintPreview_Should()
        {
            _navigationPage.TogglePrintPreviewAnchor.ToBeClickable().ToBeVisible().WaitToBe();
            _navigationPage.TogglePrintPreviewAnchor.Click();
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountPrintPreview);

            _navigationPage.ToggleSearchAnchor.ToBeClickable().ToBeVisible().Click();
            _navigationPage.SearchInput.ToBeClickable();

            _navigationPage.SearchResults.EnsureInnerTextContains("No results");
            _navigationPage.SearchInput.SetText("Weatherproof");
            _navigationPage.SearchResults.EnsureInnerTextContains("Result 1 of 3");
        }
    }
}
