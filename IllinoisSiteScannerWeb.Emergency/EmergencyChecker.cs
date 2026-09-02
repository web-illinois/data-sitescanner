using System.Xml.Linq;

namespace IllinoisSiteScannerWeb.Emergency {

    public static class EmergencyChecker {

        private static string EmergencyUrl => "https://content.getrave.com/cap/uiuc/channel1";

        public static Alert Check() {
            var node = XElement.Load(EmergencyUrl);
            var info = node.Descendants().FirstOrDefault(n => n.Name.LocalName == "info");
            if (info == null) {
                return new Alert();
            }
            var alert = new Alert {
                ResponseType = info.GetNodeValue("responseType") ?? "",
                Title = info.GetNodeValue("headline") ?? "",
                Description = info.GetNodeValue("description") ?? ""
            };
            return alert.IsSafe ? new Alert() : alert;
        }

        private static string? GetNodeValue(this XElement element, string name) {
            return element.Descendants().FirstOrDefault(n => n.Name.LocalName == name)?.Value;
        }
    }
}