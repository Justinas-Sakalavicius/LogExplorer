using System.Linq.Expressions;
using LogExplorer.Models;

namespace LogExplorer.Services;

public static class LogSearch
{
    public static List<LogEntry> SearchLogs(List<LogEntry> logs, Expression<Func<LogEntry, bool>> predicate)
    {
        var compiledPredicate = predicate.Compile();
        return logs.Where(compiledPredicate).ToList();
    }
}