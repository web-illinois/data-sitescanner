using System.Text.RegularExpressions;

namespace IllinoisSiteScannerWeb.Data.Web {

    internal static partial class ToolkitInformation {

        internal static (string oldItem, string newItem) Check(string html) {
            var oldItem = "N/A";
            var newItem = "N/A";
            var matches = Regex.Matches(html, "toolkit.illinois.edu\\/(.*?)\\/toolkit");
            foreach (Match match in matches) {
                if (match.Groups[1].Value.StartsWith("2")) {
                    oldItem = match.Groups[1].Value;
                }
                if (match.Groups[1].Value.StartsWith("3")) {
                    newItem = match.Groups[1].Value;
                }
            }
            return (oldItem, newItem);
        }

        internal static List<string> Version2Components(string html) => ComponentCheck(html, "il");

        internal static List<string> Version3Components(string html) => ComponentCheck(html, "ilw");

        private static List<string> ComponentCheck(string html, string tag) {
            var returnValue = new List<string>();
            var matches = Regex.Matches(html, "<" + tag + "-(.*?)>");
            foreach (Match match in matches) {
                returnValue.Add(match.Value);
            }
            if (returnValue.Count() == 0) {
                returnValue.Add("None found");
            }
            return [.. returnValue.Distinct()];
        }
    }
}