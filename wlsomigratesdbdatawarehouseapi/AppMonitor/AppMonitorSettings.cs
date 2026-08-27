namespace wlsomigratesdbdatawarehouseapi.AppMonitor;

public class AppMonitorSettings {
    public static readonly string SectionName = "AppMonitor";

    public string Host { get; set; } = string.Empty;
    public string AppId { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string UrlFormat { get; set; } = string.Empty;
    public bool Enabled { get; set; }
}
