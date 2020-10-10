using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using OpenQA.Selenium;

namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class NavigationPage
    {
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

            while (countofWindows != expected)
            {
                System.Threading.Thread.Sleep(100);
                countofWindows = GetWindowCount();
            }
        }

        internal void SetPage(int pageNumber)
        {
            InteractionsService service = new InteractionsService();

            CurrentPageNumber.SetNumber(pageNumber);
            CurrentPageNumber.Focus();
            service.SendKeys(Keys.Enter).Perform();
        }
    }
}
