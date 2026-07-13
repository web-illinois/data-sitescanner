namespace IllinoisSiteScannerWeb.Data.Web {

    internal static class HostingInformation {

        private static Dictionary<string, string> HostIpAddress = new() {
            { "75.2.20.195", "cPanel for Mission Critical Sites" },
            { "18.154.185.54", "cPanel for Mission Critical Sites" },
            { "18.154.185.40", "cPanel for Mission Critical Sites" },
            { "18.154.185.35", "cPanel for Mission Critical Sites" },
            { "18.220.149.166", "cPanel" },
            { "18.160.200.125", "PIE" },
            { "13.59.228.241", "PIE" }
        };

        private static Dictionary<string, string> HostnameAddress = new() {
            { "web.illinois.edu", "cPanel" },
            { "publish.illinois.edu", "PIE" }
        };

        private static Dictionary<string, string> HostnameCms = new() {
            { "Site Manager", "Site Manager" }
        };

        private static Dictionary<string, string> HelpLinksForHosting = new() {
            { "cPanel for Mission Critical Sites", "" },
            { "cPanel", "" },
            { "PIE", "" },
            { "Sitefinity", "" },
            { "Sitemanager", "" }
        };

        internal static string Check(string ip, string hostname, string cms) =>
            HostIpAddress.TryGetValue(ip, out var valueAddress) ? valueAddress :
            HostnameAddress.TryGetValue(hostname, out var valueHostname) ? valueHostname :
            HostnameCms.TryGetValue(cms, out var valueCms) ? valueCms :
            "Other";

        internal static string GetLink(string hostname) => HelpLinksForHosting.TryGetValue(hostname, out var link) ? link : "";

    }
}