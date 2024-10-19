using System.Linq.Expressions;
using LogExplorer.Models;

namespace LogExplorer.Services;

public static class ExpressionBuilder
    {
        public static Expression<Func<LogEntry, bool>> BuildExpression(List<Filter> filters)
        {
            var parameter = Expression.Parameter(typeof(LogEntry), "logEntry");
            Expression expression = null;

            foreach (var filter in filters)
            {
                var conditionExpr = BuildConditionExpression(parameter, filter.Condition);

                if (expression == null)
                {
                    expression = conditionExpr;
                }
                else
                {
                    expression = filter.Operator switch
                    {
                        LogicalOperator.AND => Expression.AndAlso(expression, conditionExpr),
                        LogicalOperator.OR => Expression.OrElse(expression, conditionExpr),
                        _ => throw new NotSupportedException($"Logical operator '{filter.Operator}' is not supported.")
                    };
                }
            }

            return Expression.Lambda<Func<LogEntry, bool>>(expression, parameter);
        }

        private static Expression BuildConditionExpression(ParameterExpression param, Condition condition)
        {
            var dataProperty = Expression.Property(param, nameof(LogEntry.Data));
            
            var keyExpression = Expression.Constant(condition.ColumnName);
            
            var containsKeyMethod = typeof(Dictionary<string, string>).GetMethod("ContainsKey");
            var containsKeyCall = Expression.Call(dataProperty, containsKeyMethod, keyExpression);
            
            var indexerProperty = typeof(Dictionary<string, string>).GetProperty("Item");
            var valueExpression = Expression.Property(dataProperty, indexerProperty, keyExpression);
            
            var comparisonExpression = BuildStringComparison(valueExpression, condition.Operator, condition.Value);

            var conditionExpr = Expression.AndAlso(containsKeyCall, comparisonExpression);
            return conditionExpr;
        }

        private static Expression BuildStringComparison(Expression valueExpression, string op, string stringValue)
        {
            var searchValueExpression = Expression.Constant(stringValue, typeof(string));
            
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string), typeof(StringComparison) });
            var equalMethod = typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(StringComparison) });
            
            var stringComparisonExpression = Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison));
            
            var containsExpressionCall = Expression.Call(valueExpression, containsMethod, searchValueExpression, stringComparisonExpression);
            var equalsExpressionCall = Expression.Call(valueExpression, equalMethod, searchValueExpression, stringComparisonExpression);
            
            return op switch
            {
                "=" => containsExpressionCall,
                "==" => equalsExpressionCall,
                "!=" => Expression.Not(equalsExpressionCall),
                _ => throw new NotSupportedException($"Operator '{op}' is not supported for string comparisons.")
            };
        }
    }