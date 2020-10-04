using System;
using System.Collections.Generic;
using System.Text;
using Bellatrix.Web.NUnit.Tests.ProductCatalogue.Data;

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
    }
}
