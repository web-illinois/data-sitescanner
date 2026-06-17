using HtmlAgilityPack;
using IllinoisSiteScannerWeb.Data.Web;
using System.Net;

namespace IllinoisSiteScannerWeb.Data {
    public class WebsiteManager {
        public static async Task<WebsiteInformation> Load(string url) {
            if (string.IsNullOrWhiteSpace(url)) {
                return new WebsiteInformation();
            }
            if (!url.StartsWith("http")) {
                url = "https://" + url;
            }
            var returnValue = new WebsiteInformation {
                Url = url
            };

            try {
                var cookieContainer = new CookieContainer();
                using var httpClientHandler = new HttpClientHandler {
                    CookieContainer = cookieContainer
                };
                using var httpClient = new HttpClient(httpClientHandler);
                var response = await httpClient.SendAsync(new HttpRequestMessage {
                    Version = HttpVersion.Version10,
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                });

                _ = response.EnsureSuccessStatusCode();
                var html = await response.Content.ReadAsStringAsync();

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var myUri = new Uri(url);
                var ip = Dns.GetHostAddresses(response?.RequestMessage?.RequestUri?.Host ?? myUri.Host)[0];
                returnValue.AbsoluteUri = response?.RequestMessage?.RequestUri?.AbsoluteUri ?? myUri.AbsoluteUri;
                returnValue.Host = response?.RequestMessage?.RequestUri?.Host ?? myUri.Host;
                returnValue.IpAddress = ip.ToString();
                returnValue.CmsInformation = CmsInformation.Check(html, returnValue.IpAddress);
                returnValue.HostingInformation = HostingInformation.Check(returnValue.IpAddress, returnValue.Host, returnValue.CmsInformation);
                returnValue.ServerInformation = ServerInformation.Check(response?.Headers?.Server?.ToString() ?? "");
                var (oldToolkit, newToolkit) = ToolkitInformation.Check(html);
                returnValue.OldToolkit = oldToolkit;
                returnValue.NewToolkit = newToolkit;
                var (owner, host) = AttributeInformation.CheckAttributes(doc);
                returnValue.Owner = owner;
                if (!string.IsNullOrWhiteSpace(host)) {
                    returnValue.HostingInformation = host;
                }
                returnValue.HeaderInformation = HeaderInformation.Check(doc);
                returnValue.PrimarySiteInformation = PrimarySiteInformation.CheckSite(html);
                returnValue.ParentSiteInformation = PrimarySiteInformation.Check(html);
                returnValue.HostingInformationHelpLink = HostingInformation.GetLink(returnValue.HostingInformation);
                returnValue.V2Components = ToolkitInformation.Version2Components(html);
                returnValue.V3Components = ToolkitInformation.Version3Components(html);
                returnValue.IsSuccessful = true;
            } catch (Exception e) {
                returnValue.IsSuccessful = false;
                returnValue.ErrorMessage = e.GetType()?.ToString() ?? "" + " - " + e.Message;
            }
            return returnValue;
        }
    }
}
