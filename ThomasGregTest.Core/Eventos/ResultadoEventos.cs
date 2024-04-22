using System.Text.Json.Serialization;

namespace ThomasGregTest.Core;

public class ResultadoEventos : IEvento
{
    public ResultadoEventos(bool success, object data, long? totalRows = null, int? id = null)
    {
        Success = success;
        Data = data;
        TotalRows = totalRows;
        Id = id;
    }

    public bool Success { get; private set; }
    public object Data { get; private set; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? TotalRows { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Id { get; private set; }
}
