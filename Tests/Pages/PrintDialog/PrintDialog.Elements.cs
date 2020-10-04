namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class PrintDialog : Page
    {
        internal Frame PrintFrame => Element.CreateById<Frame>("pdf-viewer");
        internal Div PrintHeader => PrintFrame.CreateById<Div>("headerContainer");
    }
}
