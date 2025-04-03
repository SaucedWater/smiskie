using (var stream = await FileSystem.OpenAppPackageFileAsync("data.csv"))
using (var reader = new StreamReader(stream))
{
    var dataList = new List<YourModel>();

    while (!reader.EndOfStream)
    {
        var line = await reader.ReadLineAsync();
        var parts = line.Split(',');

        var item = new YourModel
        {
            Id = int.Parse(parts[0]),
            Name = parts[1],
            Age = int.Parse(parts[2])
        };

        dataList.Add(item);
    }

    db.InsertAll(dataList);
}