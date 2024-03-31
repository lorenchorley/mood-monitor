public class InMemoryTextDataSource(string text) : ITextDataSource
{
    public Task<string> GetText()
    {
        return Task.FromResult(text);
    }
}