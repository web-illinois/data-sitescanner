using HtmlAgilityPack;

namespace IllinoisSiteScannerWeb.Data.Web {

    internal static class HeaderInformation {

        internal static string Check(HtmlDocument doc) {
            var headers = doc.DocumentNode.SelectNodes("//h1");
            if (headers == null || headers.Count() == 0) {
                return "No H1 found";
            }
            return string.Join(" / ", headers.Select(h => h.InnerText));
        }
    }
}