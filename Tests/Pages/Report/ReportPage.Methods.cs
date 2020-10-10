using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class ReportPage
    {
        internal string GetReportName()
        {
            TreeDocumentName.ToHasContent().WaitToBe();
            string text = TreeDocumentName.InnerText;

            return text;
        }
    }
}
