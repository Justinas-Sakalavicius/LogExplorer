namespace LogExplorer.Utilities;

public static class ApplicationConstants
{
    public const string SupportedFileExtension = "*.csv";
    public const string FilesPath = "LogFiles";
    
    //Errors
    public const string InvalidQueryArgumentMessage = "Invalid query argument provided";
    public const string InvalidQueryFormatMessage = "Invalid query format. Use: column_name = 'search_string'";
    public const string ColumnNotFoundMessage = "Column not found";
}