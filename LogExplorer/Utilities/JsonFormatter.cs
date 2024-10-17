using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using LogExplorer.Models;

namespace LogExplorer.Utilities;

public static class JsonFormatter
{
    public static string FormatToJson(List<LogEntry> results, string searchQuery)
    {
        var output = new
        {
            searchQuery,
            logCount = results.Count,
            result = results.ConvertAll(entry => entry.Data)
        };
        
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // required for apostrophes
            WriteIndented = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        return JsonSerializer.Serialize(output, options);
    }
}