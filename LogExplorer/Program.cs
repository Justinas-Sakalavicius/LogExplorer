using LogExplorer.Models;
using LogExplorer.Services;
using LogExplorer.Utilities;

var filePaths = Directory.GetFiles("LogFiles", "*.csv");
var logParser = new LogParser();
var logSearcher = new LogSearch();
var logs = new List<LogEntry>();

logs = logParser.ParseFiles(filePaths);

Console.WriteLine("Enter your query:");
var queryInput = Console.ReadLine();
var validQuery = QueryParser.Validate(queryInput);
var query = QueryParser.Parse(validQuery);


logSearcher.Validate(logs, query);
var results = logSearcher.SearchLogs(logs, query);
var jsonOutput = JsonFormatter.FormatToJson(results, validQuery);
Console.WriteLine(jsonOutput);