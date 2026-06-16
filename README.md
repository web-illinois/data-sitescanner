# IllinoisSiteScannerWeb

A small ASP.NET Core web app that contains two sets of tools

## Illinois Site Scanner

A website that checks a website’s basic platform characteristics (IP/hosting hints, CMS hints, Illinois toolkit hints, headers, server banner, etc.). It includes:

- A simple web UI (`/`) to enter a URL and view results
- A JSON API endpoint: `/api/scanner?url={url}`

### Hosting information

Hosting information (cPanel, PIE) is looked up via IP address / Name using the static class `HostingInformation`. 

This class contains a hardcoded list of IP addresses and hostnames and their associated hosting information. If you need to add your hosting information, you can add it to the `HostingInformation` class. 

### Future Plans

This can go one of two ways:

#### Data Repository

A database of Illinois-related websites and their characteristics, which can be used for research and analysis. This would involve:
- URL
- NetID of owner (as well as a possible alternate Net ID)
- Website title
- Hosting information (cPanel, PIE, etc.)
- Hosting Information code if applicable (cPanel reseller information)
- Notes
- Last Updated

This will require a database and a way to populate it, which could be done through the web UI or through an API.
- login, with the ability to update and delete individual sites
- login, with the ability to update and delete hosting information codes and add API keys for bots/scripts to use to populate the database. This will also allow us to manage the IP information via a database.
- an API endpoint to add/update entries, which could be used by the hosting platforms to populate the database
- the ability to transfer information to another NetID. 

#### Data Pointer

A help guide for each Hosting information (cPanel, PIE, etc.) that provides information on how to get owner information. This would be a hardcoded HTML page that can be updated via Github. 

If we use this, we still need a way to get Net IDs of the owner of the site. Recommend using data-* tags on the body, specifically:
* data-owner: NetID of the owner
* data-host: Name of the host if not available through IP addresses

``` <body data-owner="jonker" data-host="Random Hosting Service"> ```

## Emergency Checker

An API that scans the Illini Alert emergency notification system for potential issues. It includes:

- A JSON API endpoint: `/api/emergency`
- A JSON API endpoint with full information: `/api/emergency/full`
