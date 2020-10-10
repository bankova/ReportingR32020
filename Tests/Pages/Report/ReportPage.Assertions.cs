using System;
using System.Collections.Generic;
using System.Text;
using Bellatrix.Web.NUnit.Tests.ProductCatalogue.Data;

namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class ReportPage
    {
        internal void AssertExpectedScaleStyle(double expectedScale)
        {
            GetSheetNumber(1).EnsureStyleContains($"transform: scale({expectedScale}, {expectedScale});");
        }
    }
}
