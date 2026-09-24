using HtmlAgilityPack;

namespace IllinoisSiteScannerWeb.Data.Web {
    internal class OwnerInformation {
        internal static string GetOwner(HtmlDocument doc) {
            var returnValueOwner = "";
            var footerAddress = doc.DocumentNode.SelectSingleNode("//ilw-footer//*[@slot='address']");

            if (footerAddress != null) {
                returnValueOwner = footerAddress.InnerText;
            }
            return string.IsNullOrWhiteSpace(returnValueOwner) ? "No owner found" : returnValueOwner;
        }
    }
}
