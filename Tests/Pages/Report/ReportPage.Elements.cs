namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class ReportPage : Page
    {
        internal Anchor RefreshAnchor => Element.CreateByXpath<Anchor>("//div[@class='trv-nav k-widget']//a[@data-command='telerik_ReportViewer_refresh']");
    }
}
