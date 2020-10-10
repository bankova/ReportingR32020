namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class NavigationPage : AssertedNavigatablePage
    {
        private ReportPage _reportPage = new ReportPage();
        public override string Url => "http://localhost:53895/Html5ReportViewerProduct.html";
        internal Anchor StopRenderingAnchor => Element.CreateByXpath<Anchor>("//div[@class='trv-nav k-widget']//a[@data-command='telerik_ReportViewer_stopRendering']");
        internal Anchor RefreshAnchor => Element.CreateByXpath<Anchor>("//div[@class='trv-nav k-widget']//a[@data-command='telerik_ReportViewer_refresh']");
        internal Anchor GotoFirstPageAnchor => Element.CreateByXpath<Anchor>("//div[@class='trv-nav k-widget']//a[@data-command='telerik_ReportViewer_goToFirstPage']");
        internal Element GotoFirstPageLi => Element.CreateByXpath<Element>("//li[@aria-label='First page']");
        internal Element GotoPreviousPageLi => Element.CreateByXpath<Element>("//li[@aria-label='Previous page']");
        internal Element GotoNextPageLi => Element.CreateByXpath<Element>("//li[@aria-label='Next page']");
        internal Element GotoLastPageLi => Element.CreateByXpath<Element>("//li[@aria-label='Last page']");
        internal Anchor GotoPreviousPageAnchor => Element.CreateByXpath<Anchor>("//li[@aria-label='Previous page']//a");
        internal Anchor GotoNextPageAnchor => Element.CreateByXpath<Anchor>("//li[@aria-label='Next page']//a");
        internal Anchor GotoLastPageAnchor => Element.CreateByXpath<Anchor>("//li[@aria-label='Last page']//a");
        internal Number CurrentPageNumber => Element.CreateByXpath<Number>("//input[@data-role='telerik_ReportViewer_PageNumberInput'][@type='number']");
        internal Span AllPagesCountSpan => Element.CreateByXpath<Span>("//span[@data-role='telerik_ReportViewer_PageCountLabel']");
        internal Anchor ExportAnchor => Element.CreateByXpath<Anchor>("//div[@class='trv-nav k-widget']//a[@data-command='telerik_ReportViewer_export']");
        internal Anchor TogglePrintPreviewAnchor => Element.CreateByXpath<Anchor>("//div[@class='trv-nav k-widget']//a[@data-command='telerik_ReportViewer_togglePrintPreview']");
        internal Anchor ExportPdfAnchor => Element.CreateByXpath<Anchor>("//a[@data-command-parameter='PDF']");
        internal Anchor PrintAnchor => Element.CreateByXpath<Anchor>("//div[@class='trv-nav k-widget']//a[@data-command='telerik_ReportViewer_print']");
        internal Anchor ToggleSearchAnchor => Element.CreateByXpath<Anchor>("//div[@class='trv-nav k-widget']//a[@data-command='telerik_ReportViewer_toggleSearchDialog']");

        internal TextField SearchInput => Element.CreateByClass<TextField>("k-input");
        internal Span SearchResults => Element.CreateByClass<Span>("trv-search-dialog-results-label");
        internal Div TooltipActionMessage => Element.CreateByClass<Div>("trv-error-message");
        internal Element PrintPreviewApp => Element.CreateByXpath<Element>("//print-preview-app");

        internal Div TotalPageLoadedMessage(int totalPages)
        {
            return Element.CreateByXpath<Div>($"//div[text()='Done. Total {totalPages} pages loaded.']");
        }
    }
}
