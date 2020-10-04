using System;
using System.Collections.Generic;
using System.Text;

namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class NavigationPage
    {
        public void AssertCurrentPageNumberIs(int expectedPage)
        {
            System.Threading.Thread.Sleep(200);
            int actualPage = (int)CurrentPageNumber.GetNumber();
            Assert.AreEqual(expectedPage, actualPage);
        }
    }
}
