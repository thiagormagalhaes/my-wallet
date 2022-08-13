using AngleSharp.Dom;

namespace MyWallet.Scraper.Extensions
{
    public static class DocumentExtensions
    {
        public static string GetTextBySelector(this IDocument document, string selector)
        {
            var querySelectorAll = document.QuerySelectorAll(selector);

            var text = querySelectorAll.FirstOrDefault();

            return text is not null ? text.TextContent : "";
        }
    }
}
