using HtmlAgilityPack;

namespace IllinoisSiteScannerWeb.Data.Web {
    internal static class AttributeInformation {

        private const string OwnerAttribute = "data-owner";
        private const string HostAttribute = "data-host";

        internal static (string owner, string host) CheckAttributes(HtmlDocument doc) {
            var returnValueOwner = "";
            var returnValueHost = "";
            var head = doc.DocumentNode.SelectSingleNode("//head");

            if (head != null) {
                returnValueOwner = head.GetAttributeValue(OwnerAttribute, "");
                returnValueHost = head.GetAttributeValue(HostAttribute, "");
            }
            var body = doc.DocumentNode.SelectSingleNode("//body");

            if (body != null) {
                if (string.IsNullOrEmpty(returnValueOwner)) {
                    returnValueOwner = body.GetAttributeValue(OwnerAttribute, "");
                }
                if (string.IsNullOrEmpty(returnValueHost)) {
                    returnValueHost = body.GetAttributeValue(HostAttribute, "");
                }
            }
            return (string.IsNullOrWhiteSpace(returnValueOwner) ? "No owner found" : returnValueOwner, returnValueHost);
        }
    }
}
