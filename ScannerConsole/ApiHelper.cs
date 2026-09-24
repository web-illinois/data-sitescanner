using System.Dynamic;
using System.Net.Http.Json;
using System.Text.Json;

namespace ScannerConsole {
    internal static class ApiHelper {
        static HttpClient client = new HttpClient();

        internal static async Task<string> CallApi(this string hostname) {
            var url = "https://sitescanner.wigg.illinois.edu/api/scanner?url=" + hostname;
            try {
                var result = await client.GetFromJsonAsync<dynamic>(url);
                dynamic d = JsonSerializer.Deserialize<ExpandoObject>(result);
                return $"{d?.url},{d?.isSuccessful},{d?.errorMessage},{d?.ipAddress},{d?.hostingInformation},{d?.hostingHtml},{d?.cmsInformation},\"{d?.headerInformation.ToString().Replace("\n", "").Replace("\r", "").Trim()}\",{d?.primarySiteInformation},{d?.parentSiteInformation},{d?.serverInformation}";
            } catch (Exception e) {
                return $"{url},Error: {e.Message}";
            }
        }

        internal static string GetHeader() => "URL,IsSuccessful?,Error Message,IP Address,Hosting Information,Hosting Link,CMS Information,Header Information,Primary Site,Parent Site,Server Information";

        internal static string ConvertToCsv(this string s) => s.Replace(".", "-").Replace("http://", "").Replace("https://", "") + ".csv";
    }
}
