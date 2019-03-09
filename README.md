# WorldOfAiDownloader

This is a simple Windows desktop application for downloading [World of AI](http://www.world-of-ai.com/) packages from [AVSIM](http://www.avsim.net/) for use with Microsoft Flight Simulator X. (FSX)

The application works by fetching the HTML for World of AI full package list and parsing out the FSX links. A nested list of available packages is displayed, grouping the packages by category and then by country. The user can choose one or more packages for download, specifying their AVSIM library credentials and a folder where the files will be saved. The World of AI installer application can then be used to install the packages.

Requires .NET Framework version 4.7.