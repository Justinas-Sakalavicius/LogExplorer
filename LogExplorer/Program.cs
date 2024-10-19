using LogExplorer.Services;
using LogExplorer.Utilities;

var filePaths = Directory.GetFiles(ApplicationConstants.FilesPath, ApplicationConstants.SupportedFileExtension);
var logs = LogParser.ParseFiles(filePaths);

Console.WriteLine("Enter your query:");
var queryInput = Console.ReadLine();

var parsedQuery = QueryParser.ParseQuery(queryInput); 
var predicateExpression = ExpressionBuilder.BuildExpression(parsedQuery);
var results = LogSearch.SearchLogs(logs, predicateExpression);

var jsonOutput = JsonFormatter.FormatToJson(results, queryInput);
Console.WriteLine(jsonOutput);