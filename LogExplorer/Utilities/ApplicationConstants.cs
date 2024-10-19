namespace LogExplorer.Utilities;

public static class ApplicationConstants
{
    public const string SupportedFileExtension = "*.csv";
    public const string FilesPath = "LogFiles";
    
    public const string QueryRegexPattern = @"('.*?'|\S+)";
    public const string InvalidQueryArgumentMessage = "Query input is empty";
    public const string ColumnNotFoundMessage = "Column not found";
}