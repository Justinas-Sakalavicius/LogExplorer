using System.Text.RegularExpressions;
using LogExplorer.Models;
using LogExplorer.Utilities;

namespace LogExplorer.Services;

public static class QueryParser
{
    public static List<Filter> ParseQuery(string? queryInput)
    {
        if (string.IsNullOrWhiteSpace(queryInput))
        {
            throw new ArgumentException(ApplicationConstants.InvalidQueryArgumentMessage, nameof(queryInput));
        }

        var tokens = TokenizeQuery(queryInput);
        var filters = BuildFilters(tokens);
        
        return filters;
    }
    
    private static List<string> TokenizeQuery(string query)
    {
        var tokens = new List<string>();
        
        foreach (Match match in Regex.Matches(query, ApplicationConstants.QueryRegexPattern))
        {
            tokens.Add(match.Value);
        }
        
        return tokens;
    }
    
    private static List<Filter> BuildFilters(List<string> tokens)
    {
        var filters = new List<Filter>();
        var index = 0;

        while (index < tokens.Count)
        {
            var filter = new Filter();

            if (filters.Count > 0) // If it's not the first condition, expect a logical operator
            {
                var logicalOp = tokens[index++].ToLower();
                filter.Operator = ParseLogicalOperator(logicalOp);
            }

            filter.Condition = new Condition
            {
                ColumnName = tokens[index++],
                Operator = tokens[index++],
                Value = tokens[index++].Trim('\'')
            };

            filters.Add(filter);
        }

        return filters;
    }

    private static LogicalOperator ParseLogicalOperator(string token)
    {
        return token.ToUpper() switch
        {
            "AND" => LogicalOperator.AND,
            "OR" => LogicalOperator.OR,
            _ => throw new Exception($"Invalid logical operator: {token}")
        };
    }
}