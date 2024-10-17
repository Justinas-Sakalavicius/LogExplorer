using LogExplorer.Services;
using LogExplorer.Utilities;

var filePaths = Directory.GetFiles(ApplicationConstants.FilesPath, ApplicationConstants.SupportedFileExtension);
var logParser = new LogParser();
var logSearcher = new LogSearch();
var logs = logParser.ParseFiles(filePaths);

Console.WriteLine("Enter your query:");
var queryInput = Console.ReadLine();
var validQuery = QueryParser.Validate(queryInput);
var query = QueryParser.Parse(validQuery);

logSearcher.Validate(logs, query);
var results = logSearcher.SearchLogs(logs, query);

if (results.Count == 0)
{
    Console.WriteLine("No matching records found.");
    return;
}

var jsonOutput = JsonFormatter.FormatToJson(results, validQuery);
Console.WriteLine(jsonOutput);