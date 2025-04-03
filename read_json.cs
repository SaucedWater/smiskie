using System.Text.Json;

using (var stream = await FileSystem.OpenAppPackageFileAsync("data.json"))
using (var reader = new StreamReader(stream))
{
    string json = await reader.ReadToEndAsync();
    var items = JsonSerializer.Deserialize<List<YourModel>>(json);

    db.InsertAll(items);
}