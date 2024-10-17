using LogExplorer.Models;

namespace LogExplorer.Services;

public class LogSearch
{
    public List<LogEntry> SearchLogs(List<LogEntry> logs, Query query)
    {
        return logs
            .Where(log => log.Data[query.ColumnName].Contains(query.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void Validate(List<LogEntry> logs, Query query)
    {
        if (logs.Count == 0 || !logs[0].Data.ContainsKey(query.ColumnName))
        {
            throw new ArgumentOutOfRangeException("column not found");
        }
    }
}