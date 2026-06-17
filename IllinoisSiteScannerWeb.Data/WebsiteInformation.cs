namespace IllinoisSiteScannerWeb.Data {
    public class WebsiteInformation {
        public string Url { get; set; } = "";
        public string Host { get; set; } = "";
        public string AbsoluteUri { get; set; } = "";
        public string IpAddress { get; set; } = "";
        public string HostingInformation { get; set; } = "";
        public string HostingInformationHelpLink { get; set; } = "";
        public string HostingHtml => string.IsNullOrWhiteSpace(HostingInformationHelpLink) ? HostingInformation : $"<a href='{HostingInformationHelpLink}' target='_blank'>{HostingInformation}</a>";
        public string CmsInformation { get; set; } = "";
        public string NewToolkit { get; set; } = "";
        public string OldToolkit { get; set; } = "";
        public string Owner { get; set; } = "";

        public string HeaderInformation { get; set; } = "";
        public string PrimarySiteInformation { get; set; } = "";
        public string ParentSiteInformation { get; set; } = "";

        public string ServerInformation { get; set; } = "";
        public bool IsSuccessful { get; set; } = false;
        public string ErrorMessage { get; set; } = "";

        public List<string> V2Components { get; set; } = [];

        public List<string> V3Components { get; set; } = [];
    }
}
