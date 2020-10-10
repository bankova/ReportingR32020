namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class ReportPage : Page
    {
        internal Element ClothingCategoryInContent => GetDataPageNumber(1).CreateByXpath<Element>("//div[@data-id='trTocItem_1']//*[text()='Clothing']");
        internal Div PageContainer => Element.CreateByClass<Div>("trv-page-container");
        internal Element TreeViewList => Element.CreateByXpath<Element>("//ul[@class='k-group k-treeview-lines']");
        internal Span TreeDocumentName => TreeViewList.CreateByXpath<Span>("//span[@class='k-in']");
        internal Element TreeExpandArrow => TreeViewList.CreateByXpath<Element>("//span[@class='k-icon k-i-expand']");
        internal Element TreeBikesCategory => TreeViewList.CreateByXpath<Element>("//li//span[text()='Bikes']");
        internal Div GetDataPageNumber(int page)
        {
            Div pageSheet = Element.CreateByXpath<Div>($"//div[@data-page={page}]");

            return pageSheet;
        }

        internal Div GetSheetNumber(int page)
        {
            Div pageSheet = Element.CreateByXpath<Div>($"//div[@class='sheet page{page}']");

            return pageSheet;
        }
    }
}
