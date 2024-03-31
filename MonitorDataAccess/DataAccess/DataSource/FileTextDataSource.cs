using System.Text;

public class FileTextDataSource(string filePath) : ITextDataSource
{
    public async Task<string> GetText()
    {
        return File.ReadAllText(filePath, Encoding.UTF8);
    }
}