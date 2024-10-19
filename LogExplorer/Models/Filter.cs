namespace LogExplorer.Models;

public class Filter
{
    public Condition Condition { get; set; }
    public LogicalOperator? Operator { get; set; }
}