using Newtonsoft.Json;

public class contentStorage
{
    public readonly Dictionary<string, dynamic> files = new();

    public contentStorage()
    {
        ReadAllJsonFiles(Constants.contentDir);
    }

    public void ReadAllJsonFiles(string directoryPath)
    {
        string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json");

        foreach (string filePath in jsonFiles)
        {
            string fileName = Path.GetFileName(filePath);
            string json = File.ReadAllText(filePath);
            dynamic? content = JsonConvert.DeserializeObject<dynamic>(json); // Deserialize using Newtonsoft.Json
            if (content != null)
                files.Add(fileName.Replace(".json", ""), content);
        }
    }
}
