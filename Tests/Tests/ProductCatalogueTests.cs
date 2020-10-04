using System;
using System.Linq;
using Bellatrix.Assertions;
using Bellatrix.Web.NUnit.Tests.ProductCatalogue.Data;
using Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

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
        }

        [Test]
        public void Export_Should()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\" + "ProductCatalog.en.pdf";
            System.IO.File.Delete(path);
            Bellatrix.Assertions.Assert.IsFalse(System.IO.File.Exists(path));

            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountViewer);
            App.InteractionsService.SendKeys(_navigationPage.ExportAnchor, Keys.Space).Perform();
            _navigationPage.ExportPdf.ToBeClickable().ToBeVisible().Click();
            _navigationPage.AssertExportMessage();

            System.Threading.Thread.Sleep(10000);
            Bellatrix.Assertions.Assert.IsTrue(System.IO.File.Exists(path));
        }

        [Test]
        public void TogglePrintPreview_Should()
        {
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountViewer);
            _navigationPage.TogglePrintPreviewAnchor.ToBeClickable().ToBeVisible().WaitToBe();
            _navigationPage.TogglePrintPreviewAnchor.Click();
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountPrintPreview);
        }

        [Test]
        [Browser(BrowserType.Firefox, BrowserBehavior.RestartEveryTime)]
        public void PrintFirefox_ShouldOpenDialog()
        {
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountViewer);
            _navigationPage.PrintAnchor.ToBeClickable().ToBeVisible().WaitToBe();

            Screen.EnsureIsNotVisible("PrintProductCatalogueFF", similarity: 0.7, timeoutInSeconds: 30);
            _navigationPage.TooltipActionMessage.EnsureInnerTextIsNot(Messages.PrintPrepareDocumentMessage);

            _navigationPage.PrintAnchor.Click();

            _navigationPage.TooltipActionMessage.EnsureInnerTextIs(Messages.PrintPrepareDocumentMessage);
            Screen.EnsureIsVisible("PrintProductCatalogueFF", similarity: 0.7, timeoutInSeconds: 30);
        }

        [Test]
        [Browser(BrowserType.Chrome)]
        public void Print_ShouldOpenDialog()
        {
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountViewer);
            _navigationPage.PrintAnchor.ToBeClickable().ToBeVisible().WaitToBe();

            _navigationPage.TooltipActionMessage.EnsureInnerTextIsNot(Messages.PrintPrepareDocumentMessage);
            _navigationPage.PrintAnchor.Click();
            App.BrowserService.WaitUntilReady();

            //// _navigationPage.TooltipActionMessage.EnsureInnerTextIs(Messages.PrintPrepareDocumentMessage);
            ////App.BrowserService.WrappedDriver.SwitchTo().Alert();
            ////App.BrowserService.WrappedDriver.SwitchTo().Window("Print");
            object[] allWindows = App.BrowserService.WrappedDriver.WindowHandles.ToArray();
            App.BrowserService.WrappedDriver.SwitchTo().Window(allWindows[1].ToString());
            ////var windowsList = App.BrowserService.WrappedDriver.WindowHandles.ToList();
            ////App.BrowserService.WrappedDriver.SwitchTo().Window(windowsList.FirstOrDefault());
            ////var list = App.BrowserService.WrappedDriver.WindowHandles;
            string source = App.BrowserService.HtmlSource;
            ////Assert.IsTrue(source.Contains("22"));
            _printPage.PrintHeader.EnsureInnerHtmlContains(Messages.TotalPageCountPrintPreview.ToString());
            ////App.BrowserService.SwitchToDefault();
        }

        [Test]
        public void Refresh_Should()
        {
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountViewer);
            _navigationPage.RefreshAnchor.ToBeClickable().ToBeVisible().WaitToBe();
            _navigationPage.RefreshAnchor.Click();
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountViewer);
        }

        [Test]
        public void PageNavigation_Should()
        {
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountViewer);
            _navigationPage.GotoFirstPageAnchor.ToBeDisabled();
            _navigationPage.GotoPreviousPageAnchor.ToBeDisabled();
            _navigationPage.AssertCurrentPageNumberIs(1);
            _navigationPage.GotoNextPageAnchor.ToBeClickable().Click();
            _navigationPage.AssertCurrentPageNumberIs(2);
            _navigationPage.GotoLastPageAnchor.ToBeClickable().Click();
            _navigationPage.AssertCurrentPageNumberIs(5);
            _navigationPage.GotoLastPageAnchor.ToBeDisabled();
            _navigationPage.GotoNextPageAnchor.ToBeDisabled();
            _navigationPage.CurrentPageNumber.SetNumber(1);
            ////_navigationPage.CurrentPageNumber.Focus();
            App.InteractionsService.SendKeys(Keys.Enter).Perform();
            _navigationPage.AssertCurrentPageNumberIs(1);
        }

        ////[TestCase(0, 1)]
        [Test]
        public void PageNavigation_Invalid() ////int pageNumber, int expectedPage
        {
            _navigationPage = App.GoTo<NavigationPage>();
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountViewer);
            _navigationPage.CurrentPageNumber.ToBeVisible();
            _navigationPage.CurrentPageNumber.SetNumber(6);
            _navigationPage.CurrentPageNumber.Focus();
            App.InteractionsService.SendKeys(Keys.Enter).Perform();
            _navigationPage.AssertCurrentPageNumberIs(5);

            _navigationPage.CurrentPageNumber.SetNumber(0);
            _navigationPage.CurrentPageNumber.Focus();
            App.InteractionsService.SendKeys(Keys.Enter).Perform();
            _navigationPage.AssertCurrentPageNumberIs(1);
        }

        [Test]
        public void CancelReportProcessing_Should()
        {
            _navigationPage.AssertMessageLoadedPageNumber(Messages.TotalPageCountViewer);
            _navigationPage.TogglePrintPreviewAnchor.ToBeClickable().ToBeVisible().WaitToBe();
            _navigationPage.TogglePrintPreviewAnchor.Click();
            _navigationPage.StopRenderingAnchor.Click();
            _navigationPage.AssertStopRenderingMessage();
        }
    }
}