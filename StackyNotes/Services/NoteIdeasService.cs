using System.Text.Json;
using StackyNotes.Models;

namespace StackyNotes.Services;

public class NoteIdeasService
{
    private const string FileName = "noteideas.json";

    public async Task<List<NoteIdea>> LoadIdeasAsync()
    {
        await using var stream = await FileSystem.OpenAppPackageFileAsync(FileName);
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<List<NoteIdea>>(json, options) ?? new List<NoteIdea>();
    }
}
