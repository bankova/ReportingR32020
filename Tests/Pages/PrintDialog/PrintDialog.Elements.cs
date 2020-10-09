namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class PrintDialog : Page
    {
        internal Frame PrintFrame => Element.CreateById<Frame>("pdf-viewer");
        internal Div PrintHeader => Element.CreateById<Div>("header");

        internal Element PrintPreviewApp => Element.CreateByXpath<Element>("//print-preview-app");
    }
}
