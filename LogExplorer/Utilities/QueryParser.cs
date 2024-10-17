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
    
    public static Query Parse(string input)
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
}