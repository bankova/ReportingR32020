namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class NavigationPage : AssertedNavigatablePage
    {
        private ReportPage _reportPage = new ReportPage();
        public override string Url => "http://localhost:53895/Html5ReportViewerProduct.html";
        private string _menubarWidgetXPath = "//div[@class='trv-nav k-widget']";
        internal Div NavWidget => Element.CreateByXpath<Div>(_menubarWidgetXPath);
        internal Anchor StopRenderingAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//a[@data-command='telerik_ReportViewer_stopRendering']");
        internal Anchor RefreshAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//a[@data-command='telerik_ReportViewer_refresh']");
        internal Anchor GotoFirstPageAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//a[@data-command='telerik_ReportViewer_goToFirstPage']");
        internal Element GotoFirstPageLi => Element.CreateByXpath<Element>(_menubarWidgetXPath + "//li[@aria-label='First page']");
        internal Element GotoPreviousPageLi => Element.CreateByXpath<Element>(_menubarWidgetXPath + "//li[@aria-label='Previous page']");
        internal Anchor GotoPreviousPageAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//li[@aria-label='Previous page']//a");
        internal Element GotoNextPageLi => Element.CreateByXpath<Element>(_menubarWidgetXPath + "//li[@aria-label='Next page']");
        internal Anchor GotoNextPageAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//li[@aria-label='Next page']//a");
        internal Element GotoLastPageLi => Element.CreateByXpath<Element>(_menubarWidgetXPath + "//li[@aria-label='Last page']");
        internal Anchor GotoLastPageAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//li[@aria-label='Last page']//a");
        internal Number CurrentPageNumber => Element.CreateByXpath<Number>(_menubarWidgetXPath + "//input[@data-role='telerik_ReportViewer_PageNumberInput'][@type='number']");
        internal Span AllPagesCountSpan => Element.CreateByXpath<Span>(_menubarWidgetXPath + "//span[@data-role='telerik_ReportViewer_PageCountLabel']");
        internal Anchor ExportAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//a[@data-command='telerik_ReportViewer_export']");
        internal Anchor TogglePrintPreviewAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//li[@aria-label='Toggle print preview']//a");
        internal Anchor ExportPdfAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//a[@data-command-parameter='PDF']");
        internal Anchor PrintAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//a[@data-command='telerik_ReportViewer_print']");
        internal Anchor ZoomInAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//li[@aria-label='Zoom in']//a");
        internal Anchor ZoomOutAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//li[@aria-label='Zoom out']//a");
        internal Anchor ToggleSearchAnchor => Element.CreateByXpath<Anchor>(_menubarWidgetXPath + "//a[@data-command='telerik_ReportViewer_toggleSearchDialog']");
        internal TextField SearchInput => Element.CreateByClass<TextField>("k-input");
        internal Span SearchResults => Element.CreateByClass<Span>("trv-search-dialog-results-label");
        internal Div TooltipActionMessage => Element.CreateByClass<Div>("trv-error-message");
        internal Element PrintPreviewApp => Element.CreateByXpath<Element>("//print-preview-app");

        internal Div TotalPageLoadedMessage(int totalPages)
        {
            return Element.CreateByXpath<Div>($"//div[text()='Done. Total {totalPages} pages loaded.']");
        }

        internal Anchor ExportAnchorFileType(string fileExtension)
        {
            return Element.CreateByXpath<Anchor>(_menubarWidgetXPath + $"//a[@data-command-parameter='{fileExtension}']");
        }
    }
}
