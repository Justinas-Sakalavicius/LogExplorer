using System.Linq.Expressions;
using System.Text.RegularExpressions;
using LogExplorer.Models;

namespace LogExplorer.Utilities;

public static class QueryParser
{
    public static string Validate(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException(ApplicationConstants.InvalidQueryArgumentMessage, nameof(input));
        }
        
        return input;
    }
    
    public static Query Parse(string? input)
    {
        var pattern = @"(\w+)\s*=\s*'([^']*)'"; // pattern: column_name = 'search_string'
        var match = Regex.Match(input, pattern);

        if (!match.Success)
        {
            throw new FormatException(ApplicationConstants.InvalidQueryFormatMessage);
        }

        var columnName = match.Groups[1].Value;
        var searchValue = match.Groups[2].Value;

        return new Query
        {
            ColumnName = columnName,
            SearchString = searchValue
        };
    }
    
    public static Expression<Func<LogEntry, bool>> CreateContainsExpression(string propertyName, string value)
    {
        var param = Expression.Parameter(typeof(LogEntry), "logEntry");

        var dataProperty = Expression.Property(param, nameof(LogEntry.Data));

        var keyExpression = Expression.Constant(propertyName, typeof(string));

        var containsKeyMethod = typeof(Dictionary<string, string>).GetMethod("ContainsKey", new[] { typeof(string) });
        var containsKeyCall = Expression.Call(dataProperty, containsKeyMethod, keyExpression);

        var indexerProperty = typeof(Dictionary<string, string>).GetProperty("Item");
        var valueAtKey = Expression.Property(dataProperty, indexerProperty, keyExpression);

        var searchValueExpression = Expression.Constant(value, typeof(string));

        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string), typeof(StringComparison) });
        var stringComparisonExpression = Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison));
        var containsCall = Expression.Call(valueAtKey, containsMethod, searchValueExpression, stringComparisonExpression);

        var body = Expression.AndAlso(containsKeyCall, containsCall);

        return Expression.Lambda<Func<LogEntry, bool>>(body, param);
    }
}