namespace Bellatrix.Web.NUnit.Tests.ProductCatalogue.Pages
{
    public partial class ReportPage : Page
    {
        internal Div TableOfContents => Element.CreateByXpath<Div>("//div[text()='Table of Contents']");
        internal Element ClothingCategoryInContent => GetSheetNumber(1).CreateByXpath<Element>("//div[@data-id='trTocItem_1']//*[text()='Clothing']");
        internal Div AccessoriesCategory => Element.CreateByXpath<Div>("//div[@data-id='textBoxProductCategoryLabel_2'][text()='Accessories']");
        internal Div BikesCategory => Element.CreateByXpath<Div>("//div[@data-id='textBoxProductCategoryLabel_3'][text()='Bikes']");
        internal Div ClothingCategory => Element.CreateByXpath<Div>("//div[@data-id='textBoxProductCategoryLabel_4'][text()='Clothing']");
        internal Div ComponentsCategory => Element.CreateByXpath<Div>("//div[@data-id='textBoxProductCategoryLabel_5'][text()='Components']");
        internal Div PageContainer => Element.CreateByClass<Div>("trv-page-container");
        internal Div GetSheetNumber(int page)
        {
            Div pageSheet = Element.CreateByXpath<Div>($"//div[@data-page={page}]");

            return pageSheet;
        }
    }
}
