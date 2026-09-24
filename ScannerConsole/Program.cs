using ScannerConsole;

// take a CSV file that has the referrer link in column 4, run it through an API, and list the information of the website. 
// Calculate all the 404s (in column 1) and create a dictionary with the referrer hostname as the key
// Generate files based on the hostname. 

// CHANGE THE read and write path
var pathRead = "C:\\Users\\jonker\\Downloads\\uiuc-404s.csv";
var pathWrite = "C:\\Users\\jonker\\Documents\\Output\\";
var output = new Dictionary<string, List<string>>();
var errors = new List<string>();


if (File.Exists(pathRead)) {
    Console.WriteLine("Starting process");
    var urlStrings = File.ReadAllLines(pathRead).ToList();
    foreach (var urlString in urlStrings) {
        var urlArray = urlString.Split(',').Select(a => a.Trim([' ', '"', '\''])).ToArray();
        if (urlArray.Length > 3 && urlArray[3].StartsWith("http")) {
            var hostname = "";
            try {
                var uri = new Uri(urlArray[3]);
                hostname = uri.Host.ToLowerInvariant();
            } catch (Exception e) {
                Console.Write($"{urlString},Parsing Error: {e.Message}");
                errors.Add($"{urlString},Parsing Error: {e.Message}");
            }
            if (!string.IsNullOrWhiteSpace(hostname) && !output.ContainsKey(hostname)) {
                var item = await hostname.CallApi();
                output.Add(hostname, [item]);
                output[hostname].Add($"{urlArray[3]},{urlArray[0]}");
                Console.Write($"{output.Count}, ");
            } else if (!string.IsNullOrWhiteSpace(hostname)) {
                output[hostname].Add($"{urlArray[3]},{urlArray[0]}");
            }
        }
    }
    Console.WriteLine("Finishing process");

    foreach (var item in output) {
        await File.WriteAllLinesAsync(pathWrite + item.Key.ConvertToCsv(), item.Value);
        Console.WriteLine("Writing file " + item.Key.ConvertToCsv());
    }
    await File.WriteAllLinesAsync(pathWrite + "main.csv", new[] { ApiHelper.GetHeader() }.Concat(output.Select(o => o.Value.First())));
    await File.WriteAllLinesAsync(pathWrite + "errors.csv", errors);

} else {
    Console.WriteLine($"File not found: {pathRead}");
}