using System.Linq.Expressions;
using LogExplorer.Models;
using LogExplorer.Utilities;

namespace LogExplorer.Services;

public class LogSearch
{
    public void Validate(List<LogEntry> logs, Query query)
    {
        if (logs.Count == 0 || !logs[0].Data.ContainsKey(query.ColumnName))
        {
            throw new ArgumentException(ApplicationConstants.ColumnNotFoundMessage);
        }
    }

    public List<LogEntry> SearchLogs(List<LogEntry> logs, Expression<Func<LogEntry, bool>> predicate)
    {
        var compiledPredicate = predicate.Compile();
        return logs.Where(compiledPredicate).ToList();
    }
}