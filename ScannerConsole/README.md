# ScannerConsole

A .NET console application that processes a CSV file of 404 errors (with referrer URLs) and generates per-host CSV reports by calling the Illinois Site Scanner API.

## What it does

1. Reads a CSV file where **column 4** contains referrer URLs (e.g., the page that linked to a broken resource) and **column 1** contains the broken URL (404)
2. Extracts the hostname from each referrer URL
3. Calls the public Illinois Site Scanner API (`https://sitescanner.wigg.illinois.edu/api/scanner?url={hostname}`) **once per unique hostname** to fetch site information (IP, hosting type, CMS, toolkit usage, headers, etc.)
4. Aggregates all 404s by hostname
5. Writes output:
   - One CSV per hostname (`{hostname}.csv`), with scanner results and all broken URLs from that host
   - `main.csv` — summary of all hosts scanned
   - `errors.csv` — any parsing or API errors encountered

## Tech stack

- .NET `10.0`
- Top-level statements (no explicit `Main` method)
- `HttpClient.GetFromJsonAsync` + `System.Text.Json`

## Quick start

### Prerequisites
- .NET SDK `10.0`

### Configure paths

Edit `Program.cs` lines **8–9**:

- **pathRead**: Full path to your input CSV (must have at least 4 columns; column 4 = referrer URL, column 1 = broken URL)
- **pathWrite**: Directory where output CSVs will be written (must exist

## Output files:

- `foo-edu.csv`:
  - Line 1: API scanner result for `foo.edu`
  - Line 2: `https://foo.edu/index.html,https://example.edu/bad.html`
  - Line 3: `https://foo.edu/about.html,https://example.edu/bad2.html`
- `bar-org.csv`:
  - Line 1: API scanner result for `bar.org`
  - Line 2: `https://bar.org/contact.html,https://other.com/missing`
- `main.csv`:
  - Line 1: Scanner result for `foo.edu`
  - Line 2: Scanner result for `bar.org`
- `errors.csv`: (if any parsing/API failures)

## Notes / caveats

- The tool only makes **one API call per unique hostname** (to avoid redundant lookups).
- The API endpoint is hardcoded to `https://sitescanner.wigg.illinois.edu/api/scanner?url={hostname}` (line 10 of `ApiHelper.cs`). If you want to use a different endpoint (e.g., `localhost`), update `ApiHelper.cs`.
- Error handling is basic; any row that doesn't have at least 4 columns or whose referrer doesn't start with `http` is skipped silently (no error logged).
- Output is **not** escaped/quoted beyond what the API returns. 