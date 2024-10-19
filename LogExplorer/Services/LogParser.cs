using System.Globalization;
using CsvHelper;
using LogExplorer.Models;

namespace LogExplorer.Services;

public static class LogParser
{
    public static List<LogEntry> ParseFiles(string[] filePaths)
    {
        var logEntries = new List<LogEntry>();

        foreach (var filePath in filePaths)
        {
            var result = ParseFile(filePath);
            logEntries.AddRange(result);
        }

        return logEntries;
    }
    
    private static List<LogEntry> ParseFile(string filePath)
    {
        var logEntries = new List<LogEntry>();
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Read();
        csv.ReadHeader(); // checks if the headers exist
        var headers = csv.HeaderRecord;
            
        while (csv.Read())
        {
            var logEntry = new LogEntry();
            foreach (var header in headers)
            {
                logEntry.Data[header] = csv.GetField(header);
            }
            logEntries.Add(logEntry);
        }
        
        return logEntries;
    }
}