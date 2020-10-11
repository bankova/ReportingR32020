using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Bellatrix.Utilities;
using OpenQA.Selenium;

namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class NavigationPage : AssertedNavigatablePage
    {
        private ReportPage _reportPage = new ReportPage();
        private PrintPage _printPage = new PrintPage();
        public override string Url => "http://localhost:53895/Html5ReportViewerProduct.html";

        internal int GetWindowCount()
        {
            object[] allWindows = Browser.WrappedDriver.WindowHandles.ToArray();
            return allWindows.Length;
        }

        internal void SwitchToLastWindow()
        {
            Browser.WrappedDriver.SwitchTo().Window(Browser.WrappedDriver.WindowHandles.Last());
        }

        internal void WaitForWindowCountToBe(int expected = 2)
        {
            int countofWindows = GetWindowCount();

            Func<bool> expectedWindowsCountFound = () =>
            {
                bool expectedEqualsActual = countofWindows == expected;
                System.Threading.Thread.Sleep(100);
                countofWindows = GetWindowCount();

                return expectedEqualsActual;
            };

            Wait.Until(expectedWindowsCountFound);
        }

        internal void SetPage(int pageNumber)
        {
            InteractionsService service = new InteractionsService();

            CurrentPageNumber.SetNumber(pageNumber);
            CurrentPageNumber.Focus();
            service.SendKeys(Keys.Enter).Perform();
        }

        internal int GetLastPage()
        {
            AllPagesCountSpan.ToBeVisible().WaitToBe();
            int actual = int.Parse(AllPagesCountSpan.InnerText);

            return actual;
        }
    }
}
